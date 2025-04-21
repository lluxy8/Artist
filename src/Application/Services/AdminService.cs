using Core.Entities;
using Core.Interfaces;
using Application.Abstract;

namespace Application.Services
{
    public sealed class AdminService : BaseService<Admin>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IService<LoginAttempt> _laService; 
        public AdminService(
            IRepository<Admin> repository, 
            IAdminRepository adminRepository,
            IService<LoginAttempt> laservice) 
            : base(repository)
        {
            _adminRepository = adminRepository;
            _laService = laservice;
        }

        public async Task<bool> LoginAsync(string username, string password, string ip)
        {
            try
            {
                if(await _adminRepository.FindAdminAsync(username, password))
                {
                    var LA = new LoginAttempt
                    {
                        Ip = ip,
                        Success = true,
                        AdminId = (await _adminRepository.GetByUserNameAsync(username)).Id
                    };

                    await _laService.AddAsync(LA);

                    return true;
                }
                else
                {
                    var LA = new LoginAttempt
                    {
                        Ip = ip,
                        Success = false,
                        AdminId = (await _adminRepository.GetByUserNameAsync(username)).Id
                    };

                    await _laService.AddAsync(LA);

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting login.", ex);
            }
        }
    }
}
