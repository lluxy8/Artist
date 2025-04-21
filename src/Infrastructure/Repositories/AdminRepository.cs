using Core.Entities;
using Core.Interfaces;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(IDbContextFactory<AppDbContext> context) : base(context) { }

        public async Task<bool> FindAdminAsync(string username, string password)
        {
            using var context = _contextfactory.CreateDbContext();
            return await context.Admins
                .AsNoTracking()
                .AnyAsync(x => x.Name == username && x.PasswordHash == password);
        }

        public async Task<Admin?> GetByUserNameAsync(string username)
        {
            using var context = _contextfactory.CreateDbContext();
            return await context.Admins
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == username);
        }
    }
}
