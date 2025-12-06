using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{

    public class YoungPerson
    {
        public string Id { get; set; } // Unique identifier (GUID or string)
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string FacilityId { get; set; }
        public Facility Facility { get; set; }

        public int? RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<UserYoungPerson> UserYoungPersons { get; set; }

        // New properties
        public string ProfilePicture { get; set; } // URL or file path
        public DateTime DateOfEntryToFacility { get; set; }
        public Religion? Religion { get; set; } // Changed to enum
        public Gender? Gender { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public HairColour? HairColour { get; set; }
        public EyeColour? EyeColour { get; set; }
        public decimal? Height { get; set; } // In centimeters or meters
        public string? Education { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? HealthIssues { get; set; }
        public string? Allergies { get; set; }
        public string? EducationFacilityName { get; set; }
        public string? EducationFacilityNumber { get; set; }
        public string? EducationFacilityEmail { get; set; }
        public string? IROName { get; set; }
        public string? IROContactNo { get; set; }
        public string? IROEmail { get; set; }
        public string? Nationality { get; set; }
        public bool IsActive { get; set; } = true; // Default to true, indicating the young person is active
        public DateTime DeactivationDate { get; set; } = DateTime.MinValue; // Default to MinValue, indicating no deactivation date
    }
}

