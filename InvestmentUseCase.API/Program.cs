using InvestmentUseCase.API.Extensions;
using InvestmentUseCase.API.Validations;
using InvestmentUseCase.Infra.Data.Context;
using InvestmentUseCase.Infra.IoC.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
builder.AddOpenTelemetry();
builder.Services.Inject(builder.Configuration);
builder.Services.AddScoped<ValidateModelAttribute>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();

})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            var response = new { Error = $"Ocorreu um erro! Motivo: {exceptionHandlerPathFeature?.Error.Message}" };
            await context.Response.WriteAsJsonAsync(response);
        }
    });
});

app.UseSerilogRequestLogging();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


if(!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}

app.Run();

public partial class Program { }