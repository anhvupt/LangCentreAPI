using LangCentreAPI.Features.Course.UseCases;

namespace LangCentreAPI.Features.Course;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseServices(this IServiceCollection services)
    {
        services.AddScoped<IAddCourseHandler, AddCourseHandler>();
        services.AddScoped<IGetCourseListHandler, GetCourseListHandler>();

        return services;
    }
}