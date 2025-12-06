using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Identity;

namespace DomumBackend.Infrastructure.Services
{
    public interface IAccessService
    {
        Task<List<Facility>> GetAccessibleFacilities(ApplicationUser user);
        Task<List<YoungPerson>> GetAccessibleYoungPeople(ApplicationUser user);
    }
}

