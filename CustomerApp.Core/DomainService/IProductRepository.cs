using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface IProductRepository
    {
        //ProductRepository Interface
        //Create Data
        //No Id when enter, but Id when exits
        Products Create(Products products);
        //Read Data
        Products ReadyById(int id);
        IEnumerable<Products> ReadAll();
        //Update Data
        Products Update(Products productsUpdate);
        //Delete Data
        Products Delete(int id);
    }
}
