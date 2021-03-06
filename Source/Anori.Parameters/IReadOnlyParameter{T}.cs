// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Parameters
{
    using System;

    using Anori.Common;

    /// <summary>
    /// The I Read Only Parameter interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IReadOnlyParameter" />
    public interface IReadOnlyParameter<T> : IReadOnlyParameter
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        new T? Value { get; }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        new event EventHandler<EventArgs<T?>>? ValueChanged;
    }
}