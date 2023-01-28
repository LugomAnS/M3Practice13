using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using M3Practice13.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        UserControl curentView;

        private Worker CurrentRoleType { get; set; }

        public UserControl CurentView
        {
            get => curentView;
            set => Set(ref curentView, value);
        }

        public MainWindowViewModel()
        {
            ChangeRoleCommand = new Command(OnChangeRoleExecute);

            Service.ChangeRole += RoleAppointment;
            CurentView = new RoleChoosingView();  
        }

        private void RoleAppointment(Worker worker)
        {
            CurrentRoleType = worker;

            CurentView = new ClientWorkView(worker);
            
        }

        #region Команды

        #region Смена роли
        public ICommand ChangeRoleCommand { get; }

        private void OnChangeRoleExecute(object p) => CurentView = new RoleChoosingView();
        #endregion

        #endregion

    }
}
