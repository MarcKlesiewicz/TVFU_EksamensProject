﻿using System;
using System.ComponentModel;
using DomainLayer.EventArgs;
using DomainLayer.Models;

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
        public DateTime ConfirmedDeliveryDate
        {
            get { return _confirmedDeliveryDate; }
            set
            { _confirmedDeliveryDate = ((DateTime?)value).Value; OnPropertyChanged("ConfirmedDeliveryDate"); }
        }

        private int _productNumber;
        public int ProductNumber { get { return _productNumber; } set { _productNumber = value; OnPropertyChanged("ProductNumber"); } }

        private string _countryOfOrigin;
        public string CountryOfOrigin { get { return _countryOfOrigin; } set { _countryOfOrigin = value; OnPropertyChanged("CountryOfOrigin"); } }

        private string _purchasingManager;
        public string PurchasingManager { get { return _purchasingManager; } set { _purchasingManager = value; OnPropertyChanged("PurchasingManager"); } }

        private string _productCategory;
        public string ProductCategory { get { return _productCategory; } set { _productCategory = value; OnPropertyChanged("ProductCategory"); } }

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
            _productCategory = args.ProductCategory;
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
            _confirmedDeliveryDate = args.ConfirmedDeliveryDate;
            _productNumber = args.ProductNumber;
            _countryOfOrigin = args.CountryOfOrigin;
            _purchasingManager = args.PurchasingManager;
            _productCategory = args.ProductCategory;
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
            _productCategory = args.ProductCategory;
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
            _productCategory = args.ProductCategory;
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
            _productCategory = args.ProductCategory;
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
                $"{ConfirmedDeliveryDate.ToString("dd/MM/yyyy")};{ProductNumber};{CountryOfOrigin};{PurchasingManager};{ProductCategory}";
        }
    }
}