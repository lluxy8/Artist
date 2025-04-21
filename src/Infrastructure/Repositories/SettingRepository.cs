using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SettingRepository : BaseRepository<Setting>
    {
        public SettingRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }
    }
}
