using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Commands;
using Application.Delegates;

namespace Application.Controllers
{
    public class ProductController
    {
        public event Delegates.EventHandler AcceptProductRequested;
        public event Delegates.EventHandler CancelProductRequested;
        public event Delegates.EventHandler ProductBlockedRequested;

        public ICommand AcceptProductCommand { get; private set; }
        public ICommand CancelProductCommand { get; private set; }
        public ICommand ProductBlockedCommand { get; private set; }

        public ProductController()
        {
            AcceptProductCommand = new AcceptProductCmd(AcceptProduct);
            CancelProductCommand = new CancelProductCmd(CancelProduct);
            ProductBlockedCommand = new ProductBlockedCmd(ProductBlocked);
        }

        public void ProductBlocked(Object parameter)
        {
            ProductBlockedRequested.Invoke(this, null);
        }

        public void AcceptProduct(Object parameter)
        {
            AcceptProductRequested.Invoke(this, null);
        }

        public void CancelProduct(Object parameter)
        {
            CancelProductRequested.Invoke(this, null);
        }
        
    }
}
