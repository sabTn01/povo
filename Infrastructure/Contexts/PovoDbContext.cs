using Microsoft.EntityFrameworkCore;
using POVO.Backend.Domain.Polls;

namespace POVO.Backend.Infrastructure.Contexts
{
    public class PovoDbContext: BaseDbContext
    {
        public PovoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
    }
}
