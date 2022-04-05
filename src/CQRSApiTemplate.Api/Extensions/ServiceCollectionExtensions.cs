using CQRSApiTemplate.Api.Filters;
using CQRSApiTemplate.Application.Common.Behaviour;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CQRSApiTemplate.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilterAttribute));
        });

        return services;
    }

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

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc(configuration["Swagger:Name"],
            new OpenApiInfo
            {
                Title = configuration["Swagger:Title"],
                Description = configuration["Swagger:Description"]
            });
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            setupAction.IncludeXmlComments(xmlCommentsFullPath);
        });
    }
}
