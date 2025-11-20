using LangCentre.Infra.Persistent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Build().ConfigApplications().Run();

internal static class ApiExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistent(configuration);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static WebApplication ConfigApplications(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapApiRoutes();

        return app;
    }

    private static void MapApiRoutes(this WebApplication app)
    {
        var api = app.MapGroup("api/v1")
            .WithOpenApi();

        api.MapGet("/", () => Results.Ok("Hi"));
    }
}