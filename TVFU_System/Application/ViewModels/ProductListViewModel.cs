using System.Collections.Generic;
using System.ComponentModel;

namespace Application.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ProductViewModel> ViewModels { get; set; }

        public ProductListViewModel()
        {
            ViewModels = new List<ProductViewModel>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}