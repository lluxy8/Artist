using Application.Abstract;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public sealed class LogService : BaseService<Log>
    {
        public LogService(IRepository<Log> repository) : base(repository)
        {
        }
    }
}
