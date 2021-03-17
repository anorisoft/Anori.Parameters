// -----------------------------------------------------------------------
// <copyright file="BehaviorParameterObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ReferenceTypeObservers
{
    using System;
    using System.Linq.Expressions;
    using System.Reactive.Subjects;

    using Anori.ParameterObservers.Base;
    using Anori.ParameterObservers.Observers;

    using JetBrains.Annotations;

    /// <summary>
    /// The Behavior Parameter Observer class.
    /// </summary>
    /// <typeparam name="TResult">The type of the value.</typeparam>
    /// <seealso cref="Observers.ParameterObserver{TResult}" />
    /// <seealso cref="System.IObservable{TResult}" />
    public sealed class BehaviorParameterObserver<TResult> : ParameterObserverBase<BehaviorParameterObserver<TResult>, TResult>, IObservable<TResult?>
        where TResult : class

    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        private readonly Func<TResult?> propertyGetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorParameterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal BehaviorParameterObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression)
            : base(propertyExpression)
        {
            this.propertyGetter = ExpressionObservers.ExpressionGetter.CreateReferenceGetter(propertyExpression);
            this.subject = new BehaviorSubject<TResult?>(this.propertyGetter());
        }

        /// <summary>
        /// The subject
        /// </summary>
        private readonly SubjectBase<TResult?> subject;

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void OnAction() => this.subject.OnNext(this.propertyGetter());

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<TResult?> observer) => this.subject.Subscribe(observer);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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