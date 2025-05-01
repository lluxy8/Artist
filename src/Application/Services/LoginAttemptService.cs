using Application.Abstract;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public sealed class LoginAttemptService : BaseService<LoginAttempt>, ILoginAttemptService
    {
        public LoginAttemptService(IRepository<LoginAttempt> repository) : base(repository)
        {
        }
    }
}
