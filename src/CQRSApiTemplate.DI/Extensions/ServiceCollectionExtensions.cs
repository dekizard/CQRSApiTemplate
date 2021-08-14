using CQRSApiTemplate.Application.Common.Behaviour;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSApiTemplate.DI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContextPool<CQRSApiTemplateDbContext>(opt => opt.UseSqlServer(connectionString))
                .AddScoped<IApplicationReadDbFacade, ApplicationReadDbFacade>()
                .AddScoped<ICQRSApiTemplateDbContext>(provider => provider.GetService<CQRSApiTemplateDbContext>());
        }

        public static IServiceCollection AddDataPipelineServices(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>))
                .AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
               .AddDbContextCheck<CQRSApiTemplateDbContext>();

            return services;
        }
    }
}
