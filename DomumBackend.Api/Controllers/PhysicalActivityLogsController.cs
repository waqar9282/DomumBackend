using DomumBackend.Application.Commands.HealthWellness.PhysicalActivityLog;
using DomumBackend.Application.Queries.HealthWellness.PhysicalActivityLog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhysicalActivityLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhysicalActivityLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreatePhysicalActivityLog([FromBody] CreatePhysicalActivityLogCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var logId = await _mediator.Send(command);
                return Ok(new { logId, message = "Physical activity log created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdatePhysicalActivityLog([FromBody] UpdatePhysicalActivityLogCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Physical activity log updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetPhysicalActivityLogById(string id)
        {
            try
            {
                var log = await _mediator.Send(new GetPhysicalActivityLogByIdQuery(id));
                if (log == null)
                    return NotFound(new { message = "Physical activity log not found" });
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetPhysicalActivityLogsByYoungPerson(string youngPersonId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                var logs = await _mediator.Send(new GetPhysicalActivityLogsByYoungPersonQuery(youngPersonId, fromDate, toDate));
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByType/{youngPersonId}/{activityType}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetPhysicalActivityLogsByType(string youngPersonId, int activityType)
        {
            try
            {
                var logs = await _mediator.Send(new GetPhysicalActivityLogsByTypeQuery(youngPersonId, activityType));
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
