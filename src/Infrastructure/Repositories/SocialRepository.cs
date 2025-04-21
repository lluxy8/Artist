using Core.Entities;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SocialRepository : BaseRepository<Social>
    {
        public SocialRepository(IDbContextFactory<AppDbContext> context) : base(context)
        {
        }
    }
}
