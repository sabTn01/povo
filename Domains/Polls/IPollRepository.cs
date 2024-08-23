using POVO.Backend.Infrastructure.Repositories;

namespace POVO.Backend.Domains.Polls
{
    public interface IPollRepository : IRepository<Poll>
    {
        Task<IQueryable<Poll>> List();
    }
}
