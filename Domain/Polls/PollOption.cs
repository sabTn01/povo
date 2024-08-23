namespace POVO.Backend.Domain.Polls
{
    public class PollOption : BaseModel
    {
        public string OptionText { get; set; }
        public int PollId { get; set; }

        public Poll Poll { get; set; }
    }
}
