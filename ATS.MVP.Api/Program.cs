using ATS.MVP.Api.Extensions;
using ATS.MVP.Api.Middlewares.Exceptions;
using ATS.MVP.Application;
using ATS.MVP.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "ATS MVP", Version = "v1" });
});

builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddApplicationLayer();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddATSMVPEndpoints();

app.UseExceptionHandler();

app.Run();