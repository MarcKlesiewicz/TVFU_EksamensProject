using System;

namespace DomainLayer.EventArgs
{
    public class ProductEventArgs : System.EventArgs
    {
        public string Name { get; set; }

        public Guid Id { get; set; }
    }
}
