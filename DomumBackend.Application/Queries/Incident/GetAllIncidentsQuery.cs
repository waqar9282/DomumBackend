using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.Incident
{
    /// <summary>
    /// Query to get all incidents
    /// </summary>
    public class GetAllIncidentsQuery : IRequest<List<IncidentResponseDTO>>
    {
        public required string FacilityId { get; set; }
    }

    public class GetAllIncidentsQueryHandler : IRequestHandler<GetAllIncidentsQuery, List<IncidentResponseDTO>>
    {
        private readonly IIncidentService _incidentService;

        public GetAllIncidentsQueryHandler(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<List<IncidentResponseDTO>> Handle(GetAllIncidentsQuery request, CancellationToken cancellationToken)
        {
            return await _incidentService.GetIncidentsByFacilityAsync(request.FacilityId, cancellationToken);
        }
    }
}
