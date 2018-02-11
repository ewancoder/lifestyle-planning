namespace Lifestyle.Planning.Domain
{
    using Shared;

    public interface IGenericRepository<TAggregateRoot, TId>
        where TAggregateRoot : AggregateRoot<TId>
    {
        TId GetNextIdentity();
        TAggregateRoot FindById(TId id);
        void Save(TAggregateRoot aggregateRoot);
    }
}
