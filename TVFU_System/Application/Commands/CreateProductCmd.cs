using System;
using System.Windows.Input;

namespace Application.Commands
{
    public class CreateProductCmd : ICommand
    {
        readonly Func<object> _execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CreateProductCmd(Func<object> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
