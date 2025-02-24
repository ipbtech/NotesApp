using System.Security.Claims;

namespace NotesApp.Api.Extensions
{
    public class HttpContextProvider(
        IHttpContextAccessor contextAccessor)
    {
        public Guid GetCurrentUserId()
        {
            var result = _getGuidFromContext(ClaimTypes.NameIdentifier);
            if (result.HasValue)
                return result.Value;

            throw new UnauthorizedAccessException("Unauthorized");
        }

        public Guid GetCurrentUserEmail()
        {
            var result = _getGuidFromContext(ClaimTypes.Email);
            if (result.HasValue)
                return result.Value;

            throw new UnauthorizedAccessException("Unauthorized");
        }

        private Guid? _getGuidFromContext(string claimType)
        {
            var guid = contextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == claimType)?.Value;

            if (guid is not null && Guid.TryParse(guid, out Guid result))
            {
                return result;
            }
            return null;
        }
    }
}