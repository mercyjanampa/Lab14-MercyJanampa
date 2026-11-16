using Lab11_MercyJanampa.Application.UseCases.Tickets.Commands;
using Lab11_MercyJanampa.Infrastructure.Configuration;
using Lab11_MercyJanampa.Application.Configuration;
using Lab11_MercyJanampa.Infrastructure.Configuration;

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

app.MapControllers();

app.Run();