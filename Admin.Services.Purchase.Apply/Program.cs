using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.BusinessLayer.Services;
using Admin.Services.Purchase.Apply.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IAggregatorsService, PvAggregatorsService>();
builder.Services.AddScoped<IPvNewCarDealersService, PvNewCarDealersService>();
builder.Services.AddScoped<IPvOpenMarketsService, PvOpenMarketsService>();
builder.Services.AddScoped<IPvaMakeService, PvaMakeService>();
builder.Services.AddScoped<IPvaModelService, PvaModelService>();
builder.Services.AddScoped<IPvaVariantService, PvaVariantService>();
builder.Services.AddScoped<IPvaYearOfRegService, PvaYearOfRegService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DealerApifinalContext>();


// AutoMapper Config
builder.Services.AddAutoMapper(typeof(Program));

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
