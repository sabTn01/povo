using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using POVO.Backend.Domains.Polls;
using POVO.Backend.Infrastructure.Dtos.Polls;

namespace POVO.Backend.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            PollMap();
            PollOptionMap();
        }

        private void PollMap()
        {
            CreateMap<PollCreateUpdateInput, Poll>();
            CreateMap<Poll, PollViewOutput>().ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
            CreateMap<PollOptionUpdateInput, PollOption>();
        }

        private void PollOptionMap()
        {
            CreateMap<PollOption, PollOptionViewOutput>();
        }
    }
}
