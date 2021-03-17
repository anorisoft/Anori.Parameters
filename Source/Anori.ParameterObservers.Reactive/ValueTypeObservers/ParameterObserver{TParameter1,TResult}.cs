// -----------------------------------------------------------------------
// <copyright file="ParameterObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ValueTypeObservers
{
    using System;
    using System.Linq.Expressions;
    using System.Reactive.Subjects;

    using Anori.ParameterObservers;
    using Anori.ParameterObservers.Base;
    using Anori.ParameterObservers.Observers;

    using JetBrains.Annotations;

    /// <summary>
    /// The Parameter Observer class.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the owner.</typeparam>
    /// <typeparam name="TResult">The type of the value.</typeparam>
    /// <seealso cref="Observers.ParameterObserver{TResult}" />
    /// <seealso cref="System.IObservable{TResult}" />
    public sealed class ParameterObserver< TParameter1, TResult> : ParameterObserverBase<ParameterObserver<TParameter1, TResult>, TParameter1, TResult>, IObservable<TResult?>
        where TResult : struct

    {
        /// <summary>
        /// The property propertyGetter
        /// </summary>
        private readonly Func<TResult?> propertyGetter;

        /// <summary>
        ///     The subject
        /// </summary>
        private readonly SubjectBase<TResult?> subject;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver{TResult}" /> class.
        /// </summary>
        /// <param name="parameter1">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal ParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateValueGetter(propertyExpression)(parameter1);
            this.subject = new Subject<TResult?>();
        }

        /// <summary>
        ///     Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has finished
        ///     sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<TResult?> observer) => this.subject.Subscribe(observer);

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void OnAction() => this.subject.OnNext(this.propertyGetter());

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.subject.Dispose();
            }
        }
    }
}