using ImageMagick;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;

namespace Core.Common.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile file, string folder, HttpRequest request, bool videoSupport = false)
        {
            ThrowIfFileIsEmpty(file);

            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".webp" };
            if (videoSupport)
            {
                allowedExtensions.Add(".mp4");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            ThrowIfExtensionNotAllowed(file, allowedExtensions);

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var relativePath = Path.Combine("uploads", folder, uniqueFileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            var fullUrl = $"{baseUrl}/{relativePath.Replace("\\", "/")}";

            return fullUrl;
        }

        public static async Task SaveIconAsync(IFormFile file)
        {
            ThrowIfFileIsEmpty(file);

            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".webp", ".ico" };
            ThrowIfExtensionNotAllowed(file, allowedExtensions);

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "favicon.ico");

            if (fileExtension == ".ico")
            {
                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            else
            {
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var image = new MagickImage(memoryStream);

                image.Format = MagickFormat.Ico;
                image.Resize(64, 64); 

                await image.WriteAsync(filePath); 
            }
        }



    private static void ThrowIfFileIsEmpty(IFormFile? file)
        {
            if(file is null || file.Length == 0)
                throw new Exception("Dosya boş olamaz.");
        }

        private static void ThrowIfExtensionNotAllowed(IFormFile file, string allowedExtension)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension != allowedExtension)
                throw new Exception($"Sadece {allowedExtension} dosyaları yüklenebilir.");
        }

        private static void ThrowIfExtensionNotAllowed(IFormFile file, List<string> allowedExtensions)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new Exception($"Sadece {string.Join(", ", allowedExtensions)} dosyaları yüklenebilir.");
        }


    }
}
