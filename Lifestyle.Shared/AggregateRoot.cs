namespace Lifestyle.Shared
{
    /// <summary>
    /// Aggregate root.
    /// </summary>
    /// <typeparam name="TId">Aggregate root entity identity.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
    }
}
