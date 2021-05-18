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
        public AdminViewModel AVM;

        public AdminView()
        {
            AdminController AC = new AdminController(_filterRepo);
            InitializeComponent();
            AVM = AC.CurrentAdminVM;
            AC.GetAll();
            DataContext = new { AVM, AC };
            AC.ResetTextboxRequested += ResetTextboxRequestedHandler;
            AC.FilterExistsRequested += FilterExistsRequestedHandler;
            AC.MaxFiltersRequested += MaxFiltersRequestedHandler;
        }

        private void MaxFiltersRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere filters", "Filter fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FilterExistsRequestedHandler()
        {
            MessageBox.Show($"{NewFilter.Text} finders allerede som et filter", "Filter findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetTextboxRequestedHandler()
        {
            NewFilter.Text = "";
        }
    }
}
