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
        private readonly static IProductRepo _repo = new ProductRepo();
        private readonly ProductListController _pLC = new ProductListController(_repo);
        public ProductListView()
        {
            InitializeComponent();
            var PLVM = _pLC.CurrentProductListVM;
            DataContext = new { PLVM, _pLC };
            _pLC.NewProductRequested += NewProductRequestHandler;
            _pLC.ProductUpdateRequested += ProductUpdateRequestHandler;
            _pLC.ProductDeleteRequested += DeleteProductRequestHandler;
            _pLC.ShowProductList();
        }

        public ProductEventArgs NewProductRequestHandler()
        {
            ProductEventArgs result = new ProductEventArgs();
            ProductView PV = new ProductView(_pLC.CurrentProductVM);
            bool? dialogresult = PV.ShowDialog();

            if (dialogresult.HasValue)
            {
                if (dialogresult.Value)
                {
                    result.ProductNumber = _pLC.CurrentProductVM.ProductNumber;
                    result.ProductCategory = _pLC.CurrentProductVM.ProductCategory;
                    result.Id = _pLC.CurrentProductVM.Id;
                    result.Description = _pLC.CurrentProductVM.Description;
                    result.UnitPrice = _pLC.CurrentProductVM.UnitPrice;
                    result.GuidingPrice = _pLC.CurrentProductVM.GuidingPrice;
                    result.TotalStock = _pLC.CurrentProductVM.TotalStock;
                    result.Blocked = _pLC.CurrentProductVM.Blocked;
                    result.UnitPerPackage = _pLC.CurrentProductVM.UnitPerPackage;
                    result.QuantityDiscount = _pLC.CurrentProductVM.QuantityDiscount;
                    result.ConfirmedDeliveryDate = _pLC.CurrentProductVM.ConfirmedDeliveryDate;
                    result.CountryOfOrigin = _pLC.CurrentProductVM.CountryOfOrigin;
                    result.PurchasingManager = _pLC.CurrentProductVM.PurchasingManager;

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
            ProductView PV = new ProductView(_pLC.CurrentProductVM);
            bool? dialogresult = PV.ShowDialog();

            if (dialogresult.HasValue)
            {
                if (dialogresult.Value)
                {
                    result.ProductNumber = _pLC.CurrentProductVM.ProductNumber;
                    result.ProductCategory = _pLC.CurrentProductVM.ProductCategory;
                    result.Id = _pLC.CurrentProductVM.Id;
                    result.Description = _pLC.CurrentProductVM.Description;
                    result.UnitPrice = _pLC.CurrentProductVM.UnitPrice;
                    result.GuidingPrice = _pLC.CurrentProductVM.GuidingPrice;
                    result.TotalStock = _pLC.CurrentProductVM.TotalStock;
                    result.Blocked = _pLC.CurrentProductVM.Blocked;
                    result.UnitPerPackage = _pLC.CurrentProductVM.UnitPerPackage;
                    result.QuantityDiscount = _pLC.CurrentProductVM.QuantityDiscount;
                    result.ConfirmedDeliveryDate = _pLC.CurrentProductVM.ConfirmedDeliveryDate;
                    result.CountryOfOrigin = _pLC.CurrentProductVM.CountryOfOrigin;
                    result.PurchasingManager = _pLC.CurrentProductVM.PurchasingManager;

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

        /// <summary>
        /// Called by ComboBox_ProductCategory's SelectionChanged property 
        /// An event which calls the ConfirmFilterAndSearch method in ProductListController.
        /// </summary>
        public void FilterAndSearchProductListRequestEventHandler(object sender, EventArgs args)
        {
            _pLC.ConfirmFilterAndSearch();
        }

        /// <summary>
        /// Called by all tree sort checkboxes.
        /// Creates a temperary list of checkboxes and unchecks all added checkboxes expect for the chosen checkbox
        /// </summary>
        /// <param name="sender"> has to be the 'checked' checkbox</param>
        public void TreeSortChecked(object sender, EventArgs args)
        {
            List<CheckBox> temp = new List<CheckBox>();

            temp.Add(CheckBox_Birk);
            temp.Add(CheckBox_Akacie);
            temp.Add(CheckBox_Bøg);
            temp.Add(CheckBox_Fyr);
            temp.Add(CheckBox_Kirsebær);
            temp.Add(CheckBox_Eg);

            temp.Remove((CheckBox)sender);

            foreach (var item in temp)
            {
                item.IsChecked = false;
            }
        }

        /// <summary>
        /// Called by all color checkboxes.
        /// Creates a temperary list of checkboxes and unchecks all added checkboxes expect for the chosen checkbox
        /// </summary>
        /// <param name="sender"> has to be the 'checked' checkbox</param>
        public void ColorChecked(object sender, EventArgs args)
        {
            List<CheckBox> temp = new List<CheckBox>();

            temp.Add(CheckBox_blå);
            temp.Add(CheckBox_Rød);
            temp.Add(CheckBox_Gul);
            temp.Add(CheckBox_Sort);
            temp.Add(CheckBox_Hvid);
            temp.Add(CheckBox_Grøn);

            temp.Remove((CheckBox)sender);

            foreach (var item in temp)
            {
                item.IsChecked = false;
            }
        }

        public void ChangeAcsAndDecsOnButtons(object sender, EventArgs args)
        {
            
        }

        private void Btn_NummerSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_NummerSort.Content.ToString() == "Nummer")
                Btn_NummerSort.Content = $"Nummer \x2191";
            
            else if (Btn_NummerSort.Content.ToString() == "Nummer \x2191")
                Btn_NummerSort.Content = $"Nummer \x2193";

            else
                Btn_NummerSort.Content = "Nummer";
            
        }

        private void Btn_BeskrivelseSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_BeskrivelseSort.Content.ToString() == "Beskrivelse")
                Btn_BeskrivelseSort.Content = $"Beskrivelse \x2191";
            
            else if (Btn_BeskrivelseSort.Content.ToString() == "Beskrivelse \x2191")
                Btn_BeskrivelseSort.Content = $"Beskrivelse \x2193";
            
            else
                Btn_BeskrivelseSort.Content = "Beskrivelse";
            
        }

        private void Btn_EnhedsprisSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_EnhedsprisSort.Content.ToString() == "Enhedspris")
                Btn_EnhedsprisSort.Content = $"Enhedspris \x2191";
            
            else if (Btn_EnhedsprisSort.Content.ToString() == "Enhedspris \x2191")
                Btn_EnhedsprisSort.Content = $"Enhedspris \x2193";
            
            else
                Btn_EnhedsprisSort.Content = "Enhedspris";
            
        }

        private void Btn_VejledendePrisSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_VejledendePrisSort.Content.ToString() == "Vejledenden pris")
                Btn_VejledendePrisSort.Content = $"Vejledenden pris \x2191";

            else if (Btn_VejledendePrisSort.Content.ToString() == "Vejledenden pris \x2191")
                Btn_VejledendePrisSort.Content = $"Vejledenden pris \x2193";

            else
                Btn_VejledendePrisSort.Content = "Vejledenden pris";
        }

        private void Btn_TotalLager_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_TotalLager.Content.ToString() == "Total lager")
                Btn_TotalLager.Content = $"Total lager \x2191";

            else if (Btn_TotalLager.Content.ToString() == "Total lager \x2191")
                Btn_TotalLager.Content = $"Total lager \x2193";

            else
                Btn_TotalLager.Content = "Total lager";
        }

        private void Btn_SpærretSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_SpærretSort.Content.ToString() == "Spærret")
                Btn_SpærretSort.Content = $"Spærret(F)";

            else if (Btn_SpærretSort.Content.ToString() == "Spærret(F)")
                Btn_SpærretSort.Content = $"Spærret(T)";

            else
                Btn_SpærretSort.Content = "Spærret";
        }

        private void Btn_KolliSort_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_KolliSort.Content.ToString() == "Spærret")
                Btn_KolliSort.Content = $"Spærret(F)";

            else if (Btn_KolliSort.Content.ToString() == "Spærret(F)")
                Btn_KolliSort.Content = $"Spærret(T)";

            else
                Btn_SpærretSort.Content = "Spærret";
        }
    }
}
