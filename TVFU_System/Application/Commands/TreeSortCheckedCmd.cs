using System;
using System.Windows.Input;
using Application.ViewModels;

namespace Application.Commands
{
    public class TreeSortCheckedCmd : ICommand
    {
        readonly Action<string> _execute;

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public TreeSortCheckedCmd(Action<string> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as string);
        }
    }
}