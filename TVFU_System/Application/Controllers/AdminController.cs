using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.ViewModels;
using Persistence.Repositories.Interfaces;
using Application.Commands;
using System.Collections.ObjectModel;

namespace Application.Controllers
{
    public class AdminController
    {
        
        private readonly IFilterRepo _filterRepo;

        private readonly ICategoryRepo _categoryRepo;

        private readonly IColourRepo _colourRepo;

        private readonly IMaterialRepo _materialRepo;

        private readonly IOtherFilterRepo _otherFilterRepo;
        /// <summary>
        /// A ViewModel expected to be set as a datacontext to a view.
        /// </summary>
        public AdminViewModel CurrentAdminVM { get; set; }

        ///// <summary>
        ///// A command expected to be databinded to a xaml object governing the search for a product
        ///// </summary>
        //public ICommand AddFilterCommand { get; }

        ///// <summary>
        ///// A command expected to be databinded to a xaml object governing the search for a product
        ///// </summary>
        //public ICommand RemoveFilterCommand { get; }

        public ICommand AddCategoryCommand { get; }

        public ICommand RemoveCategoryCommand { get; }

        public ICommand AddColourCommand { get; }

        public ICommand RemoveColourCommand { get; }

        public ICommand AddMaterialCommand { get; }

        public ICommand RemoveMaterialCommand { get; }

        public ICommand AddOtherFilterCommand { get; }

        public ICommand RemoveOtherFilterCommand { get; }


        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddFilter'
        /// within this class
        /// </summary>
        public event Action ResetFilterTextboxRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddFilter'
        /// within this class
        /// </summary>
        public event Action MaxFiltersRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddFilter'
        /// within this class
        /// </summary>
        public event Action FilterExistsRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddCategory'
        /// within this class
        /// </summary>
        public event Action ResetCategoryTextboxRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddCategory'
        /// within this class
        /// </summary>
        public event Action MaxCategoriesRequested;

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddCategory'
        /// within this class
        /// </summary>
        public event Action CategoryExistsRequested;

        public event Action MaxColoursRequested;

        public event Action ColourExistsRequested;

        /// <summary>
        /// A controller used to control the ProductList view.
        /// Must be used as a singleton.
        /// </summary>
        /// <param name="filterRepo">Pass an implementation of IFilterRepo through the constructor</param>
        public AdminController(IFilterRepo filterRepo, ICategoryRepo categoryRepo)
        {
            _filterRepo = filterRepo;
            _categoryRepo = categoryRepo;
            CurrentAdminVM = new AdminViewModel();
            //AddFilterCommand = new AddFilterCmd(AddFilter);
            //RemoveFilterCommand = new RemoveFilterCmd(RemoveFilter);
            AddCategoryCommand = new AddCategoryCmd(AddCategory);
            RemoveCategoryCommand = new RemoveCategoryCmd(RemoveCategory);

        }
        /// <summary>
        /// A method that gets all filters from the filterRepo
        /// </summary>
        public void GetAll()
        {
            CurrentAdminVM.Categories.Clear();
            CurrentAdminVM.Filters.Clear();

            foreach (var item in _categoryRepo.GetAll())
            {
                CurrentAdminVM.Categories.Add(item);
            }

            foreach (var item in _filterRepo.GetAll())
            {
                CurrentAdminVM.Filters.Add(item);
            }
        }
        ///// <summary>
        ///// A method that add a ny filter.
        ///// It check if the filter already exists in the list of filters.
        ///// It also checks if the list of filters is full.
        ///// </summary>
        ///// <param name="filter">The new filter</param>
        //public void AddFilter(string filter)
        //{
        //    foreach (var item in CurrentAdminVM.Filters)
        //    {
        //        if (item.ToLower() == filter.ToLower())
        //        {
        //            FilterExistsRequested();
        //            return;
        //        }
        //    }
        //    if (CurrentAdminVM.Filters.Count <= 10)
        //    {
        //        _filterRepo.Add(filter);
        //        CurrentAdminVM.Filters.Add(filter);
        //        ResetFilterTextboxRequested();
        //        GetAll();
        //    }
        //    else
        //    {
        //        MaxFiltersRequested();
        //    }
            
        //}
        ///// <summary>
        ///// A method that remove a filter from the list.
        ///// </summary>
        ///// <param name="filter">The filter to remove</param>
        //public void RemoveFilter(string filter)
        //{
        //    _filterRepo.Remove(filter);
        //    CurrentAdminVM.Filters.Remove(filter);
        //}

        public void AddCategory(string category)
        {
            foreach (var item in CurrentAdminVM.Categories)
            {
                if (item.ToLower() == category.ToLower())
                {
                    CategoryExistsRequested();
                    return;
                }
            }
            if (CurrentAdminVM.Categories.Count <= 10)
            {
                _categoryRepo.Add(category);
                CurrentAdminVM.Categories.Add(category);
                ResetCategoryTextboxRequested();
                GetAll();
            }
            else
            {
                MaxCategoriesRequested();
            }
        }

        public void RemoveCategory(string category)
        {
            _categoryRepo.Remove(category);
            CurrentAdminVM.Categories.Remove(category);
        }

        public void AddColour(string colour)
        {
            foreach (var item in CurrentAdminVM.Colours)
            {
                if (item.ToLower() == colour.ToLower())
                {
                    ColourExistsRequested();
                    return;
                }
            }
            if (CurrentAdminVM.Colours.Count <= 10)
            {
                _categoryRepo.Add(colour);
                CurrentAdminVM.Categories.Add(colour);
                ResetCategoryTextboxRequested();
                GetAll();
            }
            else
            {
                MaxColoursRequested();
            }
        }

        public void RemoveColour(string colour)
        {
            _categoryRepo.Remove(colour);
            CurrentAdminVM.Categories.Remove(colour);
        }

        public void Add
    }
}
