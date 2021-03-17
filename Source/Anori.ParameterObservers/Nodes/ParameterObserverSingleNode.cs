// -----------------------------------------------------------------------
// <copyright file="ParameterObserverSingleNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Nodes
{
    using System;

    using Anori.ParameterObservers.Interfaces;
    using Anori.Parameters;

    /// <summary>
    /// The Parameter Observer Single Node class.
    /// </summary>
    /// <seealso cref="Anori.ParameterObservers.Nodes.ParameterObserverEndNode" />
    /// <seealso cref="Anori.ParameterObservers.Interfaces.IParameterObserverRootNode" />
    internal class ParameterObserverSingleNode : ParameterObserverEndNode, IParameterObserverRootNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterObserverSingleNode"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="parameter">The parameter.</param>
        public ParameterObserverSingleNode(Action action, IReadOnlyParameter parameter)
            : base(action)
        {
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
        /// Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }
}