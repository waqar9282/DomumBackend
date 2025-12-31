using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.Incident
{
    /// <summary>
    /// Command to close/resolve an incident
    /// </summary>
    public class CloseIncidentCommand : IRequest<int>
    {
        public required string Id { get; set; }
        public required string ClosingNotes { get; set; }
    }

    public class CloseIncidentCommandHandler : IRequestHandler<CloseIncidentCommand, int>
    {
        private readonly IIncidentService _incidentService;

        public CloseIncidentCommandHandler(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<int> Handle(CloseIncidentCommand request, CancellationToken cancellationToken)
        {
            return await _incidentService.CloseIncidentAsync(request.Id, request.ClosingNotes, cancellationToken);
        }
    }
}
