using DomumBackend.Application.Commands.Reporting;
using DomumBackend.Application.DTOs;
using DomumBackend.Application.Queries.Reporting;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomumBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Generates an incident report for a specific facility
        /// </summary>
        [HttpPost("incident")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> GenerateIncidentReport(
            [FromBody] GenerateIncidentReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Generates a health metrics report for a specific facility
        /// </summary>
        [HttpPost("health")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> GenerateHealthMetricsReport(
            [FromBody] GenerateHealthMetricsReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Generates a facility-wide report with KPIs and performance metrics
        /// </summary>
        [HttpPost("facility")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> GenerateFacilityReport(
            [FromBody] GenerateFacilityReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Gets incident trends for a facility
        /// </summary>
        [HttpGet("{facilityId}/trends")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> GetIncidentTrends(
            [FromRoute] string facilityId,
            [FromQuery] int daysBack = 30)
        {
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-daysBack);
            var query = new GetIncidentTrendQuery { FacilityId = facilityId, StartDate = startDate, EndDate = endDate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets facility KPIs
        /// </summary>
        [HttpGet("{facilityId}/kpis")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> GetFacilityKPIs(
            [FromRoute] string facilityId)
        {
            var query = new GetFacilityKPIsQuery { FacilityId = facilityId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Exports a report in the specified format (PDF, Excel, CSV)
        /// </summary>
        [HttpPost("{reportId}/export")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> ExportReport(
            [FromRoute] string reportId,
            [FromBody] ExportReportCommand command)
        {
            command.ReportId = reportId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Approves an incident report
        /// </summary>
        [HttpPost("incident/{reportId}/approve")]
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> ApproveIncidentReport(
            [FromRoute] string reportId,
            [FromBody] ApproveIncidentReportCommand command)
        {
            command.ReportId = reportId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Approves a health metrics report
        /// </summary>
        [HttpPost("health/{reportId}/approve")]
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> ApproveHealthMetricsReport(
            [FromRoute] string reportId,
            [FromBody] ApproveHealthMetricsReportCommand command)
        {
            command.ReportId = reportId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Approves a facility report
        /// </summary>
        [HttpPost("facility/{reportId}/approve")]
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(ReportGenerationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportGenerationResponseDTO>> ApproveFacilityReport(
            [FromRoute] string reportId,
            [FromBody] ApproveFacilityReportCommand command)
        {
            command.ReportId = reportId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
