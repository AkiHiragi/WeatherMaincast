using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFromDeepseek.Data.Entities {
    public class WeatherRecord {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public double Temperature {  get; set; }
        public int Humidity {  get; set; }
        public DateTime RequestTime { get; set; }

        [NotMapped]
        public string FormatedDate => RequestTime.ToLocalTime().ToString("g");
    }
}
