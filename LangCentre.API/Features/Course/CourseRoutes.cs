using LangCentreAPI.Features.Course.UseCases;

namespace LangCentreAPI.Features.Course;

public static class CourseRoutes
{
    public static IEndpointRouteBuilder MapCourseRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGroup("courses")
            .MapGetCourseListRoute()
            .MapAddCourseRoute();
        
        return app;
    }
}