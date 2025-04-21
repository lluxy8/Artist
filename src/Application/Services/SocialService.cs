using Application.Abstract;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public sealed class SocialService : BaseService<Social>
    {
        public SocialService(IRepository<Social> repository) : base(repository)
        {
        }
    }
}
