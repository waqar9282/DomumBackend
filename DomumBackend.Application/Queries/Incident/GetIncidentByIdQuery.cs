using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.Incident
{
    /// <summary>
    /// Query to get incident details by ID
    /// </summary>
    public class GetIncidentByIdQuery : IRequest<IncidentDetailsResponseDTO>
    {
        public string IncidentId { get; set; }
    }

    public class GetIncidentByIdQueryHandler : IRequestHandler<GetIncidentByIdQuery, IncidentDetailsResponseDTO>
    {
        private readonly IIncidentService _incidentService;

        public GetIncidentByIdQueryHandler(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<IncidentDetailsResponseDTO> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _incidentService.GetIncidentByIdAsync(request.IncidentId, cancellationToken);
        }
    }
}
