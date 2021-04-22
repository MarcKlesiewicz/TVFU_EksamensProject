using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Application.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ProductViewModel> ViewModels { get; set; }

        public ProductListViewModel()
        {
            ViewModels = new ObservableCollection<ProductViewModel>();
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