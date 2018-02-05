namespace Lifestyle.Shared
{
    using System;

    /// <summary>
    /// Static helper to guard agains null references.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws null reference exception if the object is null.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object to test for null reference.</param>
        /// <param name="paramName">Parameter name.</param>
        public static void ThrowIfNull<T>(T obj, string paramName)
            where T : class
        {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException(paramName);
        }
    }
}
