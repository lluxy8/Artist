using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IProjectImageRepository : IRepository<ProjectImage>
    {
        Task<bool> CheckMainImage(Guid ProjectId);
    }
}
