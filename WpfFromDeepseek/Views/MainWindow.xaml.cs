using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfFromDeepseek.Services;

namespace WpfFromDeepseek {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly IWeatherService _weatherService;
        public MainWindow(IWeatherService weatherService) {
            InitializeComponent();
            _weatherService = weatherService;
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {

            try {
                var weather = await _weatherService.GetWeatherAsync("Moscow");

                string weatherInfo = $"Город: {weather.Name}\n" +
                           $"Температура: {weather.Main?.Temp ?? 0}°C\n" +
                           $"Влажность: {weather.Main?.Humidity ?? 0}%";

                WeatherText.Text = weatherInfo;
            }
            catch (Exception ex) {
                WeatherText.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}