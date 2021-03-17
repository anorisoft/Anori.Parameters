// -----------------------------------------------------------------------
// <copyright file="PropertyObserverBase{TSelf,TParameter1,TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Base
{
    using System;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers;
    using Anori.ExpressionObservers.Interfaces;
    using Anori.ExpressionObservers.Tree;
    using Anori.ExpressionObservers.Tree.Interfaces;

    using JetBrains.Annotations;

    /// <summary>
    ///     Property Observer Base.
    /// </summary>
    /// <typeparam name="TSelf">The type of the self.</typeparam>
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="ParameterObserverBase" />
    /// <seealso cref="ParameterObserverBase" />
    public abstract class ParameterObserverBase<TSelf, TParameter1, TResult> : ParameterObserverBase<TSelf>
        where TSelf : ParameterObserverBase<TSelf, TParameter1, TResult>
    {
        /// <summary>
        ///     The property expression.
        /// </summary>
        private readonly Expression<Func<TParameter1, TResult>> propertyExpression;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserverBase{TSelf,TParameter1,TResult}" /> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     parameter1 is null.
        /// </exception>
        protected ParameterObserverBase(
            TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.Parameter1 = parameter1 ?? throw new ArgumentNullException(nameof(parameter1));
            this.ExpressionString = this.CreateChain(parameter1);
        }

        /// <summary>
        ///     Gets the expression string.
        /// </summary>
        /// <value>
        ///     The expression string.
        /// </value>
        public override string ExpressionString { get; }

        /// <summary>
        ///     Gets the parameter1.
        /// </summary>
        /// <value>
        ///     The parameter1.
        /// </value>
        [CanBeNull]
        public TParameter1 Parameter1 { get; }

        /// <summary>
        ///     Creates the chain.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <returns>
        ///     The Expression String.
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     Operation not supported for the given expression type {expression.Type}. "
        ///     + "Only MemberExpression and ConstantExpression are currently supported.
        /// </exception>
        private string CreateChain(TParameter1 parameter1)
        {
            var tree = ExpressionTree.GetTree(this.propertyExpression.Body);
            var expressionString = this.propertyExpression.ToString();

            this.CreateChain(parameter1, tree);

            return expressionString;
        }


        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="nodes">The nodes.</param>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="NotSupportedException">Expression Tree Node not supported.</exception>
        private void CreateChain(TParameter1 parameter1, IExpressionTree nodes)
        {
            foreach (var treeRoot in nodes.Roots)
            {
                switch (treeRoot)
                {
                    case IParameterNode parameterElement:
                        {
                            if (!(parameterElement is { Next: IPropertyNode propertyElement }))
                            {
                                continue;
                            }

                            var parameterGetter = ExpressionGetter.CreateParameterGetter(
                                parameterElement,
                                this.propertyExpression);

                            var owner = parameterGetter(parameter1);
                            var root = this.CreateParameterObserverChain(owner,
                                propertyElement);
                            this.RootNodes.Add(root);
                            break;
                        }

                    case IConstantNode constantElement when treeRoot.Next is IFieldNode fieldElement:
                        {
                            if (!(fieldElement is { Next: IPropertyNode propertyElement }))
                            {
                                continue;
                            }

                            var root = this.CreateParameterObserverChain(
                                fieldElement.FieldInfo.GetValue(constantElement.Value),
                                propertyElement);

                            this.RootNodes.Add(root);
                            break;
                        }

                    case IConstantNode constantElement:
                        {
                            if (!(treeRoot is { Next: IPropertyNode propertyElement }))
                            {
                                continue;
                            }

                            var root = this.CreateParameterObserverChain(constantElement.Value, propertyElement);

                            this.RootNodes.Add(root);
                            break;
                        }


                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}