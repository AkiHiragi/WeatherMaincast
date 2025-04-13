using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfFromDeepseek.Models;

namespace WpfFromDeepseek.Services {
    public class WeatherService : IWeatherService {
        private readonly HttpClient _httpClient;
        public WeatherService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<WeatherData> GetWeatherAsync(string city) {
            string apiKey = "dad0e53d811dad3fee317cc0d2b7c045";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";
            try {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<WeatherData>(json) ?? throw new Exception("Не удалось распознать данные");

                if (string.IsNullOrEmpty(result.Name))
                    result.Name = city;

                return result;
            }
            catch (Exception) {
                return new WeatherData {
                    Name = city,
                    Main = new WeatherData.Maindata {
                        Temp = 0,
                        Humidity = 0
                    }
                };
            }
        }
    }
}
