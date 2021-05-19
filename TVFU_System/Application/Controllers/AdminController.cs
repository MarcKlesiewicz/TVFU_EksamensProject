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

        /// <summary>
        /// A ViewModel expected to be set as a datacontext to a view.
        /// </summary>
        public AdminViewModel CurrentAdminVM { get; set; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the search for a product
        /// </summary>
        public ICommand AddFilterCommand { get; }

        /// <summary>
        /// A command expected to be databinded to a xaml object governing the search for a product
        /// </summary>
        public ICommand RemoveFilterCommand { get; }

        /// <summary>
        /// An event whose eventhandler is expected to be set from the GUI layer before calling the method 'AddFilter'
        /// within this class
        /// </summary>
        public event Action ResetTextboxRequested;

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
        /// A controller used to control the ProductList view.
        /// Must be used as a singleton.
        /// </summary>
        /// <param name="filterRepo">Pass an implementation of IFilterRepo through the constructor</param>
        public AdminController(IFilterRepo filterRepo)
        {
            _filterRepo = filterRepo;
            CurrentAdminVM = new AdminViewModel();
            AddFilterCommand = new AddFilterCmd(AddFilter);
            RemoveFilterCommand = new RemoveFilterCmd(RemoveFilter);
        }
        /// <summary>
        /// A method that gets all filters from the filterRepo
        /// </summary>
        public void GetAll()
        {
            var filters = _filterRepo.GetAll();
            CurrentAdminVM.Filters = (ObservableCollection<string>)filters;
        }
        /// <summary>
        /// A method that add a ny filter.
        /// It check if the filter already exists in the list of filters.
        /// It also checks if the list of filters is full.
        /// </summary>
        /// <param name="filter">The new filter</param>
        public void AddFilter(string filter)
        {
            foreach (var item in CurrentAdminVM.Filters)
            {
                if (item.ToLower() == filter.ToLower())
                {
                    FilterExistsRequested();
                    return;
                }
            }
            if (CurrentAdminVM.Filters.Count <= 10)
            {
                _filterRepo.Add(filter);
                CurrentAdminVM.Filters.Add(filter);
                ResetTextboxRequested();
                GetAll();
            }
            else
            {
                MaxFiltersRequested();
            }
            
        }
        /// <summary>
        /// A method that remove a filter from the list.
        /// </summary>
        /// <param name="filter">The filter to remove</param>
        public void RemoveFilter(string filter)
        {
            _filterRepo.Remove(filter);
            CurrentAdminVM.Filters.Remove(filter);
        }
    }
}
