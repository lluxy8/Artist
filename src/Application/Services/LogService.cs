using Application.Abstract;
using Core.Interfaces.Service;
using Core.Entities;
using Core.Interfaces.Repository;

namespace Application.Services
{
    public sealed class LogService : BaseService<Log>, ILogService
    {
        public LogService(IRepository<Log> repository) : base(repository)
        {
        }
    }
}
