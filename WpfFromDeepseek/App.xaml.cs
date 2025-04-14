using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Windows;
using WpfFromDeepseek.Data;
using WpfFromDeepseek.Services;
using WpfFromDeepseek.ViewModels;

namespace WpfFromDeepseek {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName))
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(config);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("Default")));
            
            services.AddSingleton<HttpClient>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<WeatherViewModel>();

            services.AddTransient<MainWindow>(provider => {
                var db = provider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                var viewModel = provider.GetRequiredService<WeatherViewModel>();
                return new MainWindow { DataContext = viewModel };
            });

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<MainWindow>().Show();
        }
    }

}
