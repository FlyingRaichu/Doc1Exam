using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database.Factories;

public class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    private static readonly IConfiguration Configuration;

    static DatabaseFactory()
    {
        // Load configuration from appsettings.json and environment variables
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
    
    public static DbContextOptions<DatabaseContext> BuildConnectionOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(GetConnectionString(), builderOptions => builderOptions.MigrationsAssembly("API"));

        return optionsBuilder.Options;
    }

    public static string GetConnectionString()
    {
        return Configuration.GetConnectionString("DefaultConnection") ??
               throw new Exception(
                   "Your connection string is not configured correctly in WebAPI/appsettings.json, or doesn't exist.");
    }

    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(GetConnectionString());

        return new DatabaseContext(optionsBuilder.Options);
    }
}