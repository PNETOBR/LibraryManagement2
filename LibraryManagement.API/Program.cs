using LibraryManagement.API.ExceptionHandler;
using LibraryManagement.Application.Models;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LoansBookLimitedConfig>(builder.Configuration.GetSection("LoansBookLimitedConfig"));
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

//builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryCs")));

var connectionString = builder.Configuration.GetConnectionString("LibraryCs");
builder.Services.AddDbContext<LibraryManagementDbContext>(o => o.UseSqlite(connectionString));


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
