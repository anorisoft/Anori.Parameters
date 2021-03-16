// -----------------------------------------------------------------------
// <copyright file="ParameterObserverRootNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Nodes
{
    using System;
    using System.Reflection;

    using Anori.ParameterObservers.Interfaces;
    using Anori.Parameters;

    /// <summary>
    /// The Parameter Observer Root Node class.
    /// </summary>
    /// <seealso cref="Anori.ParameterObservers.Nodes.ParameterObserverNode" />
    /// <seealso cref="Anori.ParameterObservers.Interfaces.IParameterObserverRootNode" />
    internal class ParameterObserverRootNode : ParameterObserverNode, IParameterObserverRootNode
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootPropertyObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        public ParameterObserverRootNode(
            PropertyInfo propertyInfo,
            Action action,
//            object owner,
            IReadOnlyParameter parameter)
            : base(propertyInfo, action)
        {
   //         this.Owner = owner;
            this.Parameter = parameter;
        }

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public IReadOnlyParameter Parameter { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
     //   public object Owner { get; }

        /// <summary>
        ///     Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }
}