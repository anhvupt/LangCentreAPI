using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LangCentre.Infra.Persistent;

public static class AddPersistentExtension
{
    public static void AddPersistent(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<LangCentreDbContext>(opt =>
        {
            opt.UseMySQL(config.GetConnectionString("DefaultConnection")!);
        });
    }
}