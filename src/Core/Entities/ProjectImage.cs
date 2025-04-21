using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProjectImage : BaseEntity
    {
        public required string Url { get; set; }
        public bool IsMainImage { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

    }
}
