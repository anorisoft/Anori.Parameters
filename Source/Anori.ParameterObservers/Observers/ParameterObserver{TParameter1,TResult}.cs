// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TParameter1,TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.Observers
{
    using System;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers.Observers;
    using Anori.ParameterObservers.Base;

    using JetBrains.Annotations;

    /// <summary>
    /// Property Observer.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="ParameterObserver{TResult}" />
    public sealed class
        ParameterObserver<TParameter1, TResult> : ParameterObserverBase<ParameterObserver<TParameter1, TResult>,
            TParameter1, TResult>
    {
        /// <summary>
        ///     The action.
        /// </summary>
        [NotNull]
        private readonly Action action;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TParameter1, TResult}" /> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">The action is null.</exception>
        internal ParameterObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression,
            [NotNull] Action action)
            : base(parameter1, propertyExpression) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        ///     The action.
        /// </summary>
        protected override void OnAction() => this.action();
    }
}