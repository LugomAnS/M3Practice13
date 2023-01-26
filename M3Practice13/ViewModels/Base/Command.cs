using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace M3Practice13.ViewModels.Base
{
    internal class Command : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public Command(Action<object> Execute, Predicate<object> CanExecute = null)
        {
            execute = Execute;
            canExecute = CanExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
            => canExecute?.Invoke(parameter) ?? true;


        public void Execute(object? parameter)
            => execute?.Invoke(parameter);
    }
}
