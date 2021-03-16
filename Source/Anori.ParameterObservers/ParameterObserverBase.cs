// -----------------------------------------------------------------------
// <copyright file="ParameterObserverBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Anori.ExpressionObservers.Interfaces;
    using Anori.ExpressionObservers.Observers;
    using Anori.ParameterObservers.Interfaces;
    using Anori.ParameterObservers.Nodes;
    using Anori.Parameters;

    /// <summary>
    ///     Property Observer Base.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{Anori.ParameterObservers.ParameterObserverBase}" />
    /// <seealso cref="System.IEquatable{Anori.ParameterObservers.ParameterObserverBase}" />
    /// <seealso cref="PropertyObserverBase" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="PropertyObserverBase" />
    public abstract class ParameterObserverBase : IDisposable,
                                                  IEqualityComparer<ParameterObserverBase>,
                                                  IEquatable<ParameterObserverBase>
    {
        private bool isSubscribe;

        /// <summary>
        ///     Gets the expression string.
        /// </summary>
        /// <value>
        ///     The expression string.
        /// </value>
        public abstract string ExpressionString { get; }

        /// <summary>
        ///     Gets the root nodes.
        /// </summary>
        /// <value>
        ///     The root nodes.
        /// </value>
        internal IList<IParameterObserverRootNode> RootNodes { get; } = new List<IParameterObserverRootNode>();

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(ParameterObserverBase a, ParameterObserverBase b)
        {
            return Equals(a, b);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(ParameterObserverBase a, object b)
        {
            return Equals(a, b);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(ParameterObserverBase a, ParameterObserverBase b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(ParameterObserverBase a, object b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        ///     Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        ///     true if the specified objects are equal; otherwise, false.
        /// </returns>
        public static bool Equals(ParameterObserverBase x, ParameterObserverBase y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            if (x.ExpressionString != y.ExpressionString)
            {
                return false;
            }

            if (!x.RootNodes.SequenceEqual(y.RootNodes))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(ParameterObserverBase other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.RootNodes.SequenceEqual(other.RootNodes) && this.ExpressionString == other.ExpressionString;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ParameterObserverBase)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.RootNodes != null ? this.RootNodes.GetHashCode() : 0) * 397)
                       ^ (this.ExpressionString != null ? this.ExpressionString.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        ///     true if the specified objects are equal; otherwise, false.
        /// </returns>
        bool IEqualityComparer<ParameterObserverBase>.Equals(ParameterObserverBase x, ParameterObserverBase y)
        {
            return Equals(x, y);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(ParameterObserverBase obj)
        {
            unchecked
            {
                return ((obj.ExpressionString != null ? obj.ExpressionString.GetHashCode() : 0) * 397)
                       ^ (obj.RootNodes != null ? obj.RootNodes.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            this.Subscribe(false);
        }

        /// <summary>
        ///     Subscribes the specified silent.
        /// </summary>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        public void Subscribe(bool silent)
        {
            if (this.isSubscribe)
            {
                return;
            }

            this.isSubscribe = true;
            foreach (var rootPropertyObserverNode in this.RootNodes)
            {
                rootPropertyObserverNode.SubscribeListenerForOwner();
            }

            if (!silent)
            {
                this.OnAction();
            }
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        public void Unsubscribe()
        {
            if (!this.isSubscribe)
            {
                return;
            }

            this.isSubscribe = false;

            foreach (var rootPropertyObserverNode in this.RootNodes)
            {
                rootPropertyObserverNode.UnsubscribeListener();
            }
        }

        /// <summary>
        ///     Looptrees the specified expression node.
        /// </summary>
        /// <param name="expressionNode">The expression node.</param>
        /// <param name="observerNode">The observer node.</param>
        private ParameterObserverNode LoopTree(IExpressionNode expressionNode, ParameterObserverNode observerNode)
        {
            var previousNode = observerNode;
            while (expressionNode.Next is IPropertyNode property)
            {
                if (!typeof(IReadOnlyParameter).IsAssignableFrom(property.Type))
                {
                    expressionNode = expressionNode.Next;
                    continue;
                }

                var currentNode = new ParameterObserverNode(property.PropertyInfo, this.OnAction);

                previousNode.Previous = currentNode;
                previousNode = currentNode;
                expressionNode = expressionNode.Next;
            }

            return previousNode;
        }

        /// <summary>
        ///     The action.
        /// </summary>
        protected abstract void OnAction();

        ///// <summary>
        /////     Creates the chain.
        ///// </summary>
        ///// <param name="parameter1">The parameter1.</param>
        ///// <param name="tree">The nodes.</param>
        ///// <exception cref="NotSupportedException">Expression Tree Node not supported.</exception>
        //protected void CreateChain(INotifyPropertyChanged parameter1, IExpressionTree tree)
        //{
        //    foreach (var treeRoot in tree.Roots)
        //    {
        //        switch (treeRoot)
        //        {
        //            case ParameterNode parameterElement:
        //                {
        //                    if (!(parameterElement.Next is PropertyNode propertyElement))
        //                    {
        //                        continue;
        //                    }

        //                    var root = new RootPropertyObserverNode(
        //                        propertyElement.PropertyInfo,
        //                        this.OnAction,
        //                        parameter1);
        //                    this.LoopTree(propertyElement, root);
        //                    this.RootNodes.Add(root);
        //                    break;
        //                }

        //            case ConstantNode constantElement when treeRoot.Next is FieldNode fieldElement:
        //                {
        //                    if (!(fieldElement.Next is PropertyNode propertyElement))
        //                    {
        //                        continue;
        //                    }

        //                    var root = new RootPropertyObserverNode(
        //                        propertyElement.PropertyInfo,
        //                        this.OnAction,
        //                        (INotifyPropertyChanged)fieldElement.FieldInfo.GetValue(constantElement.Value));

        //                    this.LoopTree(propertyElement, root);
        //                    this.RootNodes.Add(root);
        //                    break;
        //                }

        //            case ConstantNode constantElement:
        //                {
        //                    if (!(treeRoot.Next is PropertyNode propertyElement))
        //                    {
        //                        continue;
        //                    }

        //                    var root = new RootPropertyObserverNode(
        //                        propertyElement.PropertyInfo,
        //                        this.OnAction,
        //                        (INotifyPropertyChanged)constantElement.Value);

        //                    this.LoopTree(propertyElement, root);
        //                    this.RootNodes.Add(root);

        //                    break;
        //                }

        //            default:
        //                throw new NotSupportedException();
        //        }
        //    }
        //}

        /// <summary>
        ///     Creates the chain.
        /// </summary>
        /// <param name="tree">The nodes.</param>
        /// <exception cref="System.NotSupportedException">Expression Tree Node not supported.</exception>
        protected void CreateChain(IExpressionTree tree)
        {
            foreach (var treeRoot in tree.Roots)
            {
                switch (treeRoot)
                {
                    case IConstantNode constantElement when treeRoot.Next is IFieldNode fieldElement:
                        {
                            if (!(fieldElement.Next is IPropertyNode propertyElement))
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
                            if (!(treeRoot.Next is IPropertyNode propertyElement))
                            {
                                continue;
                            }

                            var root = this.CreateParameterObserverChain(constantElement.Value, propertyElement);

                            this.RootNodes.Add(root);
                            break;
                        }

                    default:
                        throw new NotSupportedException($"{treeRoot}");
                }
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Unsubscribe();
            }
        }

        private protected IParameterObserverRootNode CreateParameterObserverChain(object parameter, IPropertyNode property)
        {
            ParameterObserverRootNode parameterObserverRoot;
            IPropertyNode next;
            if (parameter is IReadOnlyParameter p)
            {
                if (!(property is { Next: IPropertyNode nextProperty }))
                {
                    return new ParameterObserverSingleNode(this.OnAction, parameter, p);
                }

                parameterObserverRoot = new ParameterObserverRootNode(property.PropertyInfo, this.OnAction, p);
                next = nextProperty;
            }
            else
            {
                if (!(property.PropertyInfo.GetValue(parameter) is IReadOnlyParameter readOnlyParameter))
                {
                    throw new Exception("No Parameter.");
                }

                if (!(property is { Next: IPropertyNode nextProperty }))
                {
                    return new ParameterObserverSingleNode(this.OnAction, parameter, readOnlyParameter);
                }

                if (!(nextProperty is { Next: IPropertyNode n }))
                {
                    throw new Exception("No Parameter.");
                }
                
                next = n;
                parameterObserverRoot = new ParameterObserverRootNode(
                        n.PropertyInfo,
                        this.OnAction,
                        readOnlyParameter);
                
                //if (typeof(IReadOnlyParameter).IsAssignableFrom(nextProperty.PropertyInfo.PropertyType))
                //{
                //    parameterObserverRoot = new ParameterObserverRootNode(nextProperty.PropertyInfo, this.OnAction,  readOnlyParameter);
                //}
                //else
                //{
                //    next = property.Next as IPropertyNode;
                //    if (typeof(IReadOnlyParameter).IsAssignableFrom(nextProperty.PropertyInfo.PropertyType))
                //    {
                //        parameterObserverRoot = new ParameterObserverRootNode(
                //            nextProperty.PropertyInfo,
                //            this.OnAction,
                //            readOnlyParameter);
                //    }
                //    else
                //    {
                //        throw new Exception("No Parameter 3.");
                //    }
                //}
            }

            var previousNode = this.LoopTree(next, parameterObserverRoot);
            previousNode.Previous = new ParameterObserverEndNode(this.OnAction);
            return parameterObserverRoot;
        }
    }
}