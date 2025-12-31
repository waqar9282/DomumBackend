#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DomumBackend.Infrastructure.Services
{
    public class NotificationTemplateService : INotificationTemplateService
    {
        private readonly ApplicationDbContext _context;

        public NotificationTemplateService(ApplicationDbContext context)
            => _context = context;

        public async Task<NotificationTemplateDTO> CreateTemplateAsync(
            string facilityId,
            string templateName,
            string templateKey,
            string category,
            string notificationChannel,
            string emailSubject = null,
            string emailBody = null,
            string emailHtml = null,
            string smsBody = null,
            string supportedVariables = null,
            string createdByUserId = default,
            CancellationToken cancellationToken = default)
        {
            var template = new NotificationTemplate
            {
                FacilityId = facilityId,
                TemplateName = templateName,
                TemplateKey = templateKey,
                Category = category,
                NotificationChannel = notificationChannel,
                EmailSubjectTemplate = emailSubject,
                EmailBodyTemplate = emailBody,
                EmailHtmlTemplate = emailHtml,
                SMSBodyTemplate = smsBody,
                SupportedVariables = supportedVariables,
                IsActive = true,
                VersionNumber = 1,
                EffectiveFromDate = DateTime.UtcNow,
                CreatedDate_Template = DateTime.UtcNow,
                CreatedByUserId = createdByUserId,
                Language = "en-GB",
                TimesUsed = 0
            };

            _context.NotificationTemplates.Add(template);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(template);
        }

        public async Task<NotificationTemplateDTO> UpdateTemplateAsync(
            long templateId,
            string templateName,
            string templateKey,
            string emailSubject = null,
            string emailBody = null,
            string emailHtml = null,
            bool isActive = true,
            string updatedByUserId = default,
            CancellationToken cancellationToken = default)
        {
            var template = await _context.NotificationTemplates.FindAsync(new object[] { templateId }, cancellationToken);
            if (template == null)
                throw new KeyNotFoundException($"Template {templateId} not found");

            template.TemplateName = templateName;
            template.TemplateKey = templateKey;
            template.EmailSubjectTemplate = emailSubject ?? template.EmailSubjectTemplate;
            template.EmailBodyTemplate = emailBody ?? template.EmailBodyTemplate;
            template.EmailHtmlTemplate = emailHtml ?? template.EmailHtmlTemplate;
            template.IsActive = isActive;
            template.LastModifiedDate = DateTime.UtcNow;
            template.LastModifiedByUserId = updatedByUserId;

            _context.NotificationTemplates.Update(template);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(template);
        }

        public async Task<NotificationTemplateDTO> GetTemplateByKeyAsync(
            string facilityId,
            string templateKey,
            CancellationToken cancellationToken = default)
        {
            var template = await _context.NotificationTemplates
                .FirstOrDefaultAsync(t => t.FacilityId == facilityId && 
                                         t.TemplateKey == templateKey &&
                                         t.IsActive, 
                                         cancellationToken);

            if (template == null)
                throw new KeyNotFoundException($"Template with key {templateKey} not found");

            return MapToDTO(template);
        }

        public async Task<List<NotificationTemplateDTO>> GetTemplatesByFacilityAsync(
            string facilityId,
            string category = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.NotificationTemplates
                .Where(t => t.FacilityId == facilityId && t.IsActive);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(t => t.Category == category);

            var templates = await query.ToListAsync(cancellationToken);
            return templates.Select(MapToDTO).ToList();
        }

        public async Task<string> RenderTemplateAsync(
            long templateId,
            Dictionary<string, string> variables,
            CancellationToken cancellationToken = default)
        {
            var template = await _context.NotificationTemplates.FindAsync(new object[] { templateId }, cancellationToken);
            if (template == null)
                throw new KeyNotFoundException($"Template {templateId} not found");

            string content = template.NotificationChannel switch
            {
                "Email" => template.EmailBodyTemplate ?? template.EmailHtmlTemplate ?? "",
                "SMS" => template.SMSBodyTemplate ?? "",
                _ => ""
            };

            // Replace variables in template
            foreach (var variable in variables)
            {
                var placeholder = $"{{{{{variable.Key}}}}}";
                content = content.Replace(placeholder, variable.Value);
            }

            template.TimesUsed++;
            _context.NotificationTemplates.Update(template);
            await _context.SaveChangesAsync(cancellationToken);

            return content;
        }

        public async Task<bool> DeleteTemplateAsync(
            long templateId,
            CancellationToken cancellationToken = default)
        {
            var template = await _context.NotificationTemplates.FindAsync(new object[] { templateId }, cancellationToken);
            if (template == null)
                return false;

            _context.NotificationTemplates.Remove(template);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SetDefaultTemplateAsync(
            long templateId,
            CancellationToken cancellationToken = default)
        {
            var template = await _context.NotificationTemplates.FindAsync(new object[] { templateId }, cancellationToken);
            if (template == null)
                return false;

            // Clear default flag from other templates in same category/facility
            var otherDefaults = await _context.NotificationTemplates
                .Where(t => t.FacilityId == template.FacilityId &&
                           t.Category == template.Category &&
                           t.NotificationChannel == template.NotificationChannel &&
                           t.IsDefault)
                .ToListAsync(cancellationToken);

            foreach (var other in otherDefaults)
            {
                other.IsDefault = false;
            }

            template.IsDefault = true;

            _context.NotificationTemplates.UpdateRange(otherDefaults);
            _context.NotificationTemplates.Update(template);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<NotificationTemplateDTO> CreateVersionAsync(
            long originalTemplateId,
            string modifiedContent,
            string updatedByUserId,
            CancellationToken cancellationToken = default)
        {
            var originalTemplate = await _context.NotificationTemplates.FindAsync(new object[] { originalTemplateId }, cancellationToken);
            if (originalTemplate == null)
                throw new KeyNotFoundException($"Template {originalTemplateId} not found");

            var newVersion = new NotificationTemplate
            {
                FacilityId = originalTemplate.FacilityId,
                TemplateName = originalTemplate.TemplateName,
                TemplateKey = originalTemplate.TemplateKey,
                Category = originalTemplate.Category,
                NotificationChannel = originalTemplate.NotificationChannel,
                EmailSubjectTemplate = originalTemplate.EmailSubjectTemplate,
                EmailBodyTemplate = originalTemplate.NotificationChannel == "Email" ? modifiedContent : originalTemplate.EmailBodyTemplate,
                EmailHtmlTemplate = originalTemplate.EmailHtmlTemplate,
                SMSBodyTemplate = originalTemplate.NotificationChannel == "SMS" ? modifiedContent : originalTemplate.SMSBodyTemplate,
                SupportedVariables = originalTemplate.SupportedVariables,
                IsActive = true,
                VersionNumber = originalTemplate.VersionNumber + 1,
                EffectiveFromDate = DateTime.UtcNow,
                CreatedDate_Template = DateTime.UtcNow,
                CreatedByUserId = updatedByUserId,
                Language = originalTemplate.Language
            };

            _context.NotificationTemplates.Add(newVersion);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(newVersion);
        }

        private NotificationTemplateDTO MapToDTO(NotificationTemplate template)
        {
            return new NotificationTemplateDTO
            {
                Id = template.Id,
                TemplateName = template.TemplateName,
                TemplateKey = template.TemplateKey,
                Category = template.Category,
                NotificationChannel = template.NotificationChannel,
                IsActive = template.IsActive,
                VersionNumber = template.VersionNumber,
                IsDefault = template.IsDefault,
                TimesUsed = template.TimesUsed
            };
        }
    }
}
