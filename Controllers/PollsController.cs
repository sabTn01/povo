using Microsoft.AspNetCore.Mvc;
using POVO.Backend.Application.Polls;
using AutoMapper;
using POVO.Backend.Infrastructure.Dtos.Polls;
using Microsoft.AspNetCore.JsonPatch;
using POVO.Backend.Infrastructure.Exceptions;
using POVO.Backend.Domain.Polls;

namespace POVO.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        private readonly IMapper _mapper;
        public PollsController(IPollService pollService, IMapper mapper)
        {
            _pollService = pollService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<PollViewOutput>> Get()
        {
            var polls = await _pollService.List();

            var mapped = _mapper.Map(polls, new List<PollViewOutput>());
            return mapped;
        }

        [HttpGet("{guid:Guid}")]
        public async Task<PollViewOutput> Get(Guid guid)
        {
            var poll = await _pollService.GetByGuid(guid);

            var mapped = _mapper.Map(poll, new PollViewOutput());

            return mapped;
        }

        [HttpPost]
        public async Task<PollViewOutput> Create([FromBody] PollCreateUpdateInput request)
        {
            var newPoll = await _pollService.CreatePoll(request);

            var mapped = _mapper.Map(newPoll, new PollViewOutput());
            return mapped;
        }

        [HttpPut("{guid:Guid}")]
        public async Task<PollViewOutput> Update(Guid guid, [FromBody] PollCreateUpdateInput request)
        {
            var poll = await _pollService.GetByGuid(guid);

            if (poll == null) throw new EntityNotFoundException(typeof(Poll), guid);

            var mappedInput = _mapper.Map(request, poll);

            var updated = await _pollService.SaveChanges(mappedInput);

            var mapped = _mapper.Map(updated, new PollViewOutput());

            return mapped;
        }

        [HttpDelete("{guid:Guid}")]
        public async Task Delete(Guid guid)
        {
            var poll = await _pollService.GetByGuid(guid);
            await _pollService.Delete(poll);
        }
    }
}
