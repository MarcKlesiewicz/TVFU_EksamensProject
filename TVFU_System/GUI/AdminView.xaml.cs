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
        //private IFilterRepo _filterRepo = new FilterRepo();
        private ICategoryRepo _categoryRepo = new CategoryRepo();
        private IColourRepo _colourRepo = new ColourRepo();
        private IMaterialRepo _materialRepo = new MaterialRepo();
        private IOtherFilterRepo _otherFilter = new OtherFilterRepo();

        public AdminViewModel AVM;

        public AdminView()
        {
            AdminController AC = new AdminController(_categoryRepo, _colourRepo, _materialRepo, _otherFilter);
            InitializeComponent();
            AVM = AC.CurrentAdminVM;
            AC.GetAll();
            DataContext = new { AVM, AC };
            
            AC.ResetCategoryTextboxRequested += ResetCategoryTextboxRequestedHandler;
            AC.CategoryExistsRequested += CategoryExistsRequestedHandler;
            AC.MaxCategoriesRequested += MaxCategoriesRequestedHandler;

            AC.ResetColourTextboxRequested += ResetColourTextboxRequestedHandler;
            AC.ColourExistsRequested += ColourExistsRequestedHandler;
            AC.MaxColoursRequested += MaxColoursRequestedHandler;

            AC.ResetMaterialTextboxRequested += ResetMaterialTextboxRequestedHandler;
            AC.MaterialExistsRequested += MaterialExistsRequestedHandler;
            AC.MaxMaterialsRequested += MaxMaterialsRequestedHandler;

            AC.ResetOtherFilterTextboxRequested += ResetOtherFilterTextboxRequestedHandler;
            AC.OtherFilterExistsRequested += OtherFilterExistsRequestedHandler;
            AC.MaxOtherFiltersRequested += MaxOtherFiltersRequestedHandler;

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

        private void MaxColoursRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere farver", "Farver fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ColourExistsRequestedHandler()
        {
            MessageBox.Show($"{NewColour.Text} finders allerede som en farve", "Farve findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetColourTextboxRequestedHandler()
        {
            NewColour.Text = "";
        }

        private void MaxMaterialsRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere materiale", "Materiale fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MaterialExistsRequestedHandler()
        {
            MessageBox.Show($"{NewMaterial.Text} finders allerede som en materiale", "Materiale findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetMaterialTextboxRequestedHandler()
        {
            NewMaterial.Text = "";
        }

        private void MaxOtherFiltersRequestedHandler()
        {
            MessageBox.Show("Der kan ikke tilføjes flere andet", "Andet fyldt", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OtherFilterExistsRequestedHandler()
        {
            MessageBox.Show($"{NewOtherFilter.Text} finders allerede som en anden filter", "Andet findes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ResetOtherFilterTextboxRequestedHandler()
        {
            NewOtherFilter.Text = "";
        }

        private void CloseWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
