﻿using System;

namespace DomainLayer.EventArgs
{
    public class ProductEventArgs : System.EventArgs
    {
        public Guid Id { get; set; }

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
    }
}
