using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsClientApp.Infrastructure
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public DelegateCommand(Func<object, Task> executeAction) => _execute = executeAction;

        public DelegateCommand(Func<object, Task> executeAction, Func<object, bool> canExecuteFunc)
        {
            _canExecute = canExecuteFunc;
            _execute = executeAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecute != null)
            {
                return _canExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
