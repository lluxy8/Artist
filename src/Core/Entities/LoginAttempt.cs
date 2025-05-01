using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LoginAttempt : BaseEntity
    {
        public required string Ip { get; set; }
        public required string Location { get; set; }
        public bool Success { get; set; }
    }
}
