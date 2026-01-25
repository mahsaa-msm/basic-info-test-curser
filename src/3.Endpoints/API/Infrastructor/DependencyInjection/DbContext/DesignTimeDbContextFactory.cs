using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.DbContext;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MasterDataCommandDbContext>
{
    public MasterDataCommandDbContext CreateDbContext(string[] args)
    {
        // تشخیص محیط اجرا
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        Console.WriteLine($"Environment: {environmentName}");

        // پیدا کردن مسیر پروژه API (که appsettings.json دارد)
        var basePath = FindApplicationBasePath();

        Console.WriteLine($"Base path: {basePath}");

        // پیکربندی برای خواندن connection string از appsettings مربوطه
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // پیدا کردن connection string به صورت خودکار
        var connectionString = FindConnectionString(configuration);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find a connection string.");
        }

        Console.WriteLine($"Using connection string: {connectionString}");

        var optionsBuilder = new DbContextOptionsBuilder<MasterDataCommandDbContext>();
        optionsBuilder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());

        return new MasterDataCommandDbContext(optionsBuilder.Options);
    }

    private string FindApplicationBasePath()
    {
        // مسیر جاری (پروژه Infrastructure)
        var currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current directory: {currentDirectory}");

        // سعی در پیدا کردن مسیر پروژه API
        var solutionDir = FindSolutionDirectory(currentDirectory);
        if (solutionDir != null)
        {
            var apiProjectPath = Path.Combine(solutionDir, "src", "Master.Data.Endpoints.API");
            if (Directory.Exists(apiProjectPath))
            {
                return apiProjectPath;
            }

            // یا شاید در ساختار متفاوتی باشد
            apiProjectPath = Path.Combine(solutionDir, "Master.Data.Endpoints.API");
            if (Directory.Exists(apiProjectPath))
            {
                return apiProjectPath;
            }
        }

        // اگر پیدا نکردیم، از مسیر جاری استفاده می‌کنیم
        return currentDirectory;
    }

    private string FindSolutionDirectory(string currentDirectory)
    {
        var directory = new DirectoryInfo(currentDirectory);
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory?.FullName;
    }

    private string FindConnectionString(IConfiguration configuration)
    {
        // اولویت‌بندی برای پیدا کردن connection string
        var possibleNames = new[]
        {
                "CommandDb_ConnectionString",
                "DefaultConnection",
                "ConnectionString",
                "SqlConnection"
            };

        foreach (var name in possibleNames)
        {
            var connectionString = configuration.GetConnectionString(name);
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
        }

        // اگر هیچ connection stringی پیدا نشد، اولین connection string را برگردانید
        var connectionStrings = configuration.GetSection("ConnectionStrings").GetChildren();
        return connectionStrings.FirstOrDefault()?.Value;
    }
}
