using DomumBackend.Application.Commands.HealthWellness.MedicalRecord;
using DomumBackend.Application.Queries.HealthWellness.MedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var recordId = await _mediator.Send(command);
                return Ok(new { recordId, message = "Medical record created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateMedicalRecord([FromBody] UpdateMedicalRecordCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { result, message = "Medical record updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetMedicalRecordById(string id)
        {
            try
            {
                var record = await _mediator.Send(new GetMedicalRecordByIdQuery(id));
                if (record == null)
                    return NotFound(new { message = "Medical record not found" });
                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByYoungPerson/{youngPersonId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetMedicalRecordsByYoungPerson(string youngPersonId)
        {
            try
            {
                var records = await _mediator.Send(new GetMedicalRecordsByYoungPersonQuery(youngPersonId));
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetByFacility/{facilityId}")]
        [ProducesResponseType(typeof(List<object>), 200)]
        public async Task<IActionResult> GetMedicalRecordsByFacility(string facilityId)
        {
            try
            {
                var records = await _mediator.Send(new GetMedicalRecordsByFacilityQuery(facilityId));
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
