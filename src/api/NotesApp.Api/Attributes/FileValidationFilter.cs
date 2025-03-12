using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NotesApp.Api.Attributes
{
    public class FileValidationFilter(
        string[] allowedFileTypes, 
        long allowedMaxSize) : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IFormFile);
            if (param.Value is not IFormFile file || file.Length == 0)
            {
                context.Result = new BadRequestObjectResult("File is null");
                return;
            }
            if (!IsFileSizeValid(file))
            {
                var mbSize = allowedMaxSize / 1024 / 1024;
                context.Result = new BadRequestObjectResult($"File shouldn't be more than the maximum allowed size ({mbSize}MB)");
                return;
            }
            if (!IsFileTypeValid(file))
            {
                var allowedExtensionsMessage = string.Join(", ", allowedFileTypes).Replace(".", "").ToLower();
                context.Result = new BadRequestObjectResult($"Invalid file type. Please upload {allowedExtensionsMessage} file.");
            }

        }

        private bool IsFileSizeValid(IFormFile file) => file.Length <= allowedMaxSize;

        private bool IsFileTypeValid(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).Replace(".", "").ToLower();
            return !string.IsNullOrEmpty(ext) && allowedFileTypes.Contains(ext);
        }
    }
}