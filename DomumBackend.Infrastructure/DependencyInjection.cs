using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Repositories.Command.Base;
using DomumBackend.Domain.Repositories.Query.Base;
using DomumBackend.Infrastructure.Data;
using DomumBackend.Infrastructure.Identity;
using DomumBackend.Infrastructure.Repository.Command.Base;
using DomumBackend.Infrastructure.Repository.Query.Base;
using DomumBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DomumBackend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IIncidentService, IncidentService>();
            
            // Health & Wellness Services
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IMedicationService, MedicationService>();
            services.AddScoped<IHealthAssessmentService, HealthAssessmentService>();
            services.AddScoped<INutritionLogService, NutritionLogService>();
            services.AddScoped<IPhysicalActivityLogService, PhysicalActivityLogService>();
            services.AddScoped<IMentalHealthCheckInService, MentalHealthCheckInService>();
            
            // Phase 3A: Reporting & Analytics Services
            services.AddScoped<IIncidentReportingService, IncidentReportingService>();
            services.AddScoped<IHealthAnalyticsService, HealthAnalyticsService>();
            services.AddScoped<IFacilityReportingService, FacilityReportingService>();
            services.AddScoped<IReportExportService, ReportExportService>();
            
            // Phase 3B: Notifications & Alerting Services
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAlertRuleService, AlertRuleService>();
            services.AddScoped<INotificationPreferenceService, NotificationPreferenceService>();
            services.AddScoped<INotificationTemplateService, NotificationTemplateService>();
            
            // Phase 3C: Document Management & Records Services
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentCategoryService, DocumentCategoryService>();
            services.AddScoped<IDocumentAccessService, DocumentAccessService>();
            services.AddScoped<IDocumentRetentionService, DocumentRetentionService>();
            
            // Phase 3D: Advanced Analytics & Insights Services
            services.AddScoped<IAnalyticsMetricService>(sp => new AnalyticsMetricService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<ITrendAnalysisService>(sp => new TrendAnalysisService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<IPredictiveAlertService>(sp => new PredictiveAlertService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<ICustomDashboardService>(sp => new CustomDashboardService(sp.GetRequiredService<ApplicationDbContext>()));
            
            // Phase 3E: Staff Management Services
            services.AddScoped<IStaffManagementService>(sp => new StaffManagementService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<IStaffAllocationService>(sp => new StaffAllocationService(sp.GetRequiredService<ApplicationDbContext>()));
            
            // Phase 3F: Compliance & Auditing Services
            services.AddScoped<IComplianceAuditService>(sp => new ComplianceAuditService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<IComplianceChecklistService>(sp => new ComplianceChecklistService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<IComplianceNonConformityService>(sp => new ComplianceNonConformityService(sp.GetRequiredService<ApplicationDbContext>()));
            services.AddScoped<IComplianceDocumentService>(sp => new ComplianceDocumentService(sp.GetRequiredService<ApplicationDbContext>()));
            
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            return services;
        }
    }
}

