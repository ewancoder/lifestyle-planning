namespace Lifestyle.Shared
{
    using System;

    /// <summary>
    /// Primitive.
    /// </summary>
    /// <typeparam name="TValue">Primitive value type.</typeparam>
#pragma warning disable S4035 // Classes implementing "IEquatable<T>" should be sealed
    // It's ok to implement IEquatable<T> because Equals is sealed and should not be overridden.
    public abstract class Primitive<TValue> : IEquatable<Primitive<TValue>>
#pragma warning restore S4035 // Classes implementing "IEquatable<T>" should be sealed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive{TValue}"/>
        /// class.
        /// </summary>
        /// <param name="value">Primitive value.</param>
        protected Primitive(TValue value)
        {
            if (value == null) // TODO: Check that this doesn't lead to boxing.
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        /// <summary>
        /// Primitive value.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Gets primitive value's hash code.
        /// </summary>
        /// <returns>Primitive value's hash code.</returns>
        public sealed override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Tests equality between values of primitives.
        /// </summary>
        /// <param name="other">Other primitive.</param>
        /// <returns>Result of the equality check.</returns>
        public bool Equals(Primitive<TValue> other)
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
        /// Tests equality between values of primitives.
        /// </summary>
        /// <param name="obj">Other primitive.</param>
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

            return IsValueEqual(((Primitive<TValue>)obj).Value);
        }

        /// <summary>
        /// Gets string representation of primitive value.
        /// </summary>
        /// <returns>Primitive value.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Checks if value of primitive is equal to supplied value.
        /// </summary>
        /// <param name="value">Value to check equality for.</param>
        /// <returns>Result of the equality check.</returns>
        private bool IsValueEqual(TValue value)
        {
            return Value.Equals(value);
        }
    }
}
