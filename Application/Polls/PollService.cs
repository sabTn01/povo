using POVO.Backend.Domain.Polls;
using POVO.Backend.Infrastructure.Extensions;

namespace POVO.Backend.Application.Polls
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        public PollService(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public Task<IQueryable<Poll>> List()
        {
            return _pollRepository.List();
        }

        public Task<Poll> GetByGuid(Guid guid)
        {
            return _pollRepository.GetByGuidAsync(guid);
        }

        public Task<Poll> SaveChanges(Poll poll)
        {
            ValidatePoll(poll);
            return InsertOrUpdate(poll);
        }

        private async Task<Poll> InsertOrUpdate(Poll poll)
        {
            if (poll.IsNewEntity())
                await _pollRepository.InsertAsync(poll);
            else
                await _pollRepository.UpdateAsync(poll);
            return poll;
        }

        private void ValidatePoll(Poll newPoll)
        {
            var validator =  new PollValidator();

            validator.ValidateAndThrowEntityValidation(newPoll);
        }

        public Task Delete(Poll poll)
        {
            return _pollRepository.DeleteAsync(poll);
        }
    }
}
