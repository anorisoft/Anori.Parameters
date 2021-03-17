// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Anori.Common;

namespace Anori.Parameters
{
    /// <summary>
    ///     The I Read Only Parameter interface.
    /// </summary>
    public interface IReadOnlyParameter
    {
        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        event EventHandler<EventArgs<object>> ValueChanged;

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        object? Value { get; }
    }
}