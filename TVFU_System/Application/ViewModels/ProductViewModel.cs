using System;
using System.ComponentModel;
using Application.EventArgs;

namespace Application.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; } = new Guid();

        public string Name { get; set; }

        public ProductViewModel()
        {
            
        }

        public ProductViewModel(ProductEventArgs args)
        {

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
