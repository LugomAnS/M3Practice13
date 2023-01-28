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

        #region Текущий View
        UserControl currentView;

        public UserControl CurentView
        {
            get => currentView;
            set => Set(ref currentView, value);
        }
        #endregion

        #region Рабочая роль
        private Worker currentRoleType;
        private Worker CurrentRoleType
        {
            get => currentRoleType;
            set
            {
                Set(ref currentRoleType, value);
                OnPropertyChanged(nameof(WindowTitle));
            }
        }
        #endregion

        #region Заголовок окна
        public string WindowTitle
        {
            get
            {
                if (CurrentRoleType is Consultant) return "Режим работы Консультант";

                if (CurrentRoleType is Manager) return "Режим работы Менеджер";

                return "Undefined";
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            ChangeRoleCommand = new Command(OnChangeRoleExecute);

            Service.ChangeRole += RoleAppointment;
            CurentView = new RoleChoosingView();  
        }

        /// <summary>
        /// Назначение новой роли работы
        /// </summary>
        /// <param name="worker"></param>
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
