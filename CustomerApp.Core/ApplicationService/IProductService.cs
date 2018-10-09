using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface IProductService
    {
        //New Customer
        Products NewProduct(string productType,
                                string productSize,
                                string productPrice);

        //Create //POST
        Products CreateProduct(Products prod);
        //Read //GET
        Products FindProductById(int id);
        Products FindProductByIdIncludeOrders(int id);
        List<Products> GetAllProducts();
        List<Products> GetAllByID(string name);
        //Update //PUT
        Products UpdateProduct(Products productsUpdate);
        
        //Delete //DELETE
        Products DeleteProduct(int id);
    }
}
