using LangCentre.Infra.Persistent;
using LangCentreAPI.Features.Course;
using LangCentreAPI.Features.Student;
using MediatR;
using Serilog;

WebApplication.CreateBuilder(args)
    .AddLogger()
    .AddServices()
    .Build()
    .ConfigAndRunApplications();

internal static class ApiExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddPersistent(builder.Configuration);

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddCourseServices();
        builder.Services.AddStudentServices();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHealthChecks();

        return builder;
    }

    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        Log.Information("Starting web host.");
        builder.Host.UseSerilog();

        return builder;
    }

    public static void ConfigAndRunApplications(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }

        app.MapApiRoutes(); 
        app.Run();
    }

    private static void MapApiRoutes(this WebApplication app)
    {
        var api = app.MapGroup("api/v1")
            .WithOpenApi()
            .MapCourseRoutes()
            .MapStudentRoutes();
        
        app.MapHealthChecks("/health");
    }
}