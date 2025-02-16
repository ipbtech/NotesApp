using NotesApp.DAL;
using NotesApp.Application;
using NotesApp.Api.Extensions;
using NotesApp.AuthService;

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
builder.Services.AddAuthService(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddApplicationServices();

// global exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddSwaggerGen();

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
