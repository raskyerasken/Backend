using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class ProductService: IProductService
    {
        readonly IProductRepository _productRepo;
        readonly IOrderRepository _orderRepo;

        public ProductService(IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _productRepo = productRepository;
            _orderRepo = orderRepository;
        }

        public Products NewProduct(string productType, string productSize, string productPrice)
        {
            var prod = new Products()
            {
                ProductType = productType,
                ProductSize= productSize,
                ProductPrice = productPrice
            };

            return prod;
        }

        public Products CreateProduct(Products prod)
        {
            return _productRepo.Create(prod);
        }

        public Products FindProductById(int id)
        {
            return _productRepo.ReadyById(id);
        }

        public Products FindProductByIdIncludeOrders(int id)
        {
            var product = _productRepo.ReadyById(id);
            product.Orders = _orderRepo.ReadAll()
                .Where(order => order.Product.Id == product.Id)
                .ToList();
            return product;
        }
        

        public List<Products> GetAllProducts()
        {
            return _productRepo.ReadAll().ToList(); 
        }

        public List<Products> GetAllByID(string name)
        {
            var list = _productRepo.ReadAll();
            var queryContinued = list.Where(prod => prod.ProductType.Equals(name));
            queryContinued.OrderBy(customer => customer.ProductType);
            //Not executed anything yet
            return queryContinued.ToList();
        }

        public Products UpdateProduct(Products productsUpdate)
        {
            var product = FindProductById(productsUpdate.Id);
            product.ProductType = productsUpdate.ProductType;
            product.ProductSize = productsUpdate.ProductSize;
            product.ProductPrice = productsUpdate.ProductPrice;
            return product;
        }

        public Products DeleteProduct(int id)
        {
            return _productRepo.Delete(id);
        }
    }
}
