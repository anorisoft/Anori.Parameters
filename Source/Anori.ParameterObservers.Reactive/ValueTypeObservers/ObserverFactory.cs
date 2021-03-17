// -----------------------------------------------------------------------
// <copyright file="ObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ValueTypeObservers
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// The Observer Factory class.
    /// </summary>
    public static class ObserverFactory
    {
        /// <summary>
        /// Observeses the specified parameter1.
        /// </summary>
        /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            TParameter1 parameter1,
            Expression<Func<TParameter1, TResult>> propertyExpression,
            bool isAutoSubscribe = true)
            where TResult : struct
        {
            var observer = new ParameterObserver<TParameter1, TResult>(parameter1, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }

        /// <summary>
        /// Observeses the specified property expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver<TResult> Observes<TResult>(
            Expression<Func<TResult>> propertyExpression,
            bool isAutoSubscribe = true)
            where TResult : struct

        {
            var observer = new ParameterObserver<TResult>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }
    }
}