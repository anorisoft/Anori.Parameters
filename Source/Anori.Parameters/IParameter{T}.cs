// -----------------------------------------------------------------------
// <copyright file="IParameter{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Parameters
{
    /// <summary>
    /// The I Parameter interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anori.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anori.Parameters.IParameter" />
    /// <seealso cref="IReadOnlyParameter{T}" />
    /// <seealso cref="IParameter" />
    public interface IParameter<T> : IReadOnlyParameter<T>, IParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new T Value { get; set; }
    }
}