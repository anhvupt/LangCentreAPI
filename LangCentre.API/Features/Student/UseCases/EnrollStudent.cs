using LangCentre.Infra.Persistent;
using MediatR;

namespace LangCentreAPI.Features.Student.UseCases;

public record EnrollStudentRequest : IRequest<Guid>
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
            IMediator mediator,
            CancellationToken ct) =>
        {
            var request = new EnrollStudentRequest { StudentId = studentId, ClassId = body.ClassId };
            var id = await mediator.Send(request, ct);
            return Results.Ok(new { id });
        });
    }
}

public record EnrollStudentBody
{
    public Guid ClassId { get; init; }
}

public class EnrollStudentHandler(LangCentreDbContext dbContext) : IRequestHandler<EnrollStudentRequest, Guid>
{
    public async Task<Guid> Handle(EnrollStudentRequest request, CancellationToken ct)
    {
        var enrolment = LangCentre.Domain.Entities.Enrolment.Create(request.StudentId, request.ClassId);

        dbContext.Enrolments.Add(enrolment);
        await dbContext.SaveChangesAsync(ct);

        return enrolment.Id;
    }
}
