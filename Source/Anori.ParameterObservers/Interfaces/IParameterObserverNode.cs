// -----------------------------------------------------------------------
// <copyright file="IParameterObserverNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Interfaces
{
    using Anori.Parameters;

    /// <summary>
    /// The Parameter Observer Node interface.
    /// </summary>
    internal interface IParameterObserverNode
    {
        /// <summary>
        /// Unsubscribes the listener.
        /// </summary>
        void UnsubscribeListener();

        /// <summary>
        /// Subscribes the listener for.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void SubscribeListenerFor(IReadOnlyParameter parameter);
    }
}