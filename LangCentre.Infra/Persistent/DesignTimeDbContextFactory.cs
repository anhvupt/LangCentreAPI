using LangCentre.Domain.Events;
using LangCentre.Infra.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LangCentre.Infra.Persistent;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LangCentreDbContext>
{
    public LangCentreDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        if (!File.Exists(Path.Combine(basePath, "appsettings.Development.json")))
            basePath = Path.Combine(basePath, "..", "LangCentre.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<LangCentreDbContext>();
        optionsBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);

        return new LangCentreDbContext(optionsBuilder.Options, new NoOpDomainEventDispatcher());
    }
}
