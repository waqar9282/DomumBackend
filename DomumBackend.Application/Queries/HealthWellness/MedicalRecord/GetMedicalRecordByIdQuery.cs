using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.HealthWellness.MedicalRecord
{
    public class GetMedicalRecordByIdQuery : IRequest<MedicalRecordResponseDTO>
    {
        public string Id { get; set; }

        public GetMedicalRecordByIdQuery(string id)
        {
            Id = id;
        }
    }
}
