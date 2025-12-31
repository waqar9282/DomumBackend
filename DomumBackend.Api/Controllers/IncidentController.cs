using DomumBackend.Application.Commands.Incident;
using DomumBackend.Application.Queries.Incident;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncidentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncidentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new incident report
        /// Requires Authentication
        /// </summary>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var incidentId = await _mediator.Send(command);
                return Ok(new { incidentId, message = "Incident created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all incidents for a facility
        /// Requires Authentication
        /// </summary>
        [HttpGet("GetByFacility/{facilityId}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetIncidentsByFacility(string facilityId)
        {
            try
            {
                var incidents = await _mediator.Send(new GetAllIncidentsQuery { FacilityId = facilityId });
                return Ok(incidents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get incident details by ID
        /// Requires Authentication
        /// </summary>
        [HttpGet("GetById/{incidentId}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetIncidentById(string incidentId)
        {
            try
            {
                var incident = await _mediator.Send(new GetIncidentByIdQuery { IncidentId = incidentId });
                if (incident == null)
                    return NotFound(new { message = "Incident not found" });

                return Ok(incident);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an incident report
        /// Requires Authentication
        /// </summary>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> UpdateIncident([FromBody] UpdateIncidentCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _mediator.Send(command);
                if (result == 0)
                    return NotFound(new { message = "Incident not found" });

                return Ok(new { message = "Incident updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Close/resolve an incident
        /// Requires Authentication
        /// </summary>
        [HttpPut("Close/{incidentId}")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> CloseIncident(string incidentId, [FromBody] CloseIncidentCommand command)
        {
            if (command.Id != incidentId)
                return BadRequest(new { message = "Incident ID mismatch" });

            try
            {
                var result = await _mediator.Send(command);
                if (result == 0)
                    return NotFound(new { message = "Incident not found" });

                return Ok(new { message = "Incident closed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
