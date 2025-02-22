using Ardalis.Specification;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Specifications
{
    public class ByDeviceSessionIdSpec : Specification<RefreshToken>
    {
        public ByDeviceSessionIdSpec(Guid deviceSessionId)
        {
            Query.Where(e => e.DeviceSessionId == deviceSessionId);
        }
    }
}
