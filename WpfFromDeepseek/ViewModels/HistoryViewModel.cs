using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfFromDeepseek.Data;
using WpfFromDeepseek.Data.Entities;

namespace WpfFromDeepseek.ViewModels {
    public class HistoryViewModel:INotifyPropertyChanged {
        private readonly AppDbContext _db;
        private ObservableCollection<WeatherRecord> _records = new();

        public ObservableCollection<WeatherRecord> Records {
            get => _records;
            set {
                _records = value;
                OnPropertyChanged();
            }
        }

        public HistoryViewModel(AppDbContext db) {
            _db = db;
            LoadHistory();
        }
        private async void LoadHistory() {
            var history = new ObservableCollection<WeatherRecord>(
                await _db.WeatherRecords
                .OrderByDescending(r => r.RequestTime)
                .ToListAsync());

            Records = new ObservableCollection<WeatherRecord>(history);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
