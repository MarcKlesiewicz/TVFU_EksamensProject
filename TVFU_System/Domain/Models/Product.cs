using System;

namespace DomainLayer.Models
{
    public class Product : IComparable
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

        public string ProductCategory { get; set; }

        public int CompareTo(object obj)
        {
            return String.Compare(this.ProductCategory.ToLower(), (obj as Product).ProductCategory.ToLower());
        }

        public override string ToString()
        {
            return $"{Description};{UnitPrice};{GuidingPrice};{TotalStock};{Blocked};{UnitPerPackage};{QuantityDiscount};" +
                $"{ConfirmedDeliveryDate.ToString("dd-MM-yyyy")};{ProductNumber};{CountryOfOrigin};{PurchasingManager};{ProductCategory}";
        }
    }
}