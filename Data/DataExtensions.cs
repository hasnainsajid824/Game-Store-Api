
using gamestoreapi.Entities;
using gamestoreapi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace gamestoreapi.Data;

public static class DataExtensions
{
    public static async Task InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await DbContext.Database.MigrateAsync();
    }
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("GameStoreContext");
        services.AddSqlServer<GameStoreContext>(connString).AddScoped<IGameRepository, EFGameRepository>();

        return services;
    }
}