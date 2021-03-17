// -----------------------------------------------------------------------
// <copyright file="IParameterObserverRootNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Interfaces
{
    /// <summary>
    /// The Parameter Observer Root Node interface.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="IParameterObserverNode" />
    internal interface IParameterObserverRootNode : IParameterObserverNode
    {
        /// <summary>
        /// Subscribes the listener for owner.
        /// </summary>
        void SubscribeListenerForOwner();
    }
}