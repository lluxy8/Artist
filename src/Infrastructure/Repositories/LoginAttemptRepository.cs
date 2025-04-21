using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoginAttemptRepository : BaseRepository<LoginAttempt>
    {
        public LoginAttemptRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }

        protected override IQueryable<LoginAttempt> IncludeRelatedEntities(IQueryable<LoginAttempt> query)
        {
            return query
                .Include(p => p.Admin);
        }
    }
}
