using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.ViewModels;
using Persistence.Repositories.Interfaces;
using Application.Commands;

namespace Application.Controllers
{
    public class AdminController
    {
        private readonly IFilterRepo _filterRepo;

        public AdminViewModel CurrentAdminVM;

        public ICommand AddFilterCommand { get; }

        public ICommand RemoveFilterCommand { get; }

        public event Action ResetTextboxRequested;

        public event Action MaxFiltersRequested;

        public event Action FilterExistsRequested;

        public AdminController(IFilterRepo filterRepo)
        {
            _filterRepo = filterRepo;
            CurrentAdminVM = new AdminViewModel();
            AddFilterCommand = new AddFilterCmd(AddFilter);
            RemoveFilterCommand = new RemoveFilterCmd(RemoveFilter);
        }

        public void GetAll()
        {
            CurrentAdminVM.Filters.Clear();
            var filters = _filterRepo.GetAll();
            foreach (var item in filters)
            {
                CurrentAdminVM.Filters.Add(item);
            }
        }

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

        public void RemoveFilter(string filter)
        {
            _filterRepo.Remove(filter);
            CurrentAdminVM.Filters.Remove(filter);
        }
    }
}
