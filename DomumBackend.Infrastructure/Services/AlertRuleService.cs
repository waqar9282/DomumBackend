#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class AlertRuleService : IAlertRuleService
    {
        private readonly ApplicationDbContext _context;

        public AlertRuleService(ApplicationDbContext context)
            => _context = context;

        public async Task<AlertRuleDTO> CreateAlertRuleAsync(
            string facilityId,
            string ruleName,
            string alertType,
            string triggerCondition,
            int thresholdValue,
            int timeframeMinutes,
            string recipientRoles,
            string notificationChannels,
            string createdByUserId,
            CancellationToken cancellationToken = default)
        {
            var alertRule = new AlertRule
            {
                FacilityId = facilityId,
                RuleName = ruleName,
                AlertType = alertType,
                TriggerCondition = triggerCondition,
                ThresholdValue = thresholdValue,
                TimeframeMinutes = timeframeMinutes,
                RecipientRoles = recipientRoles,
                NotificationChannels = notificationChannels,
                IsActive = true,
                CreatedByUserId_Date = DateTime.UtcNow,
                CreatedByUserId = createdByUserId
            };

            _context.AlertRules.Add(alertRule);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(alertRule);
        }

        public async Task<AlertRuleDTO> UpdateAlertRuleAsync(
            long alertRuleId,
            string ruleName,
            string triggerCondition,
            int thresholdValue,
            int timeframeMinutes,
            bool isActive,
            string updatedByUserId,
            CancellationToken cancellationToken = default)
        {
            var alertRule = await _context.AlertRules.FindAsync(new object[] { alertRuleId }, cancellationToken);
            if (alertRule == null)
                throw new KeyNotFoundException($"Alert rule {alertRuleId} not found");

            alertRule.RuleName = ruleName;
            alertRule.TriggerCondition = triggerCondition;
            alertRule.ThresholdValue = thresholdValue;
            alertRule.TimeframeMinutes = timeframeMinutes;
            alertRule.IsActive = isActive;
            alertRule.LastModifiedDate = DateTime.UtcNow;
            alertRule.LastModifiedByUserId = updatedByUserId;

            _context.AlertRules.Update(alertRule);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToDTO(alertRule);
        }

        public async Task<List<AlertRuleDTO>> GetAlertRulesByFacilityAsync(
            string facilityId,
            bool activeOnly = true,
            CancellationToken cancellationToken = default)
        {
            var query = _context.AlertRules.Where(ar => ar.FacilityId == facilityId);
            
            if (activeOnly)
                query = query.Where(ar => ar.IsActive);

            var alertRules = await query.ToListAsync(cancellationToken);
            return alertRules.Select(MapToDTO).ToList();
        }

        public async Task<AlertRuleDTO> GetAlertRuleByIdAsync(
            long alertRuleId,
            CancellationToken cancellationToken = default)
        {
            var alertRule = await _context.AlertRules.FindAsync(new object[] { alertRuleId }, cancellationToken);
            if (alertRule == null)
                throw new KeyNotFoundException($"Alert rule {alertRuleId} not found");

            return MapToDTO(alertRule);
        }

        public async Task<bool> DeleteAlertRuleAsync(
            long alertRuleId,
            CancellationToken cancellationToken = default)
        {
            var alertRule = await _context.AlertRules.FindAsync(new object[] { alertRuleId }, cancellationToken);
            if (alertRule == null)
                return false;

            _context.AlertRules.Remove(alertRule);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<AlertTriggeredResponseDTO> TriggerAlertRuleAsync(
            long alertRuleId,
            string triggerEntityId = null,
            string triggerDetails = null,
            CancellationToken cancellationToken = default)
        {
            var alertRule = await _context.AlertRules.FindAsync(new object[] { alertRuleId }, cancellationToken);
            if (alertRule == null)
                throw new KeyNotFoundException($"Alert rule {alertRuleId} not found");

            alertRule.TimesTriggered++;
            alertRule.LastTriggeredDate = DateTime.UtcNow;

            _context.AlertRules.Update(alertRule);
            await _context.SaveChangesAsync(cancellationToken);

            // Get recipients based on roles
            var recipientUserIds = new List<string>();
            // TODO: Implement recipient lookup based on roles and facility

            return new AlertTriggeredResponseDTO
            {
                AlertRuleId = alertRuleId,
                AlertType = alertRule.AlertType,
                NotificationsSent = recipientUserIds.Count,
                RecipientUserIds = recipientUserIds,
                TriggeredDate = DateTime.UtcNow,
                TriggerDetails = triggerDetails
            };
        }

        public async Task<bool> SuspendAlertRuleAsync(
            long alertRuleId,
            DateTime suspensionUntilDate,
            CancellationToken cancellationToken = default)
        {
            var alertRule = await _context.AlertRules.FindAsync(new object[] { alertRuleId }, cancellationToken);
            if (alertRule == null)
                return false;

            alertRule.TemporarilySuspended = true;
            alertRule.SuspensionUntilDate = suspensionUntilDate;

            _context.AlertRules.Update(alertRule);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<AlertRuleDTO>> CheckAndTriggerApplicableAlertsAsync(
            string facilityId,
            string triggerType,
            string triggerEntityId,
            CancellationToken cancellationToken = default)
        {
            var applicableRules = await _context.AlertRules
                .Where(ar => ar.FacilityId == facilityId &&
                            ar.IsActive &&
                            !ar.TemporarilySuspended &&
                            ar.AffectedEntityType == triggerType)
                .ToListAsync(cancellationToken);

            var triggeredRules = new List<AlertRuleDTO>();
            foreach (var rule in applicableRules)
            {
                await TriggerAlertRuleAsync(rule.Id, triggerEntityId, null, cancellationToken);
                triggeredRules.Add(MapToDTO(rule));
            }

            return triggeredRules;
        }

        private AlertRuleDTO MapToDTO(AlertRule alertRule)
        {
            return new AlertRuleDTO
            {
                Id = alertRule.Id,
                FacilityId = alertRule.FacilityId,
                RuleName = alertRule.RuleName,
                AlertType = alertRule.AlertType,
                IsActive = alertRule.IsActive,
                ThresholdValue = alertRule.ThresholdValue,
                TimeframeMinutes = alertRule.TimeframeMinutes,
                NotificationChannels = alertRule.NotificationChannels,
                TimesTriggered = alertRule.TimesTriggered,
                LastTriggeredDate = alertRule.LastTriggeredDate
            };
        }
    }
}
