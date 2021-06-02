using System;

namespace DomainLayer.EventArgs
{
    public class ProductEventArgs : System.EventArgs
    {
        public string Id { get; set; }

        public string ProductNumber { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public double GuidingPrice { get; set; }

        public double TotalStock { get; set; }

        public string Blocked { get; set; }

        public double UnitPerPackage { get; set; }

        public double QuantityDiscount { get; set; }

        public string PurchasingManager { get; set; }

        public DateTime ConfirmedDeliveryDate { get; set; }

        public string CountryOfOrigin { get; set; }

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
        }

        public override string ToString()
        {
            return $"{Description};{UnitPrice};{GuidingPrice};{TotalStock};{Blocked};{UnitPerPackage};{QuantityDiscount};" +
                $"{ConfirmedDeliveryDate.ToString("dd-MM-yyyy")};{ProductNumber};{CountryOfOrigin};{PurchasingManager}";
        }
    }
}