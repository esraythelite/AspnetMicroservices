using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging((hostingContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
builder.Services.AddOcelot();
var app = builder.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging();
var serviceProvider = serviceCollection.BuildServiceProvider();
var _logger = serviceProvider.GetService<ILogger<Program>>();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
