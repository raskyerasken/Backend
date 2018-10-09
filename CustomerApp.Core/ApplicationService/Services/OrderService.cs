using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class OrderService: IOrderService
    {
        readonly IOrderRepository _orderRepo;
        readonly IProductRepository _productRepo;

        public OrderService(IOrderRepository orderRepo,
            IProductRepository productRepository)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepository;
        }
        
        public Order New()
        {
            return new Order();
        }

        public Order CreateOrder(Order order)
        {
            if(order.Product == null || order.Product.Id <= 0)
                throw new InvalidDataException("To create Order you need a Customer");
            if(_productRepo.ReadyById(order.Product.Id) == null)
                throw new InvalidDataException("Customer Not found");
            if(order.OrderDate == null)
                throw new InvalidDataException("Order needs a Order Date");

            return _orderRepo.Create(order);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepo.ReadyById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.ReadAll().ToList();
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            return _orderRepo.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepo.Delete(id);
        }
    }
}