using LangCentre.Infra.Persistent;

namespace LangCentreAPI.Features.Student.UseCases;

public record EnrollStudentRequest
{
    public Guid StudentId { get; init; }
    public Guid ClassId { get; init; }
}

public static class EnrollStudentRoute
{
    public static void MapEnrollStudentRoute(this IEndpointRouteBuilder api)
    {
        api.MapPost("/{studentId:guid}/enrol", async (
            Guid studentId,
            EnrollStudentBody body,
            IEnrollStudentHandler handler,
            CancellationToken ct) =>
        {
            var request = new EnrollStudentRequest { StudentId = studentId, ClassId = body.ClassId };
            var id = await handler.Handle(request, ct);
            return Results.Ok(new { id });
        });
    }
}

public record EnrollStudentBody
{
    public Guid ClassId { get; init; }
}

public interface IEnrollStudentHandler
{
    Task<Guid> Handle(EnrollStudentRequest request, CancellationToken ct);
}

public class EnrollStudentHandler(LangCentreDbContext dbContext) : IEnrollStudentHandler
{
    public async Task<Guid> Handle(EnrollStudentRequest request, CancellationToken ct)
    {
        var enrolment = LangCentre.Domain.Entities.Enrolment.Create(request.StudentId, request.ClassId);

        dbContext.Enrolments.Add(enrolment);
        await dbContext.SaveChangesAsync(ct);

        return enrolment.Id;
    }
}
