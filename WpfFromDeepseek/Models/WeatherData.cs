using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfFromDeepseek.Models {
    public class WeatherData {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "Не указано";
        [JsonPropertyName("main")]
        public Maindata Main { get; set; } = new Maindata();

        public class Maindata {
            [JsonPropertyName("temp")]
            public double Temp { get; set; }
            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }
        }
    }
}
