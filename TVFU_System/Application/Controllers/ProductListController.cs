using Persistence.Repositories.Interfaces;
using Application.ViewModels;
using DomainLayer.EventArgs;
using Application.Commands;
using Application.Delegates;
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

        public ProductViewModel CurrentProductVM { get; set; }
        public ProductListViewModel CurrentProductListVM { get; set; }

        public ICommand CreateProductCommand { get; private set; }

        public ICommand ChangeProductCommand { get; private set; }

        public ICommand DeleteProductCommand { get; private set; }

        public ICommand ShowProductListCommand { get; }

        public ICommand CloseProductListCommand { get; }

        public event ProductEventHandler NewProductRequested;

        public event ProductEventHandler ProductUpdateRequested;

        public event BoolEventHandler ProductDeleteRequested;

        public event Action ShowProductListRequested;

        public ProductListController(IProductRepo productRepo)
        {
            CreateProductCommand = new CreateProductCmd(CreateProduct);
            ShowProductListCommand = new ShowProductListCmd(ShowProductList);
            CloseProductListCommand = new CloseProductListCmd(CloseProductList);
            _productRepository = productRepo;
            CurrentProductListVM = new ProductListViewModel();
        }

        public ProductEventArgs CreateProduct(object parameter)
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

        protected ProductEventArgs OnProductUpdateRequested()
        {
            ProductEventArgs result = null;
            ProductEventHandler newProductRequested = ProductUpdateRequested;
            if (newProductRequested != null)
            {
                ProductEventArgs args = null;
                result = newProductRequested(this, args);
            }
            return result;
        }

        public void DeleteProduct(object parameter)
        {
            var delObj = (parameter as ProductViewModel);
            CurrentProductVM = delObj;

            bool confirmDelete = OnProductDeleteRequested();


            if (confirmDelete)
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

        protected bool OnProductDeleteRequested()
        {
            bool result = false;
            BoolEventHandler deleteProductRequested = ProductDeleteRequested;
            if (deleteProductRequested != null)
            {
                result = deleteProductRequested();
            }

            return result;
        }

        public void ShowProductList()
        {
            List<Product> temp = (_productRepository.GetByProductCategories() as List<Product>);
            for (int i = 0; i < temp.Count; i++)
            {
                CurrentProductListVM.ViewModels.Add(new ProductViewModel(temp[i]));
            }
            if (ShowProductListRequested != null)
            {
                ShowProductListRequested.Invoke();
            }
        }

        public void CloseProductList()
        {
            CurrentProductListVM.ViewModels.Clear();
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