using Persistence.Repositories.Interfaces;
using Application.ViewModels;
using DomainLayer.EventArgs;
using Application.Commands;
using System.Windows.Input;
using System.Linq;
using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace Application.Controllers
{
    public class ProductListController
    {
        private IProductRepo _productRepository;

        /// <summary>
        /// A ViewModel expected to be set as a datacontext to a view that can take user input towards twelve different properties.
        /// </summary>
        public ProductViewModel CurrentProductVM { get; set; }

        /// <summary>
        /// A ViewModel expected to be set as a datacontext to a view that is able to show more than one thousand entities.
        /// </summary>
        public ProductListViewModel CurrentProductListVM { get; set; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the creation creation of a product
        /// </summary>
        public ICommand CreateProductCommand { get; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the change of a product
        /// </summary>
        public ICommand ChangeProductCommand { get; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the deletion of a product
        /// </summary>
        public ICommand DeleteProductCommand { get; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the entry of the Product List
        /// </summary>
        public ICommand ShowProductListCommand { get; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the closing of the Product List
        /// </summary>
        public ICommand CloseProductListCommand { get; }

        public ICommand SearchProductListCommand { get; }

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'CreateProduct'
        /// within this class
        /// </summary>
        public event Func<ProductEventArgs> NewProductRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'ChangeProduct'
        /// within this class
        /// </summary>
        public event Func<ProductEventArgs> ProductUpdateRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'DeleteProduct'
        /// within this class
        /// </summary>
        public event Func<bool> ProductDeleteRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'ShowProductList'
        /// within this class.
        /// But only if the GUI has more main windows other than the Product List 
        /// </summary>
        public event Action ShowProductListRequested;

        /// <summary>
        /// A controller used to control the ProductList view.
        /// Must be used as a singleton.
        /// </summary>
        /// <param name="productRepo">Pass an implementation of IProductRepo through the constructor</param>
        public ProductListController(IProductRepo productRepo)
        {
            CreateProductCommand = new CreateProductCmd(CreateProduct);
            ShowProductListCommand = new ShowProductListCmd(ShowProductList);
            CloseProductListCommand = new CloseProductListCmd(CloseProductList);
            ChangeProductCommand = new ChangeProductCmd(ChangeProduct);
            DeleteProductCommand = new DeleteProductCmd(DeleteProduct);
            SearchProductListCommand = new SearchProductListCmd(ConfirmSearch);
            _productRepository = productRepo;
            CurrentProductListVM = new ProductListViewModel();
        }

        /// <summary>
        /// A method used to create a product for CurrentProductListVM,
        /// as well as saving said product in the repository.
        /// Calls the method 'OnNewProductRequested' in its own class to populate a ProductEventArgs object's properties.
        /// If the object return from 'OnNewProductRequested' is null, this method will not save said product neither in
        /// CurrentProductListVM or the repository.
        /// </summary>
        /// <returns>The data of the product that was created stored in the properties of a ProductEventArgs object.
        /// Can be null</returns>
        public ProductEventArgs CreateProduct()
        {
            CurrentProductVM = new ProductViewModel();
            ProductEventArgs productValues = OnNewProductRequested();
            if (productValues != null)
            {
                productValues.Id = CurrentProductVM.Id;
                _productRepository.Add(productValues);
                CurrentProductVM.Update(productValues);
                CurrentProductListVM.ViewModels.Add(CurrentProductVM);
            }
            return productValues;
        }

        /// <summary>
        /// Calls the event 'NewProductRequested' in order to populate its ProductEventArgs return value.
        /// If the event is null, this method will return null.
        /// </summary>
        /// <returns>All the necessary data (except the ID) of a product stored in the properties of a ProductEventArgs object.
        /// Can be null</returns>
        protected ProductEventArgs OnNewProductRequested()
        {
            ProductEventArgs result = null;
            Func<ProductEventArgs> newProductRequested = NewProductRequested;
            if (newProductRequested != null)
            {
                result = newProductRequested();
            }
            return result;
        }

        /// <summary>
        /// A method used to change the properties of the parameter to whatever data is returned from the
        /// method 'OnProductUpdateRequested' in its own class. Which is called from this method.
        /// Should the return value from 'OnProductUpdateRequested' turn out to be null;
        /// will the parameter end up with the same property values as it did when it was parsed as an argument
        /// </summary>
        /// <param name="parameter">Is expected to be of type 'ProductViewModel'</param>
        public void ChangeProduct(object parameter)
        {
            var oldObj = new ProductViewModel((parameter as ProductViewModel));
            var newObj = (parameter as ProductViewModel);
            CurrentProductVM = newObj;
            ProductEventArgs productValues = OnProductUpdateRequested();

            if (productValues != null)
            {
                productValues.Id = CurrentProductVM.Id;
                _productRepository.Update(productValues);
            }
            else
            {
                CurrentProductListVM.ViewModels.First(s => s.Id == oldObj.Id).Update(oldObj);
            }
            CurrentProductVM = null;
        }

        /// <summary>
        /// Calls the event 'ProductUpdateRequested' in order to populate its ProductEventArgs return value.
        /// If the event is null, this method will return null.
        /// </summary>
        /// <returns>All the necessary data (except the ID) of a product stored in the properties of a ProductEventArgs object.
        /// Can be null</returns>
        protected ProductEventArgs OnProductUpdateRequested()
        {
            ProductEventArgs result = null;
            Func<ProductEventArgs> newProductRequested = ProductUpdateRequested;
            if (newProductRequested != null)
            {
                result = newProductRequested();
            }
            return result;
        }

        /// <summary>
        /// A method used to remove the parameter object from both the Product List and the repository.
        /// It calls the method 'OnProductDeleteRequested' from within its own class.
        /// Should said method return true, will the parameter object be deleted.
        /// Otherwise nothing happens
        /// </summary>
        /// <param name="parameter">Is expected to be of type 'ProductViewModel'</param>
        public void DeleteProduct(object parameter)
        {
            var delObj = (parameter as ProductViewModel);
            CurrentProductVM = delObj;

            if (OnProductDeleteRequested())
            {
                foreach (var item in CurrentProductListVM.ViewModels)
                {
                    if (item.Id == delObj.Id)
                    {
                        _productRepository.Remove(item.Id);
                        CurrentProductListVM.ViewModels.Remove(item);

                        break;
                    }
                }
            }
            CurrentProductVM = null;
        }

        /// <summary>
        /// Calls the event 'ProductDeleteRequested' in order to determine if this method's return value should be true or false
        /// </summary>
        /// <returns>False if the event 'ProductDeleteRequested' is null</returns>
        protected bool OnProductDeleteRequested()
        {
            bool result = false;
            Func<bool> deleteProductRequested = ProductDeleteRequested;
            if (deleteProductRequested != null)
            {
                result = deleteProductRequested();
            }

            return result;
        }

        /// <summary>
        /// A method used to populate 'CurrentProductListVM's observable collection property 'ViewModels',
        /// by calling the repository's method 'GetByProductCategories'
        /// </summary>
        public void ShowProductList()
        {
            List<Product> temp = (_productRepository.GetByProductCategories() as List<Product>);
            for (int i = 0; i < temp.Count; i++)
            {
                CurrentProductListVM.ViewModels.Add(new ProductViewModel(temp[i]));
            }
            //if (ShowProductListRequested != null)
            //{
            //    ShowProductListRequested.Invoke();
            //}
        }

        /// <summary>
        /// A method used to clear the 'CurrentProductListVM's observable collection property 'ViewModels' of all entities
        /// </summary>
        public void CloseProductList()
        {
            CurrentProductListVM.ViewModels.Clear();
        }

        public void ConfirmSearch()
        {
            List<Product> temp = (_productRepository.SearchProductList(CurrentProductListVM.SearchCategory.ToString(), CurrentProductListVM.SearchWord) as List<Product>);

            CloseProductList();
            for (int i = 0; i < temp.Count; i++)
            {
                CurrentProductListVM.ViewModels.Add(new ProductViewModel(temp[i]));
            }
        }
    }
}