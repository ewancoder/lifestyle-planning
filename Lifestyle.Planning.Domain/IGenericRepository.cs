namespace Lifestyle.Planning.Domain
{
    using Shared;

    public interface IGenericRepository<TModel, TId>
        where TModel : AggregateRoot<TId>
    {
        TId GetNextIdentity();
        TModel FindById(TId id);
        void Save(TModel aggregateRoot);
    }
}
