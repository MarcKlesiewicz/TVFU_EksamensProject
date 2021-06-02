using System;
using System.ComponentModel;
using DomainLayer.EventArgs;
using DomainLayer.Models;

namespace Application.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; }

        private string _productNumber;
        public string ProductNumber { get { return _productNumber; } set { _productNumber = value; OnPropertyChanged("ProductNumber"); } }

        private string _description;
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }

        private double _unitPrice;
        public double UnitPrice { get { return _unitPrice; } set { _unitPrice = value; OnPropertyChanged("UnitPrice"); } }

        private double _guidingPrice;
        public double GuidingPrice { get { return _guidingPrice; } set { _guidingPrice = value; OnPropertyChanged("GuidingPrice"); } }

        private double _totalStock;
        public double TotalStock { get { return _totalStock; } set { _totalStock = value; OnPropertyChanged("TotalStock"); } }

        private string _blocked;
        public string Blocked { get { return _blocked; } set { _blocked = value; OnPropertyChanged("Blocked"); } }

        private double _unitPerPackage;
        public double UnitPerPackage { get { return _unitPerPackage; } set { _unitPerPackage = value; OnPropertyChanged("UnitPerPackage"); } }

        private double _quantityDiscount;
        public double QuantityDiscount { get { return _quantityDiscount; } set { _quantityDiscount = value; OnPropertyChanged("QuantityDiscount"); } }

        private string _purchasingManager;
        public string PurchasingManager { get { return _purchasingManager; } set { _purchasingManager = value; OnPropertyChanged("PurchasingManager"); } }

        private string _confirmedDeliveryDate;
        public string ConfirmedDeliveryDate
        {
            get { return _confirmedDeliveryDate; }
            set
            { _confirmedDeliveryDate = value; OnPropertyChanged("ConfirmedDeliveryDate"); }
        }

        private string _countryOfOrigin;
        public string CountryOfOrigin { get { return _countryOfOrigin; } set { _countryOfOrigin = value; OnPropertyChanged("CountryOfOrigin"); } }

        public ProductViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ProductViewModel(ProductEventArgs args)
        {
            Id = args.Id;
            _description = args.Description;
            _unitPrice = args.UnitPrice;
            _guidingPrice = args.GuidingPrice;
            _totalStock = args.TotalStock;
            _blocked = args.Blocked;
            _unitPerPackage = args.UnitPerPackage;
            _quantityDiscount = args.QuantityDiscount;
            _confirmedDeliveryDate = (args.ConfirmedDeliveryDate == default(DateTime)) ? String.Empty : args.ConfirmedDeliveryDate.ToString("dd-MM-yyyy");
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
        }

        public ProductViewModel(Product args)
        {
            Id = args.Id;
            _description = args.Description;
            _unitPrice = args.UnitPrice;
            _guidingPrice = args.GuidingPrice;
            _totalStock = args.TotalStock;
            _blocked = args.Blocked;
            _unitPerPackage = args.UnitPerPackage;
            _quantityDiscount = args.QuantityDiscount;
            _confirmedDeliveryDate = (args.ConfirmedDeliveryDate == default(DateTime)) ? String.Empty : args.ConfirmedDeliveryDate.ToString("dd-MM-yyyy");
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
        }

        public ProductViewModel(ProductViewModel args)
        {
            Id = args.Id;
            _description = args.Description;
            _unitPrice = args.UnitPrice;
            _guidingPrice = args.GuidingPrice;
            _totalStock = args.TotalStock;
            _blocked = args.Blocked;
            _unitPerPackage = args.UnitPerPackage;
            _quantityDiscount = args.QuantityDiscount;
            _confirmedDeliveryDate = args.ConfirmedDeliveryDate;
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
        }

        public void Update(ProductEventArgs args)
        {
            _description = args.Description;
            _unitPrice = args.UnitPrice;
            _guidingPrice = args.GuidingPrice;
            _totalStock = args.TotalStock;
            _blocked = args.Blocked;
            _unitPerPackage = args.UnitPerPackage;
            _quantityDiscount = args.QuantityDiscount;
            _confirmedDeliveryDate = (args.ConfirmedDeliveryDate == default(DateTime)) ? String.Empty : args.ConfirmedDeliveryDate.ToString("dd-MM-yyyy");
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
        }

        public void Update(ProductViewModel args)
        {
            _description = args.Description;
            _unitPrice = args.UnitPrice;
            _guidingPrice = args.GuidingPrice;
            _totalStock = args.TotalStock;
            _blocked = args.Blocked;
            _unitPerPackage = args.UnitPerPackage;
            _quantityDiscount = args.QuantityDiscount;
            _confirmedDeliveryDate = args.ConfirmedDeliveryDate;
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return $"{Description};{UnitPrice};{GuidingPrice};{TotalStock};{Blocked};{UnitPerPackage};{QuantityDiscount};" +
                $"{ConfirmedDeliveryDate};{ProductNumber};{CountryOfOrigin};{PurchasingManager}";
        }
    }
}