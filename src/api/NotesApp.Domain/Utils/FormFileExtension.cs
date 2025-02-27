using Microsoft.AspNetCore.Http;

namespace NotesApp.Domain.Utils
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> ToBytesAsync(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
