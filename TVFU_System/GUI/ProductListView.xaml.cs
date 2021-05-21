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
        private readonly static IProductRepo _productRepo = new ProductRepo();
        private readonly ProductListController _pLC = new ProductListController(_productRepo);
        public ProductListView()
        {
            //_pLC.ShowProductList();
            InitializeComponent();
            var PLVM = _pLC.CurrentProductListVM;
            DataContext = new { PLVM, _pLC };
            _pLC.NewProductRequested += NewProductRequestHandler;
            _pLC.ProductUpdateRequested += ProductUpdateRequestHandler;
            _pLC.ProductDeleteRequested += DeleteProductRequestHandler;
            _pLC.ResetRequested += ResetRequestedHandler;
            _pLC.ExceptionThrown += ExceptionHandler;
            _pLC.OpenAdminRequested += OpenAdminRequestHandler;
            GetCategoiesAndFilters();
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
        public void ResetRequestedHandler()
        {
            ResetSortButtons(new object());

            TreeSortChecked(null, null);

            ColorChecked(null, null);
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

            if (sender != null)
            {
                temp.Remove((CheckBox)sender);
            }

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

            if (sender != null)
            {
                temp.Remove((CheckBox)sender);
            }

            foreach (var item in temp)
            {
                item.IsChecked = false;
            }
        }

        /// <summary>
        /// Called by all sorting columns from productlistbox.
        /// Changes the content of a button on click
        /// </summary>
        /// <param name="sender"> has to be a button from listbox</param>
        public void ChangeAcsAndDecsOnButtons(object sender, EventArgs args)
        {
            if (sender.ToString().Contains("Nummer"))
            {
                if (Btn_NummerSort.Content.ToString() == "Nummer")
                    Btn_NummerSort.Content = $"Nummer \x2191";

                else if (Btn_NummerSort.Content.ToString() == "Nummer \x2191")
                    Btn_NummerSort.Content = $"Nummer \x2193";

                else
                    Btn_NummerSort.Content = "Nummer";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Beskrivelse"))
            {
                if (Btn_BeskrivelseSort.Content.ToString() == "Beskrivelse")
                    Btn_BeskrivelseSort.Content = $"Beskrivelse \x2191";

                else if (Btn_BeskrivelseSort.Content.ToString() == "Beskrivelse \x2191")
                    Btn_BeskrivelseSort.Content = $"Beskrivelse \x2193";

                else
                    Btn_BeskrivelseSort.Content = "Beskrivelse";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Enhedspris"))
            {
                if (Btn_EnhedsprisSort.Content.ToString() == "Enhedspris")
                    Btn_EnhedsprisSort.Content = $"Enhedspris \x2191";

                else if (Btn_EnhedsprisSort.Content.ToString() == "Enhedspris \x2191")
                    Btn_EnhedsprisSort.Content = $"Enhedspris \x2193";

                else
                    Btn_EnhedsprisSort.Content = "Enhedspris";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Vejledende pris"))
            {
                if (Btn_VejledendePrisSort.Content.ToString() == "Vejledende pris")
                    Btn_VejledendePrisSort.Content = "Vejledende pris \x2191";

                else if (Btn_VejledendePrisSort.Content.ToString() == "Vejledende pris \x2191")
                    Btn_VejledendePrisSort.Content = "Vejledende pris \x2193";

                else
                    Btn_VejledendePrisSort.Content = "Vejledende pris";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Total lager"))
            {
                if (Btn_TotalLager.Content.ToString() == "Total lager")
                    Btn_TotalLager.Content = $"Total lager \x2191";

                else if (Btn_TotalLager.Content.ToString() == "Total lager \x2191")
                    Btn_TotalLager.Content = $"Total lager \x2193";

                else
                    Btn_TotalLager.Content = "Total lager";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Spærret"))
            {
                if (Btn_SpærretSort.Content.ToString() == "Spærret")
                    Btn_SpærretSort.Content = $"Spærret(F)";

                else if (Btn_SpærretSort.Content.ToString() == "Spærret(F)")
                    Btn_SpærretSort.Content = $"Spærret(T)";

                else
                    Btn_SpærretSort.Content = "Spærret";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Antal pr. kolli"))
            {
                if (Btn_KolliSort.Content.ToString() == "Antal pr. kolli")
                    Btn_KolliSort.Content = $"Antal pr. kolli \x2191";

                else if (Btn_KolliSort.Content.ToString() == "Antal pr. kolli \x2191")
                    Btn_KolliSort.Content = $"Antal pr. kolli \x2193";

                else
                    Btn_KolliSort.Content = "Antal pr. kolli";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Mængderabat"))
            {
                if (Btn_MængderabatSort.Content.ToString() == "Mængderabat")
                    Btn_MængderabatSort.Content = $"Mængderabat \x2191";

                else if (Btn_MængderabatSort.Content.ToString() == "Mængderabat \x2191")
                    Btn_MængderabatSort.Content = $"Mængderabat \x2193";

                else
                    Btn_MængderabatSort.Content = "Mængderabat";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Indkøbskode"))
            {
                if (Btn_IndkøbskodeSort.Content.ToString() == "Indkøbskode")
                    Btn_IndkøbskodeSort.Content = $"Indkøbskode a-z";

                else if (Btn_IndkøbskodeSort.Content.ToString() == "Indkøbskode a-z")
                    Btn_IndkøbskodeSort.Content = $"Indkøbskode z-a";

                else
                    Btn_IndkøbskodeSort.Content = "Indkøbskode";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Bekræftet Modtagelsedato"))
            {
                if (Btn_ModtagelsesdatoSort.Content.ToString() == "Bekræftet Modtagelsedato")
                    Btn_ModtagelsesdatoSort.Content = $"Bekræftet Modtagelsedato \x2191";

                else if (Btn_ModtagelsesdatoSort.Content.ToString() == "Bekræftet Modtagelsedato \x2191")
                    Btn_ModtagelsesdatoSort.Content = $"Bekræftet Modtagelsedato \x2193";

                else
                    Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";

                ResetSortButtons(sender);
                return;
            }
            else if (sender.ToString().Contains("Oprindelsesland"))
            {
                if (Btn_OprindelseslandSort.Content.ToString() == "Oprindelsesland")
                    Btn_OprindelseslandSort.Content = $"Oprindelsesland a-z";

                else if (Btn_OprindelseslandSort.Content.ToString() == "Oprindelsesland a-z")
                    Btn_OprindelseslandSort.Content = $"Oprindelsesland z-a";

                else
                    Btn_OprindelseslandSort.Content = "Oprindelsesland";

                ResetSortButtons(sender);
                return;
            }

        }

        /// <summary>
        /// Called in the ChangeAcsAndDecsButtons method
        /// Resets buttons content which isn't clicked to default
        /// </summary>
        private void ResetSortButtons(object sender)
        {
            if (sender.ToString().Contains("Nummer"))
            {
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Beskrivelse"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Enhedspris"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Vejledende pris"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Total lager"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Spærret"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Antal pr. kolli"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Mængderabat"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Indkøbskode"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet Modtagelsedato";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Bekræftet modtagelsedato"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
            }
            else if (sender.ToString().Contains("Oprindelsesland"))
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet modtagelsedato";
            }
            else
            {
                Btn_NummerSort.Content = "Nummer";
                Btn_BeskrivelseSort.Content = "Beskrivelse";
                Btn_EnhedsprisSort.Content = "Enhedspris";
                Btn_VejledendePrisSort.Content = "Vejledende pris";
                Btn_TotalLager.Content = "Total lager";
                Btn_SpærretSort.Content = "Spærret";
                Btn_KolliSort.Content = "Antal pr. kolli";
                Btn_MængderabatSort.Content = "Mængderabat";
                Btn_IndkøbskodeSort.Content = "Indkøbskode";
                Btn_OprindelseslandSort.Content = "Oprindelsesland";
                Btn_ModtagelsesdatoSort.Content = "Bekræftet modtagelsedato";
            } 

        }

        private void ExceptionHandler(string exceptionMessage)
        {
            MessageBox.Show(exceptionMessage, "Der gik noget galt", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private AdminEventArgs OpenAdminRequestHandler()
        {
            AdminEventArgs args = new AdminEventArgs();
            AdminView AV = new AdminView();
            AV.ShowDialog();
            args.Categories = AV.AVM.Categories;
            args.Filters = AV.AVM.Filters;
            return args;
        }
        private void GetCategoiesAndFilters()
        {
            AdminView AV = new AdminView();
            _pLC.CurrentProductListVM.Filters = AV.AVM.Filters;
            _pLC.CurrentProductListVM.Categories = AV.AVM.Categories;
            AV.Close();
        }
    }
}
