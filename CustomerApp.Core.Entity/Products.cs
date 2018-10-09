using System.Collections.Generic;

namespace CustomerApp.Core.Entity
{
    public class Products
    {
        public int Id { get; set; }

        public string ProductType { get; set; }

        public string ProductPrice { get; set; }

        public string ProductSize { get; set; }
        
        public string ProductStock { get; set; }

        public List<Order> Orders { get; set; }
    }
}
