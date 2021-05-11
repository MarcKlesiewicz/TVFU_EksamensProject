using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Commands
{
    public class ResetCmd : ICommand
    {
        readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public ResetCmd(Action execute)
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
