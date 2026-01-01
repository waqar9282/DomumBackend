using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace DomumBackend.Infrastructure.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
         }
        */
        public DbSet<UserFacility> UserFacilities { get; set; }
        public DbSet<UserYoungPerson> UserYoungPersons { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<YoungPerson> YoungPeople { get; set; }
        public DbSet<Room> Rooms { get; set; }

        // Phase 1: Compliance & Audit
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<SafetyAlert> SafetyAlerts { get; set; }
        public DbSet<SafeguardingConcern> SafeguardingConcerns { get; set; }

        // Phase 2: Health & Wellness
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<HealthAssessment> HealthAssessments { get; set; }
        public DbSet<NutritionLog> NutritionLogs { get; set; }
        public DbSet<PhysicalActivityLog> PhysicalActivityLogs { get; set; }
        public DbSet<MentalHealthCheckIn> MentalHealthCheckIns { get; set; }

        // Phase 3A: Reporting & Analytics
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<HealthMetricsReport> HealthMetricsReports { get; set; }
        public DbSet<FacilityReport> FacilityReports { get; set; }

        // Phase 3B: Notifications & Alerting
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AlertRule> AlertRules { get; set; }
        public DbSet<NotificationPreference> NotificationPreferences { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

        // Phase 3C: Document Management & Records
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<DocumentAccess> DocumentAccesses { get; set; }
        public DbSet<DocumentRetention> DocumentRetentions { get; set; }

        // Phase 3D: Advanced Analytics & Insights
        public DbSet<AnalyticsMetric> AnalyticsMetrics { get; set; }
        public DbSet<TrendAnalysis> TrendAnalyses { get; set; }
        public DbSet<PredictiveAlert> PredictiveAlerts { get; set; }
        public DbSet<CustomDashboard> CustomDashboards { get; set; }

        // Phase 3E: Staff Management
        public DbSet<Staff> Staff { get; set; }
        public DbSet<StaffAllocation> StaffAllocations { get; set; }

        // Phase 3F: Compliance & Auditing
        public DbSet<ComplianceAudit> ComplianceAudits { get; set; }
        public DbSet<ComplianceChecklistItem> ComplianceChecklistItems { get; set; }
        public DbSet<ComplianceNonConformity> ComplianceNonConformities { get; set; }
        public DbSet<ComplianceDocument> ComplianceDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one: Room <-> YoungPerson
            modelBuilder.Entity<Room>()
                .HasOne(r => r.YoungPerson)
                .WithOne(yp => yp.Room)
                .HasForeignKey<YoungPerson>(yp => yp.RoomId);

            // Facility and Room: One-to-many
            modelBuilder.Entity<Facility>()
                .HasMany(f => f.Rooms)
                .WithOne(r => r.Facility)
                .HasForeignKey(r => r.FacilityId);

            // Facility and YoungPerson: One-to-many
            modelBuilder.Entity<Facility>()
                .HasMany(f => f.YoungPersons)
                .WithOne(yp => yp.Facility)
                .HasForeignKey(yp => yp.FacilityId);

            // UserFacility: Composite Key and Relationships
            modelBuilder.Entity<UserFacility>()
                .HasKey(uf => new { uf.UserId, uf.FacilityId });

            modelBuilder.Entity<UserFacility>()
                .HasOne(uf => uf.Facility)
                .WithMany(f => f.UserFacilities)
                .HasForeignKey(uf => uf.FacilityId);

            // UserYoungPerson: Composite Key and Relationships
            modelBuilder.Entity<UserYoungPerson>()
                .HasKey(uy => new { uy.UserId, uy.YoungPersonId });

            modelBuilder.Entity<UserYoungPerson>()
                .HasOne(uy => uy.YoungPerson)
                .WithMany(yp => yp.UserYoungPersons)
                .HasForeignKey(uy => uy.YoungPersonId);

            // ApplicationUser navigation properties
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserFacilities)
                .WithOne()
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserYoungPersons)
                .WithOne()
                .HasForeignKey(uy => uy.UserId);

            modelBuilder.Entity<YoungPerson>()
                .Property(y => y.Height)
                .HasPrecision(5, 2); // Allows values like 123.45

            // ===== PHASE 1: COMPLIANCE & AUDIT =====

            // AuditLog configuration
            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.EntityId);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.UserId);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.Timestamp);

            // Incident configuration
            modelBuilder.Entity<Incident>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Facility)
                .WithMany()
                .HasForeignKey(i => i.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.YoungPerson)
                .WithMany()
                .HasForeignKey(i => i.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Incident records must persist even if young person is deleted (compliance)
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.FacilityId);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.YoungPersonId);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.IncidentDate);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.Status);

            // SafetyAlert configuration
            modelBuilder.Entity<SafetyAlert>()
                .HasKey(sa => sa.Id);
            modelBuilder.Entity<SafetyAlert>()
                .HasOne(sa => sa.Facility)
                .WithMany()
                .HasForeignKey(sa => sa.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SafetyAlert>()
                .HasOne(sa => sa.YoungPerson)
                .WithMany()
                .HasForeignKey(sa => sa.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Safety alerts must persist for audit trail
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.FacilityId);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.YoungPersonId);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.AlertLevel);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.IsActive);

            // SafeguardingConcern configuration
            modelBuilder.Entity<SafeguardingConcern>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasOne(sc => sc.Facility)
                .WithMany()
                .HasForeignKey(sc => sc.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasOne(sc => sc.YoungPerson)
                .WithMany()
                .HasForeignKey(sc => sc.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Safeguarding records must persist for compliance
            
            // Many-to-many: Incident <-> SafeguardingConcern (with NoAction on join table)
            modelBuilder.Entity<SafeguardingConcern>()
                .HasMany(sc => sc.LinkedIncidents)
                .WithMany(i => i.LinkedConcerns)
                .UsingEntity<Dictionary<string, object>>(
                    "IncidentSafeguardingConcern",
                    j => j.HasOne<Incident>().WithMany().OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<SafeguardingConcern>().WithMany().OnDelete(DeleteBehavior.NoAction));
            
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.FacilityId);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.YoungPersonId);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.ConcernType);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.Status);

            // ===== PHASE 2: HEALTH & WELLNESS =====

            // MedicalRecord configuration
            modelBuilder.Entity<MedicalRecord>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.YoungPerson)
                .WithMany()
                .HasForeignKey(m => m.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Medical records must persist for health history
            modelBuilder.Entity<MedicalRecord>()
                .HasIndex(m => m.FacilityId);
            modelBuilder.Entity<MedicalRecord>()
                .HasIndex(m => m.YoungPersonId);
            modelBuilder.Entity<MedicalRecord>()
                .HasIndex(m => m.AppointmentDate);
            modelBuilder.Entity<MedicalRecord>()
                .HasIndex(m => m.Status);

            // Medication configuration
            modelBuilder.Entity<Medication>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Medication>()
                .HasOne(m => m.YoungPerson)
                .WithMany()
                .HasForeignKey(m => m.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Medication records critical for health
            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.FacilityId);
            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.YoungPersonId);
            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.Status);
            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.StartDate);

            // HealthAssessment configuration
            modelBuilder.Entity<HealthAssessment>()
                .HasKey(ha => ha.Id);
            modelBuilder.Entity<HealthAssessment>()
                .HasOne(ha => ha.YoungPerson)
                .WithMany()
                .HasForeignKey(ha => ha.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Assessment history critical for wellbeing
            
            // Decimal precision for health measurements
            modelBuilder.Entity<HealthAssessment>()
                .Property(ha => ha.Height)
                .HasPrecision(10, 2); // cm with 2 decimal places
            modelBuilder.Entity<HealthAssessment>()
                .Property(ha => ha.Weight)
                .HasPrecision(10, 2); // kg with 2 decimal places
            modelBuilder.Entity<HealthAssessment>()
                .Property(ha => ha.BMI)
                .HasPrecision(10, 2); // BMI with 2 decimal places
            modelBuilder.Entity<HealthAssessment>()
                .Property(ha => ha.BodyTemperature)
                .HasPrecision(5, 2); // Temperature with 2 decimal places
            
            modelBuilder.Entity<HealthAssessment>()
                .HasIndex(ha => ha.FacilityId);
            modelBuilder.Entity<HealthAssessment>()
                .HasIndex(ha => ha.YoungPersonId);
            modelBuilder.Entity<HealthAssessment>()
                .HasIndex(ha => ha.AssessmentType);
            modelBuilder.Entity<HealthAssessment>()
                .HasIndex(ha => ha.AssessmentDate);

            // NutritionLog configuration
            modelBuilder.Entity<NutritionLog>()
                .HasKey(nl => nl.Id);
            modelBuilder.Entity<NutritionLog>()
                .HasOne(nl => nl.YoungPerson)
                .WithMany()
                .HasForeignKey(nl => nl.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Decimal precision for nutritional values
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.PortionSize)
                .HasPrecision(10, 2); // grams or ml
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Calories)
                .HasPrecision(10, 2); // Calories per serving
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Protein)
                .HasPrecision(10, 2); // grams
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Carbohydrates)
                .HasPrecision(10, 2); // grams
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Fat)
                .HasPrecision(10, 2); // grams
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Fiber)
                .HasPrecision(10, 2); // grams
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Sugar)
                .HasPrecision(10, 2); // grams
            modelBuilder.Entity<NutritionLog>()
                .Property(nl => nl.Sodium)
                .HasPrecision(10, 2); // mg
            
            modelBuilder.Entity<NutritionLog>()
                .HasIndex(nl => nl.FacilityId);
            modelBuilder.Entity<NutritionLog>()
                .HasIndex(nl => nl.YoungPersonId);
            modelBuilder.Entity<NutritionLog>()
                .HasIndex(nl => nl.LogDate);

            // PhysicalActivityLog configuration
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasKey(pal => pal.Id);
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasOne(pal => pal.YoungPerson)
                .WithMany()
                .HasForeignKey(pal => pal.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasIndex(pal => pal.FacilityId);
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasIndex(pal => pal.YoungPersonId);
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasIndex(pal => pal.ActivityType);
            modelBuilder.Entity<PhysicalActivityLog>()
                .HasIndex(pal => pal.ActivityDate);

            // MentalHealthCheckIn configuration
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasKey(mhc => mhc.Id);
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasOne(mhc => mhc.YoungPerson)
                .WithMany()
                .HasForeignKey(mhc => mhc.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Mental health records are sensitive and critical
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasIndex(mhc => mhc.FacilityId);
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasIndex(mhc => mhc.YoungPersonId);
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasIndex(mhc => mhc.CurrentMood);
            modelBuilder.Entity<MentalHealthCheckIn>()
                .HasIndex(mhc => mhc.CheckInDate);

            // Phase 3A - Reporting & Analytics
            modelBuilder.Entity<IncidentReport>()
                .HasIndex(ir => ir.FacilityId);
            modelBuilder.Entity<IncidentReport>()
                .HasIndex(ir => ir.ReportGeneratedDate);
            modelBuilder.Entity<IncidentReport>()
                .HasOne(ir => ir.Facility)
                .WithMany()
                .HasForeignKey(ir => ir.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IncidentReport>()
                .Property(ir => ir.IncidentTrend)
                .HasPrecision(10, 2);

            modelBuilder.Entity<HealthMetricsReport>()
                .HasIndex(hmr => hmr.FacilityId);
            modelBuilder.Entity<HealthMetricsReport>()
                .HasIndex(hmr => hmr.ReportGeneratedDate);
            modelBuilder.Entity<HealthMetricsReport>()
                .HasOne(hmr => hmr.Facility)
                .WithMany()
                .HasForeignKey(hmr => hmr.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageBodyMassIndex)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageHeight)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageWeight)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageBloodPressureSystolic)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageBloodPressureDiastolic)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageCaloriesPerDay)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageProteinIntake)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageFatIntake)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageActivityMinutesPerWeek)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.AverageMoodScore)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.MedicationComplianceRate)
                .HasPrecision(10, 2);
            modelBuilder.Entity<HealthMetricsReport>()
                .Property(hmr => hmr.ActivityComplianceRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FacilityReport>()
                .HasIndex(fr => fr.FacilityId);
            modelBuilder.Entity<FacilityReport>()
                .HasIndex(fr => fr.ReportGeneratedDate);
            modelBuilder.Entity<FacilityReport>()
                .HasOne(fr => fr.Facility)
                .WithMany()
                .HasForeignKey(fr => fr.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.ComplianceScore)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.IncidentReductionRate)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.HealthGoalCompletionRate)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.SuccessfulOutcomeRate)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.StaffRetentionRate)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.StorageUtilizationPercentage)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.KPI1_Value)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.KPI2_Value)
                .HasPrecision(10, 2);
            modelBuilder.Entity<FacilityReport>()
                .Property(fr => fr.KPI3_Value)
                .HasPrecision(10, 2);

            // Phase 3B - Notifications & Alerting
            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.FacilityId);
            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.RecipientUserId);
            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.Status);
            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.CreatedDate_Scheduled);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Facility)
                .WithMany()
                .HasForeignKey(n => n.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlertRule>()
                .HasIndex(ar => ar.FacilityId);
            modelBuilder.Entity<AlertRule>()
                .HasIndex(ar => ar.IsActive);
            modelBuilder.Entity<AlertRule>()
                .HasOne(ar => ar.Facility)
                .WithMany()
                .HasForeignKey(ar => ar.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationPreference>()
                .HasIndex(np => np.UserId);
            modelBuilder.Entity<NotificationPreference>()
                .HasIndex(np => np.FacilityId);
            modelBuilder.Entity<NotificationPreference>()
                .HasOne(np => np.Facility)
                .WithMany()
                .HasForeignKey(np => np.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationTemplate>()
                .HasIndex(nt => nt.FacilityId);
            modelBuilder.Entity<NotificationTemplate>()
                .HasIndex(nt => nt.TemplateKey);
            modelBuilder.Entity<NotificationTemplate>()
                .HasIndex(nt => nt.Category);
            modelBuilder.Entity<NotificationTemplate>()
                .HasOne(nt => nt.Facility)
                .WithMany()
                .HasForeignKey(nt => nt.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Phase 3C - Document Management & Records
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.FacilityId);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.DocumentType);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.DocumentCategory);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.UploadedDate);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.Status);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.ExpiryDate);
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Facility)
                .WithMany()
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentCategory>()
                .HasIndex(dc => dc.FacilityId);
            modelBuilder.Entity<DocumentCategory>()
                .HasIndex(dc => dc.CategoryCode);
            modelBuilder.Entity<DocumentCategory>()
                .HasOne(dc => dc.Facility)
                .WithMany()
                .HasForeignKey(dc => dc.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentAccess>()
                .HasIndex(da => da.FacilityId);
            modelBuilder.Entity<DocumentAccess>()
                .HasIndex(da => da.DocumentId);
            modelBuilder.Entity<DocumentAccess>()
                .HasIndex(da => da.AccessedByUserId);
            modelBuilder.Entity<DocumentAccess>()
                .HasIndex(da => da.AccessedDate);
            modelBuilder.Entity<DocumentAccess>()
                .HasOne(da => da.Facility)
                .WithMany()
                .HasForeignKey(da => da.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DocumentAccess>()
                .HasOne(da => da.Document)
                .WithMany()
                .HasForeignKey(da => da.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentRetention>()
                .HasIndex(dr => dr.FacilityId);
            modelBuilder.Entity<DocumentRetention>()
                .HasIndex(dr => dr.DocumentType);
            modelBuilder.Entity<DocumentRetention>()
                .HasIndex(dr => dr.IsActive);
            modelBuilder.Entity<DocumentRetention>()
                .HasOne(dr => dr.Facility)
                .WithMany()
                .HasForeignKey(dr => dr.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Phase 3D: Analytics & Insights Configurations
            modelBuilder.Entity<AnalyticsMetric>()
                .HasIndex(am => am.FacilityId);
            modelBuilder.Entity<AnalyticsMetric>()
                .HasIndex(am => am.MetricCode);
            modelBuilder.Entity<AnalyticsMetric>()
                .HasIndex(am => am.Category);
            modelBuilder.Entity<AnalyticsMetric>()
                .HasIndex(am => am.Status);
            modelBuilder.Entity<AnalyticsMetric>()
                .HasIndex(am => am.MeasurementDate);
            modelBuilder.Entity<AnalyticsMetric>()
                .HasOne(am => am.Facility)
                .WithMany()
                .HasForeignKey(am => am.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrendAnalysis>()
                .HasIndex(ta => ta.FacilityId);
            modelBuilder.Entity<TrendAnalysis>()
                .HasIndex(ta => ta.AnalyticsMetricId);
            modelBuilder.Entity<TrendAnalysis>()
                .HasIndex(ta => ta.TrendType);
            modelBuilder.Entity<TrendAnalysis>()
                .HasIndex(ta => ta.CreatedAnalysisDate);
            modelBuilder.Entity<TrendAnalysis>()
                .HasOne(ta => ta.Facility)
                .WithMany()
                .HasForeignKey(ta => ta.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrendAnalysis>()
                .HasOne(ta => ta.AnalyticsMetric)
                .WithMany()
                .HasForeignKey(ta => ta.AnalyticsMetricId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.FacilityId);
            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.AnalyticsMetricId);
            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.AlertType);
            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.Status);
            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.AlertCategory);
            modelBuilder.Entity<PredictiveAlert>()
                .HasIndex(pa => pa.AlertGeneratedDate);
            modelBuilder.Entity<PredictiveAlert>()
                .HasOne(pa => pa.Facility)
                .WithMany()
                .HasForeignKey(pa => pa.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PredictiveAlert>()
                .HasOne(pa => pa.AnalyticsMetric)
                .WithMany()
                .HasForeignKey(pa => pa.AnalyticsMetricId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomDashboard>()
                .HasIndex(cd => cd.FacilityId);
            modelBuilder.Entity<CustomDashboard>()
                .HasIndex(cd => cd.CreatedByUserId);
            modelBuilder.Entity<CustomDashboard>()
                .HasIndex(cd => cd.DashboardType);
            modelBuilder.Entity<CustomDashboard>()
                .HasIndex(cd => cd.IsDefault);
            modelBuilder.Entity<CustomDashboard>()
                .HasIndex(cd => cd.CreatedDate);
            modelBuilder.Entity<CustomDashboard>()
                .HasOne(cd => cd.Facility)
                .WithMany()
                .HasForeignKey(cd => cd.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Phase 3E: Staff Management Configurations
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.FacilityId);
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.UserId);
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email);
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.EmploymentStatus);
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.IsActive);
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Facility)
                .WithMany()
                .HasForeignKey(s => s.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StaffAllocation>()
                .HasIndex(sa => sa.FacilityId);
            modelBuilder.Entity<StaffAllocation>()
                .HasIndex(sa => sa.StaffId);
            modelBuilder.Entity<StaffAllocation>()
                .HasIndex(sa => sa.YoungPersonId);
            modelBuilder.Entity<StaffAllocation>()
                .HasIndex(sa => sa.IsActive);
            modelBuilder.Entity<StaffAllocation>()
                .HasIndex(sa => sa.AllocationType);
            modelBuilder.Entity<StaffAllocation>()
                .HasOne(sa => sa.Staff)
                .WithMany()
                .HasForeignKey(sa => sa.StaffId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StaffAllocation>()
                .HasOne(sa => sa.YoungPerson)
                .WithMany()
                .HasForeignKey(sa => sa.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StaffAllocation>()
                .HasOne(sa => sa.Facility)
                .WithMany()
                .HasForeignKey(sa => sa.FacilityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
