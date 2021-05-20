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
using Application.ViewModels;
using Application.Controllers;
using Persistence.Repositories.Interfaces;
using Persistence.Repositories.Implementations;
using System.Collections.ObjectModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        private IFilterRepo _filterRepo = new FilterRepo();
        private ICategoryRepo _categoryRepo = new CategoryRepo();
        public AdminViewModel AVM;

        public AdminView()
        {
            AdminController AC = new AdminController(_filterRepo, _categoryRepo);
            InitializeComponent();
            AVM = AC.CurrentAdminVM;
            AC.GetAll();
            DataContext = new { AVM, AC };
            AC.ResetFilterTextboxRequested += ResetFilterTextboxRequestedHandler;
            AC.FilterExistsRequested += FilterExistsRequestedHandler;
            AC.MaxFiltersRequested += MaxFiltersRequestedHandler;
            AC.ResetCategoryTextboxRequested += ResetCategoryTextboxRequestedHandler;
            AC.CategoryExistsRequested += CategoryExistsRequestedHandler;
            AC.MaxCategoriesRequested += MaxCategoriesRequestedHandler;
        }

        private void MaxFiltersRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere filters", "Filter fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FilterExistsRequestedHandler()
        {
            MessageBox.Show($"{NewFilter.Text} finders allerede som et filter", "Filter findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetFilterTextboxRequestedHandler()
        {
            NewFilter.Text = "";
        }
        private void MaxCategoriesRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere kategorier", "Kategorier fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CategoryExistsRequestedHandler()
        {
            MessageBox.Show($"{NewCategory.Text} finders allerede som en kategory", "Kategory findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetCategoryTextboxRequestedHandler()
        {
            NewCategory.Text = "";
        }
    }
}
