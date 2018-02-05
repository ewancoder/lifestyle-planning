namespace Lifestyle.Shared
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Identity.
    /// </summary>
    /// <typeparam name="TValue">Identity value type.</typeparam>
#pragma warning disable S4035 // Classes implementing "IEquatable<T>" should be sealed
    // It's ok to implement IEquatable<T> because Equals is sealed and should not be overridden.
    public abstract class Identity<TValue> : IEquatable<Identity<TValue>>
#pragma warning restore S4035 // Classes implementing "IEquatable<T>" should be sealed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identity{TValue}"/>
        /// class.
        /// </summary>
        /// <param name="value">Identity value.</param>
        protected Identity(TValue value)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, default(TValue)))
                throw new ArgumentException("Identity can't have default value.");

            Value = value;
        }

        /// <summary>
        /// Identity value.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Gets identity value's hash code.
        /// </summary>
        /// <returns>Identity value's hash code.</returns>
        public sealed override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Tests equality between values of identities.
        /// </summary>
        /// <param name="other">Other identity.</param>
        /// <returns>Result of the equality check.</returns>
        public bool Equals(Identity<TValue> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            // TODO: test performance.
            if (GetType() != other.GetType())
                return false;

            return IsValueEqual(other.Value);
        }

        /// <summary>
        /// Tests equality between values of identities.
        /// </summary>
        /// <param name="obj">Other identity.</param>
        /// <returns>Result of the equality check.</returns>
        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (ReferenceEquals(obj, this))
                return true;

            // TODO: test performance.
            if (GetType() != obj.GetType())
                return false;

            return IsValueEqual(((Identity<TValue>)obj).Value);
        }

        /// <summary>
        /// Gets string representation of identity value.
        /// </summary>
        /// <returns>Identity value.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Checks if value of identity is equal to supplied value.
        /// </summary>
        /// <param name="value">Value to check equality for.</param>
        /// <returns>Result of the equality check.</returns>
        private bool IsValueEqual(TValue value)
        {
            return Value.Equals(value);
        }
    }
}
