using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using TeamCounter.Application.Dtos;
using TeamCounter.Application.Handlers.CreateTeam;
using TeamCounter.Application.Services;
using TeamCounter.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// register fluent validators
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Team Counter API",
        Version = "v1",
        Description = "API for tracking steps of teams"
    });
});

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<CreateTeamCommand>();
});

// register services
builder.Services.AddSingleton<ITeamService, TeamService>();

// register validators
builder.Services.AddScoped<IValidator<TeamCreateDto>, TeamCreateDtoValidator>();
builder.Services.AddScoped<IValidator<CounterCreateDto>, CounterCreateDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();