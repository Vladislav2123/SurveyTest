using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyTest.Application.Abstraction;

namespace SurveyTest.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration config)
    {
        Console.WriteLine($"Connection string: {config.GetConnectionString("PostgreSQL")}");

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(config.GetConnectionString("PostgreSQL")));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetService<ApplicationDbContext>());

        return services;
    }
}
