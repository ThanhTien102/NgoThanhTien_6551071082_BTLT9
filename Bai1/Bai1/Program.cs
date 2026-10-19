using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Bai1.Configurations;
using Bai1.Models;
using Microsoft.EntityFrameworkCore;

namespace Bai1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Cấu hình Configuration
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .Build();

            // Cấu hình Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(
                    path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("Ứng dụng đang khởi động...");

                // Cấu hình DI
                var services = new ServiceCollection();
                ConfigureServices(services, configuration);

                using (var serviceProvider = services.BuildServiceProvider())
                {
                    var mainForm = serviceProvider.GetRequiredService<FormChiTieu>();
                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Ứng dụng bị dừng đột ngột!");
                MessageBox.Show("Đã xảy ra lỗi nghiêm trọng. Vui lòng kiểm tra file log.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Bind AppSettings
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            // DbContext
            services.AddDbContext<QLChiTieuContext>(options =>
                options.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection));

            // Forms
            services.AddTransient<FormChiTieu>();
        }
    }
}
