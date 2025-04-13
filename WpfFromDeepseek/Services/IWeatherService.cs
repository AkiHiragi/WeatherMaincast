using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFromDeepseek.Models;

namespace WpfFromDeepseek.Services {
    public interface IWeatherService {
        Task<WeatherData> GetWeatherAsync(string city);
    }
}
