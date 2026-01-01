namespace DomumBackend.Domain.Entities
{
    public class UserFacility
    {
        public string? UserId { get; set; }
        public string? FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}


