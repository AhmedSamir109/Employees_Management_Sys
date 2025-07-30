using EmpsManagement.DAL.Models.Shared;

namespace EmpsManagement.DAL.Repositories.Interfaces
{
    public interface IGenericRepository <TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(bool WithTracking = false);

        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, bool WithTracking = false);
        TEntity? GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
