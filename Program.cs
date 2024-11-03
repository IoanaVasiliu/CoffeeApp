using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibCafeApp.Data;
using LibCafeApp;
using LibCafeApp.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibCafeAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibCafeAppContext") ?? throw new InvalidOperationException("Connection string 'LibCafeAppContext' not found.")));

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapReservationEndpoints();

app.MapBookReservationEndpoints();

app.MapUserEndpoints();



app.MapOrderEndpoints();

app.MapReviewEndpoints();

app.MapPaymentEndpoints();

app.MapBookEndpoints();

app.Run();
