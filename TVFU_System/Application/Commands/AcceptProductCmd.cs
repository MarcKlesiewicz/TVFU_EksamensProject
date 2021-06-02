using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Commands
{
    public class AcceptProductCmd : ICommand
    {
        readonly Action<object> _execute;

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AcceptProductCmd(Action<object> execute)
        {
            this._execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            var PVM = (ProductViewModel)parameter;

            if (PVM != null)
            {
                if (PVM.ProductNumber != "" && PVM.Description != "" && PVM.CountryOfOrigin != "" && PVM.ConfirmedDeliveryDate != String.Empty && PVM.GuidingPrice != 0 && PVM.PurchasingManager != "" && PVM.UnitPerPackage != 0 && PVM.UnitPrice != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
