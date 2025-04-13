using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using WpfFromDeepseek.Services;
using WpfFromDeepseek.ViewModels;

namespace WpfFromDeepseek {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            var services = new ServiceCollection();
            services.AddSingleton<HttpClient>();

            services.AddTransient<IWeatherService, WeatherService>();

            services.AddTransient<WeatherViewModel>();

            services.AddTransient<MainWindow>(provider => {
                var viewModel = provider.GetRequiredService<WeatherViewModel>();
                return new MainWindow { DataContext = viewModel };
            });

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<MainWindow>().Show();
        }
    }

}
