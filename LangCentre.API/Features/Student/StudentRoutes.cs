using LangCentreAPI.Features.Student.UseCases;

namespace LangCentreAPI.Features.Student;

public static class StudentRoutes
{
    public static IEndpointRouteBuilder MapStudentRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGroup("students")
            .MapEnrollStudentRoute();

        return app;
    }
}