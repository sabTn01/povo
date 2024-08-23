using POVO.Backend.Infrastructure.Repositories;

namespace POVO.Backend.Domain.Polls
{
    public interface IPollRepository : IRepository<Poll>
    {
        Task<IQueryable<Poll>> List();
    }
}
