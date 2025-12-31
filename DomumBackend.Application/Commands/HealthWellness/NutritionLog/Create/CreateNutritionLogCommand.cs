using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.NutritionLog
{
    public class CreateNutritionLogCommand : IRequest<string>
    {
        public string FacilityId { get; set; }
        public string YoungPersonId { get; set; }
        public DateTime LogDate { get; set; }
        public string MealType { get; set; }
        public string FoodDescription { get; set; }
        public decimal? PortionSize { get; set; }
        public string Appetite { get; set; }
        public string Notes { get; set; }
        public string RecordedByUserId { get; set; }
    }

    public class CreateNutritionLogCommandHandler : IRequestHandler<CreateNutritionLogCommand, string>
    {
        private readonly INutritionLogService _service;
        public CreateNutritionLogCommandHandler(INutritionLogService service) => _service = service;
        public async Task<string> Handle(CreateNutritionLogCommand r, CancellationToken ct)
            => await _service.CreateNutritionLogAsync(r.FacilityId, r.YoungPersonId, r.LogDate, r.MealType,
                r.FoodDescription, r.PortionSize, r.Appetite, r.Notes, r.RecordedByUserId, ct);
    }

    public class UpdateNutritionLogCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string FoodDescription { get; set; }
        public string Appetite { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateNutritionLogCommandHandler : IRequestHandler<UpdateNutritionLogCommand, int>
    {
        private readonly INutritionLogService _service;
        public UpdateNutritionLogCommandHandler(INutritionLogService service) => _service = service;
        public async Task<int> Handle(UpdateNutritionLogCommand r, CancellationToken ct)
            => await _service.UpdateNutritionLogAsync(r.Id, r.FoodDescription, r.Appetite, r.Notes, ct);
    }
}
