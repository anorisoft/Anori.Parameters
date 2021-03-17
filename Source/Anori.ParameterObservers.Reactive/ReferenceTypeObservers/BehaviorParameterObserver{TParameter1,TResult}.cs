// -----------------------------------------------------------------------
// <copyright file="ObservableParameterObserver.cs" company="AnoriSoft">
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
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Observers.ParameterObserver{TResult}" />
    /// <seealso cref="System.IObservable{TResult}" />
    public sealed class BehaviorParameterObserver<TParameter1, TResult> 
        : ParameterObserverBase<BehaviorParameterObserver<TParameter1, TResult>, TParameter1, TResult>, 
          IObservable<TResult?>
    where TResult : class
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        private readonly Func<TResult?> propertyGetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviorParameterObserver{TParameter1, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal BehaviorParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateReferenceGetter(propertyExpression)(parameter1);
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