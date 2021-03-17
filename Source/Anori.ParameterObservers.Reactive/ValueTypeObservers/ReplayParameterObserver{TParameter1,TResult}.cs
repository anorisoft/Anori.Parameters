// -----------------------------------------------------------------------
// <copyright file="ReplayParameterObserver.cs" company="AnoriSoft">
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

    using JetBrains.Annotations;

    /// <summary>
    /// The Replay Parameter Observer class.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="ReplayParameterObserver{TResult}" />
    public sealed class ReplayParameterObserver<TParameter1, TResult> : ParameterObserverBase<ReplayParameterObserver<TParameter1, TResult>, TParameter1, TResult>, IObservable<TResult?>
        where TResult : struct
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        [NotNull]
        private readonly Func<TResult?> propertyGetter;

        /// <summary>
        /// The subject
        /// </summary>
        [NotNull]
        private readonly SubjectBase<TResult?> subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TParameter1, TResult}" /> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal ReplayParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateValueGetter(propertyExpression)(parameter1);
            this.subject = new ReplaySubject<TResult?>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TParameter1, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        internal ReplayParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            int bufferSize)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateValueGetter(propertyExpression)(parameter1);
            this.subject = new ReplaySubject<TResult?>(bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TParameter1, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            int bufferSize,
            TimeSpan window)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateValueGetter(propertyExpression)(parameter1);
            this.subject = new ReplaySubject<TResult?>(bufferSize, window);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TParameter1, TResult}"/> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            TimeSpan window)
            : base(parameter1, propertyExpression)
        {
            this.propertyGetter = () => ExpressionObservers.ExpressionGetter.CreateValueGetter(propertyExpression)(parameter1);
            this.subject = new ReplaySubject<TResult?>(window);
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

        /// <summary>
        /// Properties the getter.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        private static TResult? PropertyGetter([NotNull] Func<TResult> propertyExpression)
        {
            try
            {
                return propertyExpression();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Properties the getter.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <returns></returns>
        private TResult? PropertyGetter([NotNull] Func<TParameter1, TResult> propertyExpression, TParameter1 parameter1)
        {
            try
            {
                return propertyExpression(parameter1);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}