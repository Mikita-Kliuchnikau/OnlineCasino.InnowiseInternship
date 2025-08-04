using AutoMapper;
using GamingService.Mapping.Interfaces;
using System.Reflection;

namespace GamingService.Mapping;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly) =>
        ApplyMappingFromAssambly(assembly);

    private void ApplyMappingFromAssambly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(nameof(IMapWith<object>.Mapping));
            methodInfo?.Invoke(instance, [this]);
        }
    }
}