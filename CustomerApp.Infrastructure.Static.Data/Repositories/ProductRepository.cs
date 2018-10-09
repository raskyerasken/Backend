using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Static.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public ProductRepository()
        {
            if (FakeDB.Products.Count >= 1) return;
            var product1 = new Products()
            {
                Id = FakeDB.Id ++,
                ProductType = "Shoe",
                ProductSize = "10.5    ",
                ProductPrice = "200"
            };
            FakeDB.Products.Add(product1);

            var prod2 = new Products()
            {
                Id = FakeDB.Id ++,
                ProductType = "Lars",
                ProductSize = "Bilde",
                ProductPrice = "Ostestrasse 202"
            };
            FakeDB.Products.Add(prod2);
        }
        
        public Products Create(Products products)
        {
            products.Id = FakeDB.Id++;
            FakeDB.Products.Add(products);
            return products;
        }

        public IEnumerable<Products> ReadAll()
        {
            return FakeDB.Products;
        }

        public Products ReadyById(int id)
        {
            return FakeDB.Products.
                Select(c => new Products()
                {
                    Id = c.Id,
                    ProductType = c.ProductType,
                    ProductPrice = c.ProductPrice,
                    ProductSize = c.ProductSize
                }).
                FirstOrDefault(c => c.Id == id);
            
        }

        //Remove later when we use UOW
        public Products Update(Products productsUpdate)
        {
            var productFromDB = ReadyById(productsUpdate.Id);
            if (productFromDB == null) return null;
            
            productFromDB.ProductType = productsUpdate.ProductType;
            productFromDB.ProductSize = productsUpdate.ProductSize;
            productFromDB.ProductPrice = productsUpdate.ProductPrice;
            return productFromDB;
        }

        public Products Delete(int id)
        {
            var productFound = ReadyById(id);
            if (productFound == null) return null;
            
            FakeDB.Products.Remove(productFound);
            return productFound;
        }

    }
}
