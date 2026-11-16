using Lab11_MercyJanampa.Infrastructure.Configuration;
using Lab11_MercyJanampa.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

// Infrastructure: DbContext + Repository
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapGet("/", () => "Lab14 Mercy Janampa API funcionando correctamente.");

app.MapControllers();

app.Run();