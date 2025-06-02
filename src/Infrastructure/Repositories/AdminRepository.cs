using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(IDbContextFactory<AppDbContext> context) : base(context) { }

        public async Task<Admin?> FindAdminAsync(string username, string password)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await context.Admins
                .Where(x => x.Name == username && x.PasswordHash == password)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Admin?> GetByUserNameAsync(string username)
        {
            await using var context = await _contextfactory.CreateDbContextAsync();
            return await context.Admins
                    .Where(x => x.Name == username)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }
    }
}
