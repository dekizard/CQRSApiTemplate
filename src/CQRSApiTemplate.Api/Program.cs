using Autofac;
using Autofac.Extensions.DependencyInjection;
using CQRSApiTemplate.Api.Extensions;
using CQRSApiTemplate.Api.Modules;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog();

Log.Logger = new LoggerConfiguration()
   .WriteTo.File(configuration["Serilog:FilePath"], rollingInterval: RollingInterval.Day)
   .ReadFrom.Configuration(configuration)
   .CreateLogger();

builder.Services
    .AddDataAccessServices(configuration.GetConnectionString("CQRSApiTemplate"))
    .AddDataPipelineServices()
    .AddSwagger(configuration)
    .AddServices()
    .AddHealthCheck();

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule<AutofacModule>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger()
        .UseSwaggerUI(setupAction =>
        {
            setupAction.SwaggerEndpoint(configuration["Swagger:EndpointUrl"], configuration["Swagger:ApiName"]);
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

app.Run();