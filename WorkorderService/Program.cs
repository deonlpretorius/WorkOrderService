using DigitalTwin.WorkOrderService.Endpoints;
using DigitalTwin.WorkOrderService.WebAPI.Data;
using DigitalTwin.WorkOrderService.WebAPI.Endpoints;
using DigitalTwin.WorkOrderService.WebAPI.Endpoints.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Services;
using DigitalTwin.WorkOrderService.WebAPI.Services.WorkOrders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Add the DbContext to the service container
builder.Services.AddDbContext<WorkOrderWebServiceWebAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add the static data services.
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IExternalSystemService, ExternalSystemService>();

// Add the Work Order services.
builder.Services.AddScoped<IWorkOrderStatusService, WorkOrderStatusService>();
builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();
builder.Services.AddScoped<IWorkOrderHistoryService, WorkOrderHistoryService>();
builder.Services.AddScoped<IWorkOrderEventService, WorkOrderEventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add the endpoints.
app.MapSiteEndpoints();
app.MapExternalSystemEndpoint();
app.MapWorkOrderStatusEndpoints();
app.MapWorkOrderEndpoints();
app.MapWorkOrderHistoryEndpoint();
app.MapWorkOrderEventEndpoints();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
