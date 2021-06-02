using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Commands
{
    public class AddMaterialCmd : ICommand
    {
        readonly Action<string> _execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddMaterialCmd(Action<string> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if ((string)parameter != "")
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke((string)parameter);
        }
    }
}
