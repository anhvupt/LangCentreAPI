using LangCentre.Domain.Enums;
using LangCentre.Infra.Persistent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LangCentreAPI.Features.Course.UseCases;

public record GetCourseListRequest : IRequest<List<GetCourseItemListResponse>>;
public record GetCourseItemListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public LangLevel Level { get; set; }
}

public static class GetCourseListRoute
{
    public static IEndpointRouteBuilder MapGetCourseListRoute(this IEndpointRouteBuilder api)
    {
        api.MapGet("/", async ([AsParameters] GetCourseListRequest request, IMediator mediator, CancellationToken ct) =>
        {
            var data = await mediator.Send(request, ct);
            return Results.Ok(new { data });
        });
        
        return api;
    }
}

public class GetCourseListHandler(LangCentreDbContext dbContext) : IRequestHandler<GetCourseListRequest, List<GetCourseItemListResponse>>
{
    public async Task<List<GetCourseItemListResponse>> Handle(GetCourseListRequest request, CancellationToken ct)
    {
        return await dbContext.Courses.AsNoTracking()
            .OrderBy(x => x.CreatedAt)
            .Select(x => new GetCourseItemListResponse
            {
                Id = x.Id,
                Name = x.Name,
                Level = x.Level
            })
            .ToListAsync(ct);
    }
}