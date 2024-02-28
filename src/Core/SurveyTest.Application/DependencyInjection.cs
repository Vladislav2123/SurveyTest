using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SurveyTest.Application.Mapping;
using FluentValidation;
using MediatR;
using SurveyTest.Application.Validation;

namespace SurveyTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Adding assembly AutoMapper profile
        services.AddAutoMapper(config =>
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));

        // Adding valitation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
