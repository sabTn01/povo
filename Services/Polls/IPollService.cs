using POVO.Backend.Domains.Polls;
using POVO.Backend.Infrastructure.Dtos.Polls;

namespace POVO.Backend.Services.Polls
{
    public interface IPollService
    {
        public Task<Poll> CreatePoll(PollCreateUpdateInput request);
        public Task<IQueryable<Poll>> List();
        public Task<Poll> GetByGuid(Guid guid);
        public Task<Poll> SaveChanges(Poll poll);
        public Task Delete(Poll poll);
    }
}
