namespace Lifestyle.Shared
{
    using System;

    /// <summary>
    /// Entity.
    /// </summary>
    /// <typeparam name="TId">Identity type.</typeparam>
#pragma warning disable S4035 // Classes implementing "IEquatable<T>" should be sealed
    // It's ok to implement IEquatable<T> because Equals is sealed and should not be overridden.
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
#pragma warning restore S4035 // Classes implementing "IEquatable<T>" should be sealed
    {
        /// <summary>
        /// Gets entity identity.
        /// </summary>
        /// <returns>Entity identity.</returns>
        protected abstract TId GetIdentity();

        /// <summary>
        /// Gets entity identity's hash code.
        /// </summary>
        /// <returns>Entity identity's hash code.</returns>
        public sealed override int GetHashCode()
        {
            return GetIdentity().GetHashCode();
        }

        /// <summary>
        /// Tests equality between identities of entities.
        /// </summary>
        /// <param name="other">Other entity.</param>
        /// <returns>Result of the equality check.</returns>
        public bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            // TODO: test performance.
            if (GetType() != other.GetType())
                return false;

            return IsIdentityEqual(other.GetIdentity());
        }

        /// <summary>
        /// Tests equality between identities of entities. Also checks that both
        /// entities are the same type.
        /// </summary>
        /// <param name="obj">Other entity.</param>
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

            return IsIdentityEqual(((Entity<TId>)obj).GetIdentity());
        }

        /// <summary>
        /// Checks if identity of entity is equal to supplied value.
        /// </summary>
        /// <param name="id">Identity to check equality for.</param>
        /// <returns>Result of the equality check.</returns>
        private bool IsIdentityEqual(TId id)
        {
            return GetIdentity().Equals(id);
        }
    }
}
