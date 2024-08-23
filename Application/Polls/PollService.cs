using POVO.Backend.Domain.Polls;
using POVO.Backend.Infrastructure.Dtos.Polls;

namespace POVO.Backend.Application.Polls
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        public PollService(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<Poll> CreatePoll(PollCreateUpdateInput request)
        {
            var validRequest = await ValidatePoll(request);

            if (!validRequest) throw new Exception("Not Valid reuqest");

            var poll = new Poll
            {
                Title = request.Title,
                Description = request.Description,
                ExpiryDate = request.ExpiryDate,
                Options = request.Options.Select(o => new PollOption { OptionText = o.OptionText }).ToList()
            };

            var response = await SaveChanges(poll);

            return response;
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

        private Task<bool> ValidatePoll(PollCreateUpdateInput request)
        {
            var valid = true;
            if (string.IsNullOrWhiteSpace(request.Title) || 
                string.IsNullOrWhiteSpace(request.Description) || 
                !(request.ExpiryDate != DateTime.MinValue) || 
                request.Options.Count == 0)
            {
                valid = false;
            }

            return Task.FromResult(valid);
        }

        public Task Delete(Poll poll)
        {
            return _pollRepository.DeleteAsync(poll);
        }
    }
}
