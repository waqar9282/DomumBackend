#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class NotificationPreferenceService : INotificationPreferenceService
    {
        private readonly ApplicationDbContext _context;

        public NotificationPreferenceService(ApplicationDbContext context)
            => _context = context;

        public async Task<NotificationPreferenceDTO> GetUserPreferencesAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId && np.FacilityId == facilityId, cancellationToken);

            if (preference == null)
                return await CreateDefaultPreferencesAsync(userId, facilityId, cancellationToken);

            return MapToDTO(preference);
        }

        public async Task<NotificationPreferenceDTO> UpdateUserPreferencesAsync(
            string userId,
            string facilityId,
            NotificationPreferenceDTO preferences,
            string updatedByUserId,
            CancellationToken cancellationToken = default)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId && np.FacilityId == facilityId, cancellationToken);

            if (preference == null)
                return await CreateDefaultPreferencesAsync(userId, facilityId, cancellationToken);

            preference.EmailNotificationsEnabled = preferences.EmailNotificationsEnabled;
            preference.EmailAddress = preferences.EmailAddress;
            preference.SMSNotificationsEnabled = preferences.SMSNotificationsEnabled;
            preference.PhoneNumber = preferences.PhoneNumber;
            preference.InAppNotificationsEnabled = preferences.InAppNotificationsEnabled;
            preference.HighPriorityFrequency = preferences.HighPriorityFrequency;
            preference.QuietHoursEnabled = preferences.QuietHoursEnabled;
            preference.QuietHoursStart = preferences.QuietHoursStart;
            preference.QuietHoursEnd = preferences.QuietHoursEnd;
            preference.PreferenceLastUpdatedDate = DateTime.UtcNow;
            preference.LastUpdatedByUserId = updatedByUserId;

            _context.NotificationPreferences.Update(preference);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(preference);
        }

        public async Task<bool> SetEmailNotificationsAsync(
            string userId,
            string facilityId,
            bool enabled,
            string emailAddress = null,
            CancellationToken cancellationToken = default)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId && np.FacilityId == facilityId, cancellationToken);

            if (preference == null)
                preference = await CreateDefaultPreferenceEntityAsync(userId, facilityId, cancellationToken);

            preference.EmailNotificationsEnabled = enabled;
            if (!string.IsNullOrEmpty(emailAddress))
                preference.EmailAddress = emailAddress;
            preference.PreferenceLastUpdatedDate = DateTime.UtcNow;

            _context.NotificationPreferences.Update(preference);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SetSMSNotificationsAsync(
            string userId,
            string facilityId,
            bool enabled,
            string phoneNumber = null,
            CancellationToken cancellationToken = default)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId && np.FacilityId == facilityId, cancellationToken);

            if (preference == null)
                preference = await CreateDefaultPreferenceEntityAsync(userId, facilityId, cancellationToken);

            preference.SMSNotificationsEnabled = enabled;
            if (!string.IsNullOrEmpty(phoneNumber))
                preference.PhoneNumber = phoneNumber;
            preference.PreferenceLastUpdatedDate = DateTime.UtcNow;

            _context.NotificationPreferences.Update(preference);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UnsubscribeFromCategoryAsync(
            string userId,
            string facilityId,
            string category,
            CancellationToken cancellationToken = default)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId && np.FacilityId == facilityId, cancellationToken);

            if (preference == null)
                return false;

            var categories = preference.UnsubscribedEmailCategories?.Split(',').ToList() ?? new List<string>();
            if (!categories.Contains(category))
                categories.Add(category);

            preference.UnsubscribedEmailCategories = string.Join(",", categories);
            preference.PreferenceLastUpdatedDate = DateTime.UtcNow;

            _context.NotificationPreferences.Update(preference);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UnsubscribeFromAllAsync(
            string userId,
            string unsubscribeToken,
            CancellationToken cancellationToken = default)
        {
            var preferences = await _context.NotificationPreferences
                .Where(np => np.UserId == userId && np.UnsubscribeToken == unsubscribeToken)
                .ToListAsync(cancellationToken);

            foreach (var preference in preferences)
            {
                preference.UnsubscribedFromAllEmails = true;
                preference.PreferenceLastUpdatedDate = DateTime.UtcNow;
            }

            _context.NotificationPreferences.UpdateRange(preferences);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<NotificationPreferenceDTO> CreateDefaultPreferencesAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default)
        {
            var entity = await CreateDefaultPreferenceEntityAsync(userId, facilityId, cancellationToken);
            return MapToDTO(entity);
        }

        public async Task<List<string>> GetRecipientsForAlertAsync(
            string facilityId,
            string alertType,
            NotificationFrequency minimumFrequency = NotificationFrequency.Immediate,
            CancellationToken cancellationToken = default)
        {
            var recipients = await _context.NotificationPreferences
                .Where(np => np.FacilityId == facilityId &&
                            !np.UnsubscribedFromAllEmails &&
                            np.EmailNotificationsEnabled)
                .Select(np => np.UserId)
                .ToListAsync(cancellationToken);

            return recipients;
        }

        private async Task<NotificationPreference> CreateDefaultPreferenceEntityAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default)
        {
            var preference = new NotificationPreference
            {
                UserId = userId,
                FacilityId = facilityId,
                EmailNotificationsEnabled = true,
                SMSNotificationsEnabled = false,
                InAppNotificationsEnabled = true,
                ShowNotificationBadge = true,
                PlaySoundForNotifications = true,
                ImmediateAlertFrequency = NotificationFrequency.Immediate,
                HighPriorityFrequency = NotificationFrequency.Immediate,
                MediumPriorityFrequency = NotificationFrequency.Daily,
                LowPriorityFrequency = NotificationFrequency.Weekly,
                QuietHoursEnabled = false,
                NotifyOnIncidents = true,
                NotifyOnHealthConcerns = true,
                NotifyOnSafeguardingIssues = true,
                NotifyOnReportGeneration = true,
                IncludeDetailedContent = true,
                IncludeActionableLinks = true,
                PreferenceLastUpdatedDate = DateTime.UtcNow,
                UnsubscribeToken = Guid.NewGuid().ToString()
            };

            _context.NotificationPreferences.Add(preference);
            await _context.SaveChangesAsync(cancellationToken);
            return preference;
        }

        private NotificationPreferenceDTO MapToDTO(NotificationPreference preference)
        {
            return new NotificationPreferenceDTO
            {
                Id = preference.Id,
                UserId = preference.UserId,
                FacilityId = preference.FacilityId,
                EmailNotificationsEnabled = preference.EmailNotificationsEnabled,
                EmailAddress = preference.EmailAddress,
                SMSNotificationsEnabled = preference.SMSNotificationsEnabled,
                PhoneNumber = preference.PhoneNumber,
                InAppNotificationsEnabled = preference.InAppNotificationsEnabled,
                HighPriorityFrequency = preference.HighPriorityFrequency,
                QuietHoursEnabled = preference.QuietHoursEnabled,
                QuietHoursStart = preference.QuietHoursStart,
                QuietHoursEnd = preference.QuietHoursEnd
            };
        }
    }
}
