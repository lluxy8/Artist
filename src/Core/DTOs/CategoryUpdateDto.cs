using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CategoryUpdateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IFormFile? Image { get; set; }
        public required string UrlName { get; set; }
        public required string DisplayName { get; set; }
        public required string ImageUrl { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsHighlighted { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
