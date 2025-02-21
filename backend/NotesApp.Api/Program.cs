using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using NotesApp.Api.Extensions;
using NotesApp.Application;
using NotesApp.Auth;
using NotesApp.Auth.Dto;
using NotesApp.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// lowercase paths
builder.Services.Configure<RouteOptions>(opt => {
    opt.LowercaseUrls = true;
    opt.LowercaseQueryStrings = true;
});

// application services
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddApplicationServices();

// auto fluent-validation
builder.Services.AddFluentValidationAutoValidation();

// global exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddNotesAppSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
