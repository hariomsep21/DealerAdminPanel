using Admin.Services.CarService.BuisnessLayer;
using Admin.Services.CarService.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Admin.Services.CarService.DataLayer.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DealerApisemiFinalContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICarServices, CarServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
