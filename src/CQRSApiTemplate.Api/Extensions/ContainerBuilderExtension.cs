using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Linq;
using System.Reflection;

namespace CQRSApiTemplate.Api.Extensions;

internal static class ContainerBuilderExtension
{
    internal static void RegisterServices(this ContainerBuilder containerBuilder, string assemblyName)
    {
        containerBuilder.RegisterAssemblyTypes(GetReferencedAssemblies(assemblyName))
            .Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    internal static void RegisterHandlers(this ContainerBuilder containerBuilder, string assemblyName)
    {
        containerBuilder.RegisterAssemblyTypes(GetReferencedAssemblies(assemblyName))
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
    }
 
    private static Assembly GetReferencedAssemblies(string assemblyName)
    {
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        foreach (var library in dependencies)
        {
            if (IsCandidateLibrary(library, assemblyName))
            {
                return Assembly.Load(new AssemblyName(library.Name));
            }
        }

        return null;
    }

    private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
    {
        return library.Name.EndsWith(assemblyName, StringComparison.OrdinalIgnoreCase);
    }
}
