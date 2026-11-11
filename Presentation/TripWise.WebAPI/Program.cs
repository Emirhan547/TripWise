using Scalar.AspNetCore;
using TripWise.Application.Extensions;
using TripWise.Persistence.Extensions;
using TripWise.WebAPI.Endpoints;
using TripWise.WebAPI.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddPersistenceExt(builder.Configuration).AddApplicationExt();

var app = builder.Build();

app.UseExceptionHandler();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapTripEndpoints();

app.Run();