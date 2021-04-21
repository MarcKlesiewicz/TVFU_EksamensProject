using System;
using System.Windows.Input;

namespace Application.Commands
{
    public class ChangeProductCmd : ICommand
    {
        readonly Action<object> _execute;

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ChangeProductCmd(Action<object> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
