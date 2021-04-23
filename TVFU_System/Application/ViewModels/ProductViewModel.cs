using System;
using System.ComponentModel;
using DomainLayer.EventArgs;

namespace Application.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; }

        private string _description;
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }

        private float _unitPrice;
        public float UnitPrice { get { return _unitPrice; } set { _unitPrice = value; OnPropertyChanged("UnitPrice"); } }

        private float _guidingPrice;
        public float GuidingPrice { get { return _guidingPrice; } set { _guidingPrice = value; OnPropertyChanged("GuidingPrice"); } }

        private int _totalStock;
        public int TotalStock { get { return _totalStock; } set { _totalStock = value; OnPropertyChanged("TotalStock"); } }

        private bool _blocked;
        public bool Blocked { get { return _blocked; } set { _blocked = value; OnPropertyChanged("Blocked"); } }

        private int _unitPerPackage;
        public int UnitPerPackage { get { return _unitPerPackage; } set { _unitPerPackage = value; OnPropertyChanged("UnitPerPackage"); } }

        private float _quantityDiscount;
        public float QuantityDiscount { get { return _quantityDiscount; } set { _quantityDiscount = value; OnPropertyChanged("QuantityDiscount"); } }

        private DateTime _confirmedDeliveryDate;
        public DateTime ConfirmedDeliveryDate { get { return _confirmedDeliveryDate; } set { _confirmedDeliveryDate = value; OnPropertyChanged("ConfirmedDeliveryDate"); } }

        private int _productNumber;
        public int ProductNumber { get { return _productNumber; } set { _productNumber = value; OnPropertyChanged("ProductNumber"); } }

        private string _countryOfOrigin;
        public string CountryOfOrigin { get { return _countryOfOrigin; } set { _countryOfOrigin = value; OnPropertyChanged("CountryOfOrigin"); } }

        private string _purchasingManager;
        public string PurchasingManager { get { return _purchasingManager; } set { _purchasingManager = value; OnPropertyChanged("PurchasingManager"); } }

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
            _confirmedDeliveryDate = args.ConfirmedDeliveryDate;
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
            return $"{_description};{_unitPrice};{_guidingPrice};{_totalStock};{_blocked};{_unitPerPackage};{_quantityDiscount};" +
                $"{_confirmedDeliveryDate.ToString()};{_productNumber};{_countryOfOrigin};{_purchasingManager}";
        }
    }
}
