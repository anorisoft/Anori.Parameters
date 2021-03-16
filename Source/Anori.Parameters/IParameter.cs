// -----------------------------------------------------------------------
// <copyright file="IParameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Parameters
{
    /// <summary>
    /// </summary>
    /// <seealso cref="IReadOnlyParameter{T}" />
    /// <seealso cref="IParameter" />
    public interface IParameter : IReadOnlyParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new object Value { get; set; }
    }
}