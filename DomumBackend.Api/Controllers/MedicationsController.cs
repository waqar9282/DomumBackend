using DomumBackend.Application.Commands.HealthWellness.Medication;
using DomumBackend.Application.Queries.HealthWellness.Medication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateMedication([FromBody] CreateMedicationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var medicationId = await _mediator.Send(command);
                return Ok(new { medicationId, message = "Medication created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateMedication([FromBody] UpdateMedicationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Medication updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Discontinue/{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> DiscontinueMedication(string id, [FromBody] DiscontinueMedicationCommand command)
        {
            command.Id = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Medication discontinued successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetMedicationById(string id)
        {
            try
            {
                var medication = await _mediator.Send(new GetMedicationByIdQuery(id));
                if (medication == null)
                    return NotFound(new { message = "Medication not found" });
                return Ok(medication);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetMedicationsByYoungPerson(string youngPersonId)
        {
            try
            {
                var medications = await _mediator.Send(new GetMedicationsByYoungPersonQuery(youngPersonId));
                return Ok(medications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetActive/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetActiveMedicationsByYoungPerson(string youngPersonId)
        {
            try
            {
                var medications = await _mediator.Send(new GetActiveMedicationsByYoungPersonQuery(youngPersonId));
                return Ok(medications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
