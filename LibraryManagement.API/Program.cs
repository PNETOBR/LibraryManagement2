using LibraryManagement.API.ExceptionHandler;
using LibraryManagement.Application.Models.Config;
using LibraryManagement.Application.Models.Services.Interfaces;
using LibraryManagement.Application.Models.Services;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LoansBookLimitedConfig>(builder.Configuration.GetSection("LoansBookLimitedConfig"));
//builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

var connectionString = builder.Configuration.GetConnectionString("LibraryCs");
builder.Services.AddDbContext<LibraryManagementDbContext>(o => o.UseSqlite(connectionString));

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
