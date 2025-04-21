using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LogRepository : BaseRepository<Log>
    {
        public LogRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }
    }
}
