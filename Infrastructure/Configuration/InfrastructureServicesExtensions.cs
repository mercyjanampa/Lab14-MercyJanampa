using Lab11_MercyJanampa.Domain.Interfaces;
using Lab11_MercyJanampa.Infrastructure.Context;
using Lab11_MercyJanampa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab11_MercyJanampa.Infrastructure.Services;

namespace Lab11_MercyJanampa.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TicketeraDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            ));

        services.AddScoped(
            typeof(IRepository<>),
            typeof(Repository<>)
        );
        services.AddScoped<IExcelReportService, ExcelReportService>();

        return services;
    }
}