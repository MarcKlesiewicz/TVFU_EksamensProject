using Application.Controllers;
using Application.ViewModels;
using DomainLayer.EventArgs;
using Persistence.Repositories.Implementations;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductListView : Window
    {
        private static IProductRepo repo = new ProductRepo();
        private ProductListController PLC = new ProductListController(repo);
        public ProductListView()
        {
            InitializeComponent();
            var PLVM = PLC.CurrentProductListVM;
            DataContext = new { PLVM, PLC };
            PLC.NewProductRequested += NewProductRequestHandler;
            PLC.ProductUpdateRequested += ProductUpdateRequestHandler;
            PLC.ProductDeleteRequested += DeleteProductRequestHandler;
            PLC.ShowProductList();
        }

        public ProductEventArgs NewProductRequestHandler()
        {
            ProductEventArgs result = new ProductEventArgs();
            ProductView PV = new ProductView(PLC.CurrentProductVM);
            bool? dialogresult = PV.ShowDialog();

            if (dialogresult.HasValue)
            {
                if (dialogresult.Value)
                {
                    result.ProductNumber = PLC.CurrentProductVM.ProductNumber;
                    result.ProductCategory = PLC.CurrentProductVM.ProductCategory;
                    result.Id = PLC.CurrentProductVM.Id;
                    result.Description = PLC.CurrentProductVM.Description;
                    result.UnitPrice = PLC.CurrentProductVM.UnitPrice;
                    result.GuidingPrice = PLC.CurrentProductVM.GuidingPrice;
                    result.TotalStock = PLC.CurrentProductVM.TotalStock;
                    result.Blocked = PLC.CurrentProductVM.Blocked;
                    result.UnitPerPackage = PLC.CurrentProductVM.UnitPerPackage;
                    result.QuantityDiscount = PLC.CurrentProductVM.QuantityDiscount;
                    result.ConfirmedDeliveryDate = PLC.CurrentProductVM.ConfirmedDeliveryDate;
                    result.CountryOfOrigin = PLC.CurrentProductVM.CountryOfOrigin;
                    result.PurchasingManager = PLC.CurrentProductVM.PurchasingManager;

                }
                else
                {
                    result = null;
                }
            }
            return result;

        }
        public ProductEventArgs ProductUpdateRequestHandler()
        {
            ProductEventArgs result = new ProductEventArgs();
            ProductView PV = new ProductView(PLC.CurrentProductVM);
            bool? dialogresult = PV.ShowDialog();

            if (dialogresult.HasValue)
            {
                if (dialogresult.Value)
                {
                    result.ProductNumber = PLC.CurrentProductVM.ProductNumber;
                    result.ProductCategory = PLC.CurrentProductVM.ProductCategory;
                    result.Id = PLC.CurrentProductVM.Id;
                    result.Description = PLC.CurrentProductVM.Description;
                    result.UnitPrice = PLC.CurrentProductVM.UnitPrice;
                    result.GuidingPrice = PLC.CurrentProductVM.GuidingPrice;
                    result.TotalStock = PLC.CurrentProductVM.TotalStock;
                    result.Blocked = PLC.CurrentProductVM.Blocked;
                    result.UnitPerPackage = PLC.CurrentProductVM.UnitPerPackage;
                    result.QuantityDiscount = PLC.CurrentProductVM.QuantityDiscount;
                    result.ConfirmedDeliveryDate = PLC.CurrentProductVM.ConfirmedDeliveryDate;
                    result.CountryOfOrigin = PLC.CurrentProductVM.CountryOfOrigin;
                    result.PurchasingManager = PLC.CurrentProductVM.PurchasingManager;

                }
                else
                {
                    result = null;
                }
            }
            return result;


        }
        public bool DeleteProductRequestHandler()
        {
            MessageBoxResult result = MessageBox.Show("Er du sikker på, at du vil slette produktet?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
