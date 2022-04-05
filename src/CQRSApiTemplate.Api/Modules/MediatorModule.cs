using Autofac;
using CQRSApiTemplate.Api.Extensions;
using MediatR;
using System.Reflection;

namespace CQRSApiTemplate.Api.Modules;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterHandlers("CQRSApiTemplate.Application");

        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
        });
    }
}
