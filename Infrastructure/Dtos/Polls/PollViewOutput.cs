namespace POVO.Backend.Infrastructure.Dtos.Polls
{
    public class PollViewOutput
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ICollection<PollOptionViewOutput> Options { get; set; }
        public DateTime CreatedAtClient { get; set; }
    }
}
