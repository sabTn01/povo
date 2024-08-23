using Microsoft.EntityFrameworkCore;
using POVO.Backend.Domains.Polls;
using POVO.Backend.Infrastructure.Contexts;
using POVO.Backend.Infrastructure.Exceptions;
using System;
using System.Drawing;

namespace POVO.Backend.Infrastructure.Repositories.Polls
{
    public class PollSqlRepository : BaseSqlRepository, IPollRepository
    {
        public PollSqlRepository(PovoDbContext context) : base(context)
        {
        }

        public async Task<Poll> GetByGuidAsync(Guid guid)
        {
            var poll = await DbContext.Polls
                .Where(p => !p.IsSoftDeleted)
                .Include(p => p.Options)
                .FirstOrDefaultAsync(s => s.Guid == guid);

            if (poll == null)
            {
                throw new EntityNotFoundException(typeof(Poll), guid);
            }

            return poll;
        }

        public async Task<Poll> GetByIdAsync(int id)
        {
            var poll = await DbContext.Polls
                  .Where(p => !p.IsSoftDeleted)
                  .Include(p => p.Options)
                  .FirstOrDefaultAsync(s => s.Id == id);

            if (poll == null)
            {
                throw new EntityNotFoundException(typeof(Poll), id);
            }

            return poll;
        }

       public Task<IQueryable<Poll>> List()
        {
            var pollCollections = DbContext.Polls
              .Where(p => !p.IsSoftDeleted)
              .Include(p => p.Options)
              .OrderBy(p => p.Title)
              .AsQueryable();

            return Task.FromResult(pollCollections);
        }

        public async Task InsertAsync(Poll entity)
        {
            await DbContext.Polls.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Poll entity)
        {
            DbContext.Polls.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Poll entity)
        {
            entity.IsSoftDeleted = true;

            DbContext.Polls.Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
