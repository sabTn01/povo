using Microsoft.EntityFrameworkCore;
using POVO.Backend.Infrastructure.Contexts;

namespace POVO.Backend.Infrastructure.Repositories
{
    public abstract class BaseSqlRepository
    {

        protected readonly PovoDbContext DbContext;
        protected BaseSqlRepository(PovoDbContext contextFactory)
        {
            DbContext = contextFactory;
        }
    }
}
