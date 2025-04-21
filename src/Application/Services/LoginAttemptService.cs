using Application.Abstract;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public sealed class LoginAttemptService : BaseService<LoginAttempt>
    {
        public LoginAttemptService(IRepository<LoginAttempt> repository) : base(repository)
        {
        }
    }
}
