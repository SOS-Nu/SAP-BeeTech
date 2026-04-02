using AutoMapper;
using SAP_BTS_API.Applications.Services;
using SAP_BTS_API.Domain.Interfaces;
using SAP_BTS_API.Domain.Interfaces.IRepositories;
using SAP_BTS_API.Domain.Interfaces.IServices;
using SAP_BTS_API.Domain.models;
using SAP_BTS_API.Infrastructure.externalService;
using SAP_BTS_API.Infrastructure.ExternalServices.Sap;
using SAP_BTS_API.Infrastructure.Persistence.Repositories;
using SAP_BTS_API.Middlewares;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SapSettings>(builder.Configuration.GetSection("SapSettings"));

// Đăng ký AuthHandler
builder.Services.AddSingleton<ISapSessionStore, SapSessionStore>(); 
builder.Services.AddTransient<SapAuthHandler>();
builder.Services.AddHttpClient<ISapServiceLayerClient, SapServiceLayerClient>(client =>
{
    var settings = builder.Configuration.GetSection("SapSettings").Get<SapSettings>();
    client.BaseAddress = new Uri($"https://{settings.SapServerSL}:50000/b1s/v1/");
    client.DefaultRequestHeaders.ExpectContinue = false;
})
.AddHttpMessageHandler<SapAuthHandler>()
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
});
//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//    options.SerializerOptions.PropertyNameCaseInsensitive = true;
//});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program));
}); 
builder.Services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddScoped<IBusinessPartnerService, BusinessPartnerService>();

builder.Services.AddScoped<IBusinessPartnerRepository, BusinessPartnerRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

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
