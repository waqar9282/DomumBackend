namespace DomumBackend.Domain.Enums
{
    /// <summary>
    /// Types of incidents that can be reported
    /// </summary>
    public enum IncidentType
    {
        /// <summary>
        /// Physical injury or accident
        /// </summary>
        Injury = 0,

        /// <summary>
        /// Behavioral incident, aggression, conflict
        /// </summary>
        BehavioralIssue = 1,

        /// <summary>
        /// Medical emergency
        /// </summary>
        MedicalEmergency = 2,

        /// <summary>
        /// Young person absconding/running away
        /// </summary>
        Elopement = 3,

        /// <summary>
        /// Property damage or vandalism
        /// </summary>
        PropertyDamage = 4,

        /// <summary>
        /// Substance misuse incident
        /// </summary>
        SubstanceMisuse = 5,

        /// <summary>
        /// Safeguarding/protection concern
        /// </summary>
        SafeguardingConcern = 6,

        /// <summary>
        /// Medication error or health-related issue
        /// </summary>
        MedicationIssue = 7,

        /// <summary>
        /// Self-harm or suicide attempt
        /// </summary>
        SelfHarm = 8,

        /// <summary>
        /// Fire safety issue
        /// </summary>
        FireSafetyIssue = 9,

        /// <summary>
        /// Pest or hygiene issue
        /// </summary>
        HygieneIssue = 10,

        /// <summary>
        /// Sexual incident or inappropriate behavior
        /// </summary>
        SexualIncident = 11,

        /// <summary>
        /// Staff-related incident
        /// </summary>
        StaffIncident = 12,

        /// <summary>
        /// Bullying/peer conflict
        /// </summary>
        Bullying = 13,

        /// <summary>
        /// Visitor-related incident
        /// </summary>
        VisitorIncident = 14,

        /// <summary>
        /// Security or breach incident
        /// </summary>
        SecurityBreach = 15,

        /// <summary>
        /// Other incident type
        /// </summary>
        Other = 99
    }

    /// <summary>
    /// Severity level of an incident
    /// </summary>
    public enum IncidentSeverity
    {
        /// <summary>
        /// Minor incident, no injuries, easily resolved
        /// </summary>
        Minor = 0,

        /// <summary>
        /// Moderate incident, minor injuries or significant disruption
        /// </summary>
        Moderate = 1,

        /// <summary>
        /// Serious incident, significant injury or major safeguarding concern
        /// </summary>
        Serious = 2,

        /// <summary>
        /// Critical/life-threatening incident
        /// </summary>
        Critical = 3
    }

    /// <summary>
    /// Status of an incident investigation
    /// </summary>
    public enum IncidentStatus
    {
        /// <summary>
        /// Incident reported but not yet reviewed
        /// </summary>
        Open = 0,

        /// <summary>
        /// Under investigation/review
        /// </summary>
        UnderInvestigation = 1,

        /// <summary>
        /// Investigation complete, waiting for action
        /// </summary>
        PendingAction = 2,

        /// <summary>
        /// Incident investigation closed
        /// </summary>
        Closed = 3,

        /// <summary>
        /// Incident report withdrawn/cancelled
        /// </summary>
        Withdrawn = 4
    }
}
