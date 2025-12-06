using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using DomumBackend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class AccessService : IAccessService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccessService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Facility>> GetAccessibleFacilities(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any(r => r == "Admin" || r == "Director" || r == "RegionalProjectManager" || r == "SeniorManager"))
                return await _context.Facilities.ToListAsync();

            if (roles.Any(r => r == "KeyWorker" || r == "OperationManager"))
                return await _context.UserFacilities
                    .Where(uf => uf.UserId == user.Id)
                    .Select(uf => uf.Facility)
                    .ToListAsync();

            return new List<Facility>();
        }

        public async Task<List<YoungPerson>> GetAccessibleYoungPeople(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any(r => r == "Admin" || r == "Director" || r == "RegionalProjectManager" || r == "SeniorManager"))
                return await _context.YoungPeople.ToListAsync();

            if (roles.Any(r => r == "SocialWorker"))
                return await _context.UserYoungPersons
                    .Where(uy => uy.UserId == user.Id)
                    .Select(uy => uy.YoungPerson)
                    .ToListAsync();

            return new List<YoungPerson>();
        }
    }
}
