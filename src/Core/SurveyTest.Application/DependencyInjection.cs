using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SurveyTest.Application.Mapping;

namespace SurveyTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Adding assembly AutoMapper profile
        services.AddAutoMapper(config =>
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));

        return services;
    }
}
