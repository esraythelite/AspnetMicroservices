using Microsoft.AspNetCore.Hosting;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
    })
    //.ConfigureWebHostDefaults(webBuilder =>
    //{
    //    webBuilder.UseStartup<Program>();
    //})
    .ConfigureLogging((hostingContext, loggingBuilder) =>
    {
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    });

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
var app = builder.Build();


app.UseRouting();

app.UseEndpoints(x =>
{
    x.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

await app.UseOcelot();

app.Run();
