using Autofac;
using CQRSApiTemplate.Api.Filters;
using CQRSApiTemplate.DI;
using CQRSApiTemplate.DI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace CQRSApiTemplate.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(Configuration["Serilog:FilePath"], rollingInterval: RollingInterval.Day)
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();

            services
                .AddDataAccessServices(Configuration.GetConnectionString("CQRSApiTemplate"))
                .AddDataPipelineServices()
                .AddSwagger(Configuration)
                .AddMvc()
                .AddHealthCheck();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger()
                    .UseSwaggerUI(setupAction =>
                    {
                        setupAction.SwaggerEndpoint(Configuration["Swagger:EndpointUrl"], Configuration["Swagger:ApiName"]);
                        setupAction.RoutePrefix = string.Empty;
                    });
            }

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = (httpContext, result) =>
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = result.Status == HealthStatus.Healthy ? (int)System.Net.HttpStatusCode.OK : (int)System.Net.HttpStatusCode.InternalServerError;
                    return httpContext.Response.WriteAsync(result.Status.ToString());
                }
            });
        }       
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
            });

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

}
