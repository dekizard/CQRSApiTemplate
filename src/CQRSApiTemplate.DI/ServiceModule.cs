using Autofac;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Application.Services;
using CQRSApiTemplate.DI.Extensions;
using Microsoft.AspNetCore.Http;

namespace CQRSApiTemplate.DI
{
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
}
