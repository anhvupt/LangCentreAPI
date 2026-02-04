using LangCentre.Domain.Enums;
using LangCentre.Infra.Persistent;
using MediatR;

namespace LangCentreAPI.Features.Course.UseCases;

public record AddCourseRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public LangLevel Level { get; set; }
}

public static class AddCourseRoute
{
    public static void MapAddCourseRoute(this IEndpointRouteBuilder api)
    {
        api.MapPost("/", async (
            AddCourseRequest request,
            IMediator mediator,
            CancellationToken ct) =>
        {
            var id = await mediator.Send(request, ct);
            return Results.Ok(new { id });
        });
    }
}

public class AddCourseHandler(LangCentreDbContext dbContext) : IRequestHandler<AddCourseRequest, Guid>
{
    public async Task<Guid> Handle(AddCourseRequest request, CancellationToken ct)
    {
        var entity = LangCentre.Domain.Entities.Course.Create(request.Name, request.Level);

        dbContext.Courses.Add(entity);
        await dbContext.SaveChangesAsync(ct);

        return entity.Id;
    }
}