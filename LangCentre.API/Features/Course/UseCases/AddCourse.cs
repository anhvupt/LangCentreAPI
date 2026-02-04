using LangCentre.Domain.Enums;
using LangCentre.Infra.Persistent;

namespace LangCentreAPI.Features.Course.UseCases;

public record AddCourseRequest
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
            IAddCourseHandler handler,
            CancellationToken ct) =>
        {
            var id = await handler.Handle(request, ct);
            return Results.Ok(new { id });
        });
    }
}

public interface IAddCourseHandler
{
    Task<Guid> Handle(AddCourseRequest request, CancellationToken ct);
};

public class AddCourseHandler(LangCentreDbContext dbContext) : IAddCourseHandler
{
    public async Task<Guid> Handle(AddCourseRequest request, CancellationToken ct)
    {
        var entity = LangCentre.Domain.Entities.Course.Create(request.Name, request.Level);

        dbContext.Courses.Add(entity);
        await dbContext.SaveChangesAsync(ct);

        return entity.Id;
    }
}