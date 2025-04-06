using Repositories.EfCore;
using System.Reflection;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMvcClient", policy =>
    {
        policy.WithOrigins("https://localhost:7257")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddCustomServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowMvcClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
