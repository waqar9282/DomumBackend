namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Notification Template - Email/SMS template definitions
    /// Stores reusable templates for different notification types
    /// </summary>
    public class NotificationTemplate : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Template Identification
        public required string TemplateName { get; set; }
        public required string TemplateKey { get; set; } // Unique identifier: INCIDENT_ALERT, HEALTH_REVIEW, etc.
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        // Template Category
        public required string Category { get; set; } // IncidentAlert, HealthAlert, SafeguardingAlert, ReportGeneration, etc.
        public required string NotificationChannel { get; set; } // Email, SMS, InApp

        // Email Template
        public string? EmailSubjectTemplate { get; set; }
        public string? EmailBodyTemplate { get; set; } // Plain text version
        public string? EmailHtmlTemplate { get; set; } // HTML version

        // SMS Template
        public string? SMSBodyTemplate { get; set; }
        public int? SMSMaxLength { get; set; } = 160;

        // In-App Template
        public string? InAppTitleTemplate { get; set; }
        public string? InAppMessageTemplate { get; set; }
        public string? InAppActionUrl { get; set; }

        // Template Variables
        public string? SupportedVariables { get; set; } // JSON list of available variables
        // e.g., {{facilityName}}, {{incidentType}}, {{youngPersonName}}, {{severity}}, {{date}}, {{reporter}}

        // Template Versioning
        public int VersionNumber { get; set; } = 1;
        public DateTime EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public bool IsDefault { get; set; } = false;

        // Template Metadata
        public DateTime CreatedDate_Template { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedByUserId { get; set; }
        public int TimesUsed { get; set; } = 0;

        // Translation/Localization
        public string? Language { get; set; } = "en-GB"; // Default to GB English
        public bool HasLocalizedVersions { get; set; } = false;
    }
}
