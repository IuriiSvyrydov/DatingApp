


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureExtensions(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddApplicationLayer();

var app = builder.Build();
app.AddPipeLine();

app.Run();
