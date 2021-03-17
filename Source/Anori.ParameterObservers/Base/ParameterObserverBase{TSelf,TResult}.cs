// -----------------------------------------------------------------------
// <copyright file="PropertyObserverBase{TSelf,TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Base
{
    using System;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers.Base;
    using Anori.ExpressionObservers.Tree;

    using JetBrains.Annotations;

    /// <summary>
    ///     Property Observer Base.
    /// </summary>
    /// <typeparam name="TSelf">The type of the self.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="PropertyObserverBase{TSelf}" />
    /// <seealso cref="PropertyObserverBase" />
    public abstract class ParameterObserverBase<TSelf, TResult> : ParameterObserverBase<TSelf>
        where TSelf : ParameterObserverBase<TSelf, TResult>
    {
        /// <summary>
        ///     The property expression.
        /// </summary>
        private readonly Expression<Func<TResult>> propertyExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserverBase{TSelf,TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">propertyExpression is null.</exception>
        protected ParameterObserverBase([NotNull] Expression<Func<TResult>> propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = this.CreateChain();
        }

        /// <summary>
        ///     Gets the expression string.
        /// </summary>
        /// <value>
        ///     The expression string.
        /// </value>
        public override string ExpressionString { get; }

        /// <summary>
        ///     Creates the chain.
        /// </summary>
        /// <returns>
        ///     The Expression String.
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     Operation not supported for the given expression type {expression.Type}. "
        ///     + "Only MemberExpression and ConstantExpression are currently supported.
        /// </exception>
        protected string CreateChain()
        {
            var tree = ExpressionTree.GetTree(this.propertyExpression.Body);
            var expressionString = this.propertyExpression.ToString();

            this.CreateChain(tree);

            return expressionString;
        }
    }
}