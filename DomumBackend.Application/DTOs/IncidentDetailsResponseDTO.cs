namespace DomumBackend.Application.DTOs
{
    public class IncidentDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int IncidentType { get; set; }
        public int Severity { get; set; }
        public string? Description { get; set; }
        public DateTime IncidentDate { get; set; }
        public DateTime ReportedDate { get; set; }
        public string? ReportedByUserId { get; set; }
        public string? Location { get; set; }
        public string? OtherPersonsInvolved { get; set; }
        public string? StaffPresent { get; set; }
        public string? InjuriesDescription { get; set; }
        public string? TreatmentProvided { get; set; }
        public bool MedicalAttentionRequired { get; set; }
        public string? MedicalTreatmentLocation { get; set; }
        public string? WitnessStatements { get; set; }
        public string? RootCause { get; set; }
        public string? CorrectiveActions { get; set; }
        public bool IsNotifiable { get; set; }
        public string? NotifiedAuthority { get; set; }
        public int Status { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string? ClosingNotes { get; set; }
        public string? ReferenceNumber { get; set; }
        public bool IsConfidential { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

