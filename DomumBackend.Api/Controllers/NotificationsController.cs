using DomumBackend.Application.Commands.Notifications;
using DomumBackend.Application.DTOs;
using DomumBackend.Application.Queries.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Send a notification to a single user
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(typeof(NotificationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NotificationResponseDTO>> SendNotification(
            [FromBody] SendNotificationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Send notifications to multiple users
        /// </summary>
        [HttpPost("send-bulk")]
        [ProducesResponseType(typeof(BulkNotificationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BulkNotificationResponseDTO>> SendBulkNotifications(
            [FromBody] SendBulkNotificationsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get notifications for current user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<NotificationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<NotificationDTO>>> GetNotifications(
            [FromQuery] string facilityId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            // Get user ID from claims
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetUserNotificationsQuery
            {
                UserId = userId,
                FacilityId = facilityId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get unread notifications for current user
        /// </summary>
        [HttpGet("unread")]
        [ProducesResponseType(typeof(List<NotificationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<NotificationDTO>>> GetUnreadNotifications(
            [FromQuery] string facilityId)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetUnreadNotificationsQuery
            {
                UserId = userId,
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Mark a notification as read
        /// </summary>
        [HttpPut("{notificationId}/read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MarkAsRead(
            [FromRoute] long notificationId)
        {
            var command = new MarkNotificationAsReadCommand { NotificationId = notificationId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Mark all notifications as read for current user
        /// </summary>
        [HttpPut("read-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MarkAllAsRead(
            [FromQuery] string facilityId)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new MarkAllNotificationsAsReadCommand
            {
                UserId = userId,
                FacilityId = facilityId
            };

            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete a notification
        /// </summary>
        [HttpDelete("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteNotification(
            [FromRoute] long notificationId)
        {
            var command = new DeleteNotificationCommand { NotificationId = notificationId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Get user notification preferences
        /// </summary>
        [HttpGet("preferences")]
        [ProducesResponseType(typeof(NotificationPreferenceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NotificationPreferenceDTO>> GetPreferences(
            [FromQuery] string facilityId)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetUserPreferencesQuery
            {
                UserId = userId,
                FacilityId = facilityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update user notification preferences
        /// </summary>
        [HttpPut("preferences")]
        [ProducesResponseType(typeof(NotificationPreferenceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NotificationPreferenceDTO>> UpdatePreferences(
            [FromQuery] string facilityId,
            [FromBody] NotificationPreferenceDTO preferences)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new UpdateNotificationPreferencesCommand
            {
                UserId = userId,
                FacilityId = facilityId,
                Preferences = preferences,
                UpdatedByUserId = userId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
