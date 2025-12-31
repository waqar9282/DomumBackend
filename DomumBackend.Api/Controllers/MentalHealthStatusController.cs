using DomumBackend.Application.Commands.HealthWellness.MentalHealthCheckIn;
using DomumBackend.Application.Queries.HealthWellness.MentalHealthCheckIn;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MentalHealthStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MentalHealthStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateMentalHealthStatus([FromBody] CreateMentalHealthCheckInCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var statusId = await _mediator.Send(command);
                return Ok(new { statusId, message = "Mental health status created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateMentalHealthStatus([FromBody] UpdateMentalHealthCheckInCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Mental health status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetMentalHealthStatusById(string id)
        {
            try
            {
                var status = await _mediator.Send(new GetMentalHealthCheckInByIdQuery(id));
                if (status == null)
                    return NotFound(new { message = "Mental health status not found" });
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetMentalHealthStatusByYoungPerson(string youngPersonId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                var statuses = await _mediator.Send(new GetMentalHealthCheckInsByYoungPersonQuery(youngPersonId, fromDate, toDate));
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetMoodHistory/{facilityId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetMoodHistory(string facilityId, [FromQuery] int days = 7)
        {
            try
            {
                var history = await _mediator.Send(new GetRecentMentalHealthCheckInsQuery(facilityId, days));
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
