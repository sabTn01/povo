namespace POVO.Backend.Domains.Polls
{
    public class Poll : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
    }
}
