using DomumBackend.Application.Commands.HealthWellness.NutritionLog;
using DomumBackend.Application.Queries.HealthWellness.NutritionLog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NutritionLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NutritionLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateNutritionLog([FromBody] CreateNutritionLogCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var logId = await _mediator.Send(command);
                return Ok(new { logId, message = "Nutrition log created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateNutritionLog([FromBody] UpdateNutritionLogCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Nutrition log updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetNutritionLogById(string id)
        {
            try
            {
                var log = await _mediator.Send(new GetNutritionLogByIdQuery(id));
                if (log == null)
                    return NotFound(new { message = "Nutrition log not found" });
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetNutritionLogsByYoungPerson(string youngPersonId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                var logs = await _mediator.Send(new GetNutritionLogsByYoungPersonQuery(youngPersonId, fromDate, toDate));
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByFacility/{facilityId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetNutritionLogsByFacility(string facilityId, [FromQuery] DateTime logDate)
        {
            try
            {
                var logs = await _mediator.Send(new GetNutritionLogsByFacilityQuery(facilityId, logDate));
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
