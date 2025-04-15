using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfFromDeepseek.Navigation;
using WpfFromDeepseek.Services;
using WpfFromDeepseek.Views;

namespace WpfFromDeepseek.ViewModels {
    public class WeatherViewModel : INotifyPropertyChanged {
        private readonly IWeatherService _weatherService;
        private string _weatherInfo = "Нажмите кнопку для загрузки";
        private string _cityName = "Moscow";

        public string WeatherInfo {
            get => _weatherInfo;
            set { _weatherInfo = value; OnPropertyChanged(); }
        }

        public string CityName {
            get => _cityName;
            set { _cityName = value; OnPropertyChanged(); }
        }

        public ICommand ShowHistoryCommand { get; }

        public ICommand GetWeatherCommand { get; }

        public WeatherViewModel(IWeatherService weatherService, INavigationService navigation) {
            _weatherService = weatherService;

            GetWeatherCommand = new RelayCommand(
                execute: async () => await LoadWeather(),
                canExecute: CanLoadWeather
            );

            ShowHistoryCommand = new RelayCommand(() => {
                navigation.ShowWindow<HistoryWindow>();
            });
        }

        private async Task LoadWeather() {
            try {
                var weather = await _weatherService.GetWeatherAsync(CityName);
                WeatherInfo = $"Город: {weather.CityName}\n" +
                    $"Температура: {weather.Main.Temperature}°C\n" +
                    $"Влажность: {weather.Main.Humidity}%";
            }
            catch (Exception ex) {
                WeatherInfo = $"Ошибка {ex.Message}";
            }
        }

        private bool CanLoadWeather() => !string.IsNullOrEmpty(CityName) && CityName.All(char.IsLetter);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
