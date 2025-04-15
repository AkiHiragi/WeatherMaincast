using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfFromDeepseek.Data;
using WpfFromDeepseek.Data.Entities;
using WpfFromDeepseek.Models;

namespace WpfFromDeepseek.Services {
    public class WeatherService : IWeatherService {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _db;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        public WeatherService(IConfiguration config, HttpClient httpClient, AppDbContext db) {
            _apiKey = config["WeatherApi:ApiKey"];
            _baseUrl = config["WeatherApi:BaseUrl"];
            _httpClient = httpClient;
            _db = db;
        }

        public async Task<WeatherData> GetWeatherAsync(string city) {
            string url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric&lang=ru";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<WeatherData>(json)!;

            await _db.WeatherRecords.AddAsync(new WeatherRecord {
                CityName = data.CityName,
                Temperature = data.Main.Temperature,
                Humidity = data.Main.Humidity,
                RequestTime = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();

            return data;

        }
    }
}
