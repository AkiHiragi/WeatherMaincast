using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfFromDeepseek.Navigation {
    public class NavigationService : INavigationService {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<T>() where T : Window {
            var window = _serviceProvider.GetRequiredService<T>();
            window.Show();
        }
    }
}
