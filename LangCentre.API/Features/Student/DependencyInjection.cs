using LangCentreAPI.Features.Student.UseCases;

namespace LangCentreAPI.Features.Student;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentServices(this IServiceCollection services)
    {
        services.AddScoped<IEnrollStudentHandler, EnrollStudentHandler>();

        return services;
    }
}
