// -----------------------------------------------------------------------
// <copyright file="ReplayParameterObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Reactive.ReferenceTypeObservers
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
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="ReplayParameterObserver{TResult}" />
    public sealed class ReplayParameterObserver<TResult> : ParameterObserverBase<ReplayParameterObserver<TResult>, TResult>, IObservable<TResult?>
        where TResult : class
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        [NotNull]
        private readonly Func<TResult?> propertyGetter;

        /// <summary>
        ///     The subject
        /// </summary>
        [NotNull]
        private readonly SubjectBase<TResult?> subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action
        /// or
        /// propertyGetter</exception>
        internal ReplayParameterObserver([NotNull] Expression<Func<TResult>> propertyExpression)
            : base(propertyExpression)
        {
            this.propertyGetter = ExpressionObservers.ExpressionGetter.CreateReferenceGetter(propertyExpression);
            this.subject = new ReplaySubject<TResult?>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        internal ReplayParameterObserver([NotNull] Expression<Func<TResult>> propertyExpression, int bufferSize)
            : base(propertyExpression)
        {
            this.propertyGetter = ExpressionObservers.ExpressionGetter.CreateReferenceGetter(propertyExpression);
            this.subject = new ReplaySubject<TResult?>(bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver(
            [NotNull] Expression<Func<TResult>> propertyExpression,
            int bufferSize,
            TimeSpan window)
            : base(propertyExpression)
        {
            this.propertyGetter = () => PropertyGetter(propertyExpression.Compile());
            this.subject = new ReplaySubject<TResult?>(bufferSize, window);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayParameterObserver{TResult}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver([NotNull] Expression<Func<TResult>> propertyExpression, TimeSpan window)
            : base(propertyExpression)
        {
            this.propertyGetter = ExpressionObservers.ExpressionGetter.CreateReferenceGetter(propertyExpression);
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
        ///     Properties the getter.
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
    }
}