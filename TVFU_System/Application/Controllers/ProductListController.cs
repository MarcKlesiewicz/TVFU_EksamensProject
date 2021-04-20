using Persistence.Repositories.Interfaces;
using Application.ViewModels;
using Application.EventArgs;
using Application.Commands;
using Application.Delegates;
using System.Windows.Input;

namespace Application.Controllers
{
    public class ProductListController
    {
        private IProductRepo _productRepository;
        private IProductListRepo _productListRepository;

        public ProductViewModel CurrentProductVM { get; private set; }
        public ProductListViewModel CurrentProductListVM { get; private set; }

        public ICommand CreateProductCommand { get; private set; }

        public event ProductEventHandler NewProductRequested;

        public ProductListController()
        {
            CreateProductCommand = new CreateProductCmd(CreateProduct);
        }

        public void CreateProduct(object parameter)
        {
            CurrentProductVM = new ProductViewModel();
            ProductEventArgs productValues = OnNewProductRequested();
            _productRepository.Add(productValues);
        }

        protected ProductEventArgs OnNewProductRequested()
        {
            ProductEventArgs result = null;
            ProductEventHandler newProductRequested = NewProductRequested;
            if (newProductRequested != null)
            {
                ProductEventArgs args = null;
                result = newProductRequested(this, args);
            }
            return result;
        }
    }
}