// -----------------------------------------------------------------------
// <copyright file="BehaviorObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ReferenceTypeObservers
{
    using System;
    using System.Linq.Expressions;

    public static class BehaviorObserverFactory
    {
        public static BehaviorParameterObserver<TParameter1,TResult> Observes< TParameter1, TResult>(
            TParameter1 parameter1,
            Expression<Func<TParameter1, TResult>> propertyExpression,
            bool isAutoSubscribe = true)
        where TResult : class
        {
            var observer = new BehaviorParameterObserver<TParameter1, TResult>(parameter1, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }

        public static BehaviorParameterObserver<TResult> Observes<TResult>(
            Expression<Func<TResult>> propertyExpression,
            bool isAutoSubscribe = true)
            where TResult : class

        {
            var observer = new BehaviorParameterObserver<TResult>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }
    }
}