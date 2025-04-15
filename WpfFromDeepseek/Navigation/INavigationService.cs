using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfFromDeepseek.Navigation {
    public interface INavigationService {
        void ShowWindow<T>() where T : Window;
    }
}
