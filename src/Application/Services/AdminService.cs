using Core.Entities;
using Application.Abstract;
using Core.DTOs;
using Core.Common.Helpers;
using Application.Events;
using Microsoft.AspNetCore.Components;
using Core.Common.Dispatchers;
using Core.Common.Enums;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Core.Common.Models;

namespace Application.Services
{
    public sealed class AdminService : BaseService<Admin> , IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IService<LoginAttempt> _laService;
        private readonly EventDispatcher _dispatcher;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(
            IRepository<Admin> repository, 
            IAdminRepository adminRepository,
            IService<LoginAttempt> laservice,
            EventDispatcher dispatcher,
            IAuthenticationManager authenticationManager,
            IHttpContextAccessor contextAccessor) 
            : base(repository)
        {
            _adminRepository = adminRepository;
            _laService = laservice;
            _dispatcher = dispatcher;
            _authenticationManager = authenticationManager;
            _httpContextAccessor = contextAccessor;
        }

        public async Task AddAsync(AdminCreateDto dto)
        {
            var admin = new Admin
            {
                Name = dto.Name,
                PasswordHash = BCryptHelper.HashPassword(dto.Password)
            };

            await _adminRepository.AddAsync(admin);

            await _dispatcher.DispatchAsync(new LogEvent(
                dto.IpAdress, "Yeni yönetici hesabı oluşturuldu.", LogType.Authorize));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Yönetici hesabı bulunamadı."); ;

            await _repository.DeleteAsync(existingEntity);
            await _dispatcher.DispatchAsync(new LogEvent(_authenticationManager.GetUser().Name,
                $"{existingEntity.Name} Adlı Yönetici Hesabı Silindi.", LogType.Delete));

        }


        public async Task UpdateAsync(AdminUpdateDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Yönetici hesabı bulunamadı.");

            var admin = new Admin
            {
                Id = dto.Id,
                Name = dto.Name,
                PasswordHash = BCryptHelper.HashPassword(dto.Password),
                UpdateDate = DateTime.UtcNow,
                CreateDate = existingEntity.CreateDate
            };

            await _repository.UpdateAsync(admin);

            await _dispatcher.DispatchAsync(new LogEvent(dto.IpAdress,
                $"{existingEntity.Name} Adlı Yönetici Hesabı Düzenlendi.", LogType.Authorize));
        }

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var account = await _adminRepository.GetByUserNameAsync(dto.Username);
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
            var LA = new LoginAttempt
            {
                Ip = ip,
                Location = await IpHelper.GetLocationFromIpAsync(ip)
            };

            if (account != null && BCryptHelper.VerifyPassword(dto.Password, account.PasswordHash))
            {
                await _authenticationManager.SignInAsync(new UserAuthModel
                {
                    Id = account.Id,
                    Name = account.Name
                });

                LA.Success = true;
                await _laService.AddAsync(LA);

                return true;
            }

            LA.Success = false;
            await _laService.AddAsync(LA);

            await Task.Delay(500);
            return false;
        }
    }
}
