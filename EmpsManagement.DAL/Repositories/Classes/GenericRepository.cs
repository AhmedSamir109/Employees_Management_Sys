using EmpsManagement.DAL.Repositories.Interfaces;

namespace EmpsManagement.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return _dbContext.Set<TEntity>().ToList();
            }
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, bool WithTracking = false)
        {
            return _dbContext.Set<TEntity>()
                .Where(predicate)
                .ToList();
        }
        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
           

        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            
        }

    }
}
