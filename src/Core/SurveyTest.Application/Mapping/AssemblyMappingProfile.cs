using System.Reflection;
using AutoMapper;

namespace SurveyTest.Application.Mapping;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
    {
        // Getting mapping types from assembly
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
            .Any(type => type.IsInterface && type == typeof(IMapping)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod("CreateMap");
            method?.Invoke(instance, new object[] { this });
        }

    }
}
