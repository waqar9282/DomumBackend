namespace DomumBackend.Application.DTOs
{
    public class IncidentResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int IncidentType { get; set; }
        public string? IncidentTypeDescription { get; set; }
        public int Severity { get; set; }
        public string? SeverityDescription { get; set; }
        public string? Description { get; set; }
        public DateTime IncidentDate { get; set; }
        public DateTime ReportedDate { get; set; }
        public string? ReportedByUserId { get; set; }
        public string? Location { get; set; }
        public string? InjuriesDescription { get; set; }
        public bool MedicalAttentionRequired { get; set; }
        public string? Status { get; set; }
        public string? ReferenceNumber { get; set; }
        public bool IsConfidential { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}

