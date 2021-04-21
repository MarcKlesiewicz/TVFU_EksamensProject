using Persistence.Repositories.Interfaces;
using Application.ViewModels;
using DomainLayer.EventArgs;
using Application.Commands;
using Application.Delegates;
using System.Windows.Input;

namespace Application.Controllers
{
    public class ProductListController
    {
        private IProductRepo _productRepository;

        public ProductViewModel CurrentProductVM { get; private set; }
        public ProductListViewModel CurrentProductListVM { get; private set; }

        public ICommand CreateProductCommand { get; private set; }

        public ICommand ChangeProductCommand { get; private set; }

        public event ProductEventHandler NewProductRequested;

        public event ProductEventHandler NewProductChange;

        public ProductListController(IProductRepo productRepo)
        {
            CreateProductCommand = new CreateProductCmd(CreateProduct);
            _productRepository = productRepo;
            CurrentProductListVM = new ProductListViewModel();
        }

        public void CreateProduct(object parameter)
        {
            CurrentProductVM = new ProductViewModel();
            ProductEventArgs productValues = OnNewProductRequested();
            if (productValues != null)
            {
                _productRepository.Add(productValues);
            }
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

        public void ChangeProduct(object parameter)
        {
            var oldObj = (parameter as ProductViewModel);
            var newObj = (parameter as ProductViewModel);
            CurrentProductVM = oldObj;
            ProductEventArgs productValues = OnNewProductRequested();

            if (productValues != null)
            {
                _productRepository.Update(productValues);
            }
            else
            {
                CurrentProductListVM.ViewModels.Find(s => s.Id == CurrentProductVM.Id).Name = oldObj.Name;
            }
            CurrentProductVM = null;
        }

        //protected ProductEventArgs OnProductChange(ProductViewModel parameter)
        //{
        //    ProductEventArgs result = null;
        //    ProductEventHandler newProductChange = NewProductChange;
        //    if (newProductChange != null)
        //    {
        //        ProductEventArgs args = null;
        //        result = newProductChange(this, args);
        //    }
        //    return result;
        //}
    }
}