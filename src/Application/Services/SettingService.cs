using Application.Abstract;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class SettingService : BaseService<Setting>
    {
        public SettingService(IRepository<Setting> repository) : base(repository)
        {
        }
    }
}
