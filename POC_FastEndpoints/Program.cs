using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using POC_FastEndpoints.Extensions;
using POC_FastEndpoints.Processors;
using POC_FastEndpoints.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Services
    .AddAuth()
    .AddAuthorization()
    .AddFastEndpoints()
    .AddScoped<IHelloService, HelloService>();

WebApplication app = builder.Build();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c =>
    {
        c.Endpoints.RoutePrefix = "hubfinance";
        c.Endpoints.Configurator = ep =>
        {
            ep.PreProcessor<ClientIdChecker>(Order.Before);
        };
    });

app.Run();