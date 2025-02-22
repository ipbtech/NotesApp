using Ardalis.Specification;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Specifications
{
    public class ByUserEmailSpec : Specification<User>
    {
        public ByUserEmailSpec(string email)
        {
            Query.AsNoTracking().Where(x => x.Email == email);
        }
    }
}
