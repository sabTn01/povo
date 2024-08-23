using POVO.Backend.Domains;

namespace POVO.Backend.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByGuidAsync(Guid guid);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
