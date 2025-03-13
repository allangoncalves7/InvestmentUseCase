using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace InvestmentUseCase.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .Enrich.FromLogContext()
             .CreateLogger();

            builder.Host.UseSerilog();


            return builder;

        }

        public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenTelemetry()
             .ConfigureResource(resource => resource
                 .AddService("InvestmentUseCase.API"))
             .WithMetrics(metrics => metrics
                 .AddAspNetCoreInstrumentation()
                 .AddHttpClientInstrumentation()
                 .AddRuntimeInstrumentation()
                 .AddProcessInstrumentation()
                 .AddPrometheusExporter()
                 .AddOtlpExporter())
             .WithTracing(tracing => tracing
                 .AddAspNetCoreInstrumentation()
                 .AddHttpClientInstrumentation());

            return builder;
        }
    }
}
