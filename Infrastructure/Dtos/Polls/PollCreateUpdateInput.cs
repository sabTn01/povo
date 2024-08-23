using POVO.Backend.Domains.Polls;

namespace POVO.Backend.Infrastructure.Dtos.Polls
{
    public class PollCreateUpdateInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ICollection<PollOptionUpdateInput> Options { get; set; }
    }
}
