namespace POVO.Backend.Domain
{
    public class BaseModel
    {
        public int Id { get; set; }

        public Guid Guid { get; set; } = Guid.NewGuid();

        public bool IsSoftDeleted { get; set; }

        public DateTime CreatedAtClient { get; set; }
        public DateTime UpdatedAtClient { get; set; }
        public DateTime CreatedAtServer { get; set; }
        public DateTime UpdatedAtServer { get; set; }

        public bool IsNewEntity()
        {
            return Id == 0;
        }
    }
}
