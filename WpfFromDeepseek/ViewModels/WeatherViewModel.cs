using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfFromDeepseek.Services;

namespace WpfFromDeepseek.ViewModels {
    public class WeatherViewModel : INotifyPropertyChanged {
        private readonly IWeatherService _weatherService;
        private string _weatherInfo = "Нажмите кнопку для загрузки";

        public string WeatherInfo {
            get => _weatherInfo;
            set {
                _weatherInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetWeatherCommand { get; }

        public WeatherViewModel(IWeatherService weatherService) {
            _weatherService = weatherService;
            GetWeatherCommand = new RelayCommand(async () => await LoadWeather());
        }

        private async Task LoadWeather() {
            try {
                var weather = await _weatherService.GetWeatherAsync("Penza");
                WeatherInfo = $"Город: {weather.Name}\n" +
                    $"Температура: {weather.Main.Temp}°C\n" +
                    $"Влажность: {weather.Main.Humidity}%";
            }
            catch (Exception ex) {
                WeatherInfo = $"Ошибка {ex.Message}";
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
