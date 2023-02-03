using M3Practice13.Controler;
using M3Practice13.Models;
using M3Practice13.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace M3Practice13.ViewModels
{
    class RoleChoosingVM : BaseViewModel
    {
        public RoleChoosingVM()
        {
            ConsultantCommand = new Command(OnConsultantExecute);
            ManagerCommand = new Command(OnManagerCommandExecute);
        }

        #region Команды

        #region Выбор роли, реализация 2-х кнопок
        public ICommand ConsultantCommand { get; }

        private void OnConsultantExecute(object p)
        {
            Service.ChangeRoleRequest(new Consultant());
        }

        public ICommand ManagerCommand { get; }

        private void OnManagerCommandExecute(object obj)
        {
            Service.ChangeRoleRequest(new Manager());
        }
        #endregion

        #endregion
    }
}
