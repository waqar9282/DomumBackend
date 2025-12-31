namespace DomumBackend.Domain.Enums
{
    /// <summary>
    /// Health & Wellness Enums - Domain enumerations for health tracking, medications, and wellness monitoring
    /// </summary>

    // Medication Status
    public enum MedicationStatus
    {
        Active = 0,           // Currently being taken
        Discontinued = 1,     // Stopped
        Pending = 2,          // Awaiting administration
        OnHold = 3,           // Temporarily paused
    }

    // Appointment Types
    public enum AppointmentType
    {
        Doctor = 0,           // General Practitioner
        Dentist = 1,          // Dental appointment
        MentalHealth = 2,     // Psychologist, counselor
        Specialist = 3,       // Hospital specialist (dermatology, etc.)
        Therapy = 4,          // Physical therapy, occupational therapy
        Emergency = 5,        // Emergency room visit
        Vaccination = 6,      // Immunization
        Screening = 7,        // Health screening
    }

    // Appointment Status
    public enum AppointmentStatus
    {
        Scheduled = 0,        // Upcoming
        Completed = 1,        // Finished
        Cancelled = 2,        // Was cancelled
        NoShow = 3,           // Young person didn't attend
        Rescheduled = 4,      // Moved to different date
    }

    // Health Assessment Types
    public enum HealthAssessmentType
    {
        Physical = 0,         // Physical health check
        Mental = 1,           // Mental health evaluation
        Developmental = 2,    // Developmental progress
        Nutritional = 3,      // Diet and nutrition assessment
        Dental = 4,           // Teeth and oral health
        Vision = 5,           // Eye sight check
        Hearing = 6,          // Hearing assessment
    }

    // Physical Activity Types
    public enum ActivityType
    {
        Sports = 0,           // Team sports, soccer, basketball, etc.
        Walking = 1,          // Walking/hiking
        Swimming = 2,         // Swimming/water sports
        Gym = 3,              // Gym/strength training
        Yoga = 4,             // Yoga/stretching
        Dance = 5,            // Dancing
        Cycling = 6,          // Biking
        Running = 7,          // Running/jogging
        Martial_Arts = 8,     // Boxing, karate, etc.
        Other = 9,            // Other activities
    }

    // Mood Levels (for Mental Health Check-In)
    public enum MoodLevel
    {
        VeryPoor = 0,         // Very upset
        Poor = 1,             // Upset
        Neutral = 2,          // OK / Neither good nor bad
        Good = 3,             // Happy
        VeryGood = 4,         // Very happy/excited
    }
}
