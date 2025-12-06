using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class YoungPersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public YoungPersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new YoungPerson profile
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] YoungPerson youngPerson)
        {
            youngPerson.Id = Guid.NewGuid().ToString();
            youngPerson.IsActive = true;
            youngPerson.DeactivationDate = DateTime.MinValue;
            _context.YoungPeople.Add(youngPerson);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = youngPerson.Id }, youngPerson);
        }

        // Get a YoungPerson by Id (for CreatedAtAction)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var youngPerson = await _context.YoungPeople.FindAsync(id);
            if (youngPerson == null)
                return NotFound();
            return Ok(youngPerson);
        }

        // Update an existing YoungPerson profile
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] YoungPerson updated)
        {
            var youngPerson = await _context.YoungPeople.FindAsync(id);
            if (youngPerson == null)
                return NotFound();

            // Update properties (add more as needed)
            youngPerson.FullName = updated.FullName;
            youngPerson.DateOfBirth = updated.DateOfBirth;
            youngPerson.ProfilePicture = updated.ProfilePicture;
            youngPerson.DateOfEntryToFacility = updated.DateOfEntryToFacility;
            youngPerson.Religion = updated.Religion;
            youngPerson.Gender = updated.Gender;
            youngPerson.Ethnicity = updated.Ethnicity;
            youngPerson.HairColour = updated.HairColour;
            youngPerson.EyeColour = updated.EyeColour;
            youngPerson.Height = updated.Height;
            youngPerson.Education = updated.Education;
            youngPerson.Email = updated.Email;
            youngPerson.PhoneNumber = updated.PhoneNumber;
            youngPerson.HealthIssues = updated.HealthIssues;
            youngPerson.Allergies = updated.Allergies;
            youngPerson.EducationFacilityName = updated.EducationFacilityName;
            youngPerson.EducationFacilityNumber = updated.EducationFacilityNumber;
            youngPerson.EducationFacilityEmail = updated.EducationFacilityEmail;
            youngPerson.IROName = updated.IROName;
            youngPerson.IROContactNo = updated.IROContactNo;
            youngPerson.IROEmail = updated.IROEmail;
            youngPerson.Nationality = updated.Nationality;
            // Do not update IsActive or DeactivationDate here

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Deactivate a YoungPerson profile
        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(string id)
        {
            var youngPerson = await _context.YoungPeople.FindAsync(id);
            if (youngPerson == null)
                return NotFound();

            youngPerson.IsActive = false;
            youngPerson.DeactivationDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/socialworker")]
        public async Task<IActionResult> UpdateSocialWorker(string id, [FromBody] string newSocialWorkerUserId)
        {
            // Check if the YoungPerson exists
            var youngPerson = await _context.YoungPeople.FindAsync(id);
            if (youngPerson == null)
                return NotFound();

            // Remove existing associations for this YoungPerson
            var existingLinks = _context.UserYoungPersons.Where(uyp => uyp.YoungPersonId == id);
            _context.UserYoungPersons.RemoveRange(existingLinks);

            // Add new association
            var newLink = new UserYoungPerson
            {
                UserId = newSocialWorkerUserId,
                YoungPersonId = id
            };
            _context.UserYoungPersons.Add(newLink);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
