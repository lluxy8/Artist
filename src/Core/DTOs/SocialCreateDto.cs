using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SocialCreateDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required IFormFile Image { get; set; }
    }
}
