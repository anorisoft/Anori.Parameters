// -----------------------------------------------------------------------
// <copyright file="ReplayObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ValueTypeObservers
{
    using System;
    using System.Linq.Expressions;

    public static class ReplayObserverFactory
    {
        public static ReplayParameterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            TParameter1 parameter1,
            Expression<Func<TParameter1, TResult>> propertyExpression,
            bool isAutoSubscribe = true) where TResult : struct
        {
            var observer = new ReplayParameterObserver<TParameter1, TResult>(parameter1, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe(silent: true);
            }

            return observer;
        }

        public static ReplayParameterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            TParameter1 parameter1,
            Expression<Func<TParameter1, TResult>> propertyExpression, TimeSpan window,
            bool isAutoSubscribe = true) where TResult : struct
        {
            var observer = new ReplayParameterObserver<TParameter1, TResult>(parameter1, propertyExpression, window);
            if (isAutoSubscribe)
            {
                observer.Subscribe(silent: true);
            }

            return observer;
        }

        public static ReplayParameterObserver<TParameter1, TResult> Observes<TParameter1, TResult>(
            TParameter1 parameter1,
            Expression<Func<TParameter1, TResult>> propertyExpression, int bufferSize,
            bool isAutoSubscribe = true) where TResult : struct
        {
            var observer = new ReplayParameterObserver<TParameter1, TResult>(parameter1, propertyExpression, bufferSize);
            if (isAutoSubscribe)
            {
                observer.Subscribe(silent: true);
            }

            return observer;
        }

        public static ReplayParameterObserver<TResult> Observes<TResult>(
            Expression<Func<TResult>> propertyExpression,
            bool isAutoSubscribe = true) where TResult : struct
        {
            var observer = new ReplayParameterObserver<TResult>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe(silent: true);
            }

            return observer;
        }
    }
}