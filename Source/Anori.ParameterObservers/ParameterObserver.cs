// -----------------------------------------------------------------------
// <copyright file="PropertyObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers.Observers;

    using JetBrains.Annotations;

    /// <summary>
    ///     Property Observer.
    /// </summary>
    public static class ParameterObserver
    {
        /// <summary>
        ///     Observeses the specified property expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="autoSubscribe">if set to <c>true</c> [automatic subscribe].</param>
        /// <returns>
        ///     The Property Observer.
        /// </returns>
        [NotNull]
        public static ParameterObserver<TResult> Observes<TResult>(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            [NotNull] Action action,
            bool autoSubscribe = true)
        {
            var observer = new ParameterObserver<TResult>(propertyExpression, action);
            if (autoSubscribe)
            {
                observer.Subscribe(true);
            }

            return observer;
        }


        /// <summary>
        ///     Observeses the specified parameter1.
        /// </summary>
        /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="autoSubscribe">if set to <c>true</c> [automatic subscribe].</param>
        /// <returns>
        ///     The Property Observer.
        /// </returns>
        [NotNull]
        public static ParameterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action,
            bool autoSubscribe = true)
        {
            var observer = new ParameterObserver<TParameter1, TResult>(parameter1, propertyExpression, action);
            if (autoSubscribe)
            {
                observer.Subscribe(true);
            }

            return observer;
        }

    }
}