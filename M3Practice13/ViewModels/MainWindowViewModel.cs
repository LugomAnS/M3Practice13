using M3Practice13.ViewModels.Base;
using M3Practice13.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace M3Practice13.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        UserControl curentView;

        public UserControl CurentView
        {
            get => curentView;
            set => Set(ref curentView, value);
        }

        public MainWindowViewModel()
        {
            CurentView = new RoleChoosingViewModel();
        }

    }
}
