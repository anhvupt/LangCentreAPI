using LangCentre.Domain.Events;
using LangCentre.Infra.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LangCentre.Infra.Persistent;

public static class AddPersistentExtension
{
    public static void AddPersistent(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IDomainEventDispatcher, AuditDomainEventHandler>();

        services.AddDbContext<LangCentreDbContext>(opt =>
        {
            opt.UseMySQL(config.GetConnectionString("DefaultConnection")!);
        });
    }
}