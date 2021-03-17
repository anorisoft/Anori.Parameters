// -----------------------------------------------------------------------
// <copyright file="PropertyObserverBase{TSelf}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Base
{
    using Anori.ExpressionObservers.Base;

    /// <summary>
    ///     Property Observer Base for flurnent.
    /// </summary>
    /// <typeparam name="TSelf">The type of the self.</typeparam>
    /// <seealso cref="PropertyObserverBase" />
    public abstract class ParameterObserverBase<TSelf> : ParameterObserverBase
        where TSelf : ParameterObserverBase<TSelf>
    {
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <returns>Self object.</returns>
        public new TSelf Subscribe() => this.Subscribe(false);

        /// <summary>
        ///     Subscribes the specified silent.
        /// </summary>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        /// <returns>Self object.</returns>
        public new TSelf Subscribe(bool silent)
        {
            base.Subscribe(silent);
            return (TSelf)this;
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        /// <returns>Self object.</returns>
        public new TSelf Unsubscribe()
        {
            base.Unsubscribe();

            return (TSelf)this;
        }
    }
}