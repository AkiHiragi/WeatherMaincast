using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFromDeepseek.Data.Entities;

namespace WpfFromDeepseek.Data {
    public class AppDbContext : DbContext {
        public AppDbContext (DbContextOptions<AppDbContext> options) 
            : base(options) { }
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}
