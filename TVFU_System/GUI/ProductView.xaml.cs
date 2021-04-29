using Application.Controllers;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductViewModel PVM;

        public ProductView(ProductViewModel PVM)
        {
            ProductController PL = new ProductController();
            InitializeComponent();
            DataContext = new { PVM,PL};
            PL.AcceptProductRequested += AcceptProductRequestedHandler;
            PL.CancelProductRequested += CancelProductRequestedHandler;
            PL.ProductBlockedRequested += ProductBlockedRequestHandler;
            this.PVM = PVM;
        }

        public void ProductBlockedRequestHandler(object sender, EventArgs args)
        {
            PVM.Blocked = BlockedCheckBox.IsChecked.Value;
        }

        public void AcceptProductRequestedHandler(object sender, EventArgs args)
        {
            this.DialogResult = true;
            Close();
        }

        public void CancelProductRequestedHandler(object sender, EventArgs args)
        {
            this.DialogResult = false;
            Close();
        }
    
    }
}
