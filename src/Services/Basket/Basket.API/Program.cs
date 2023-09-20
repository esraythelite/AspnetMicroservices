using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//                                                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
//                                            });
builder.Services.AddSwaggerGen(c =>
                                {
                                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
                                });

var app = builder.Build();
var configuration = app.Services.GetRequiredService<IConfiguration>();
var cacheSettingsValue = configuration.GetValue<string>("CacheSettings:ConnectionString");

builder.Services.AddStackExchangeRedisCache(options =>
                                            {
                                                options.Configuration = cacheSettingsValue;
                                            });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
