using Autofac;
using CQRSApiTemplate.Api.Extensions;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Application.Services;

namespace CQRSApiTemplate.Api.Modules;

public class ServiceModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterServices("CQRSApiTemplate.Application");
        builder.RegisterServices("CQRSApiTemplate.Infrastructure");
        builder.RegisterServices("CQRSApiTemplate.Domain");

        builder.RegisterType<HttpContextAccessor>()
            .As<IHttpContextAccessor>()
            .SingleInstance();

        builder.RegisterType<CurrentUser>()
            .As<ICurrentUser>()
            .SingleInstance();
    }
}
