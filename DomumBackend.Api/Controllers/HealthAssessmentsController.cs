using DomumBackend.Application.Commands.HealthWellness.HealthAssessment;
using DomumBackend.Application.Queries.HealthWellness.HealthAssessment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HealthAssessmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HealthAssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateHealthAssessment([FromBody] CreateHealthAssessmentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var assessmentId = await _mediator.Send(command);
                return Ok(new { assessmentId, message = "Health assessment created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateHealthAssessment([FromBody] UpdateHealthAssessmentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Health assessment updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetHealthAssessmentById(string id)
        {
            try
            {
                var assessment = await _mediator.Send(new GetHealthAssessmentByIdQuery(id));
                if (assessment == null)
                    return NotFound(new { message = "Health assessment not found" });
                return Ok(assessment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetHealthAssessmentsByYoungPerson(string youngPersonId)
        {
            try
            {
                var assessments = await _mediator.Send(new GetHealthAssessmentsByYoungPersonQuery(youngPersonId));
                return Ok(assessments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByType/{youngPersonId}/{assessmentType}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetHealthAssessmentsByType(string youngPersonId, int assessmentType)
        {
            try
            {
                var assessments = await _mediator.Send(new GetHealthAssessmentsByTypeQuery(youngPersonId, assessmentType));
                return Ok(assessments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
