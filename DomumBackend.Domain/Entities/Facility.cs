namespace DomumBackend.Domain.Entities
{
    public class Facility
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public bool IsActive { get; set; }
        // Navigation property for assignments (optional, if using EF Core)
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<UserFacility>? UserFacilities { get; set; }
        public ICollection<YoungPerson>? YoungPersons { get; set; }

    }
}


