


using API.Data;
using API.Middleware;
using Application.Services;
using Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureExtensions(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddApplicationLayer();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.AddPipeLine();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e,"An  error occurred during migration ");

}
app.Run();
