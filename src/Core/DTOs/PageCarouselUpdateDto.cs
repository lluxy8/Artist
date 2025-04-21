using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PageCarouselUpdateDto
    {
        public Guid Id { get; set; }
        public IFormFile? Image { get; set; }
        public required string ImageUrl { get; set; } 
        public required string Text1 { get; set; } 
        public required string Text2 { get; set; }
        public required string Text3 { get; set; } 
        public Guid PageContentId { get; set; }
        public required PageContent PageContent { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
