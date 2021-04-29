using System;

namespace DomainLayer.EventArgs
{
    public class ProductEventArgs : System.EventArgs
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public float UnitPrice { get; set; }

        public float GuidingPrice { get; set; }

        public int TotalStock { get; set; }

        public bool Blocked { get; set; }

        public int UnitPerPackage { get; set; }

        public float QuantityDiscount { get; set; }

        public DateTime ConfirmedDeliveryDate { get; set; }

        public int ProductNumber { get; set; }

        public string CountryOfOrigin { get; set; }

        public string PurchasingManager { get; set; }

        public string ProductCategory { get; set; }

        public ProductEventArgs() { }

        public ProductEventArgs(ProductEventArgs args)
        {
            Id = args.Id;
            Description = args.Description;
            UnitPrice = args.UnitPrice;
            GuidingPrice = args.GuidingPrice;
            TotalStock = args.TotalStock;
            Blocked = args.Blocked;
            UnitPerPackage = args.UnitPerPackage;
            QuantityDiscount = args.QuantityDiscount;
            ConfirmedDeliveryDate = args.ConfirmedDeliveryDate;
            ProductNumber = args.ProductNumber;
            CountryOfOrigin = args.CountryOfOrigin;
            PurchasingManager = args.PurchasingManager;
            ProductCategory = args.ProductCategory;
        }

        public override string ToString()
        {
            return $"{Description};{UnitPrice};{GuidingPrice};{TotalStock};{Blocked};{UnitPerPackage};{QuantityDiscount};" +
                $"{ConfirmedDeliveryDate.ToString("dd/MM/yyyy")};{ProductNumber};{CountryOfOrigin};{PurchasingManager};{ProductCategory}";
        }
    }
}