using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        // GET api/customers -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<Products>> Get()
        {
            ///Products with all there orders? NO
            return _productService.GetAllProducts();
        }

        // GET api/customers/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<Products> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            
            //return _customerService.FindCustomerById(id);
            return _productService.FindProductByIdIncludeOrders(id);
        }

        // POST api/customers -- CREATE
        [HttpPost]
        public ActionResult<Products> Post([FromBody] Products products)
        {
            if (string.IsNullOrEmpty(products.ProductType))
            {
                return BadRequest("Product name is required for creating a product");
            }

            if (string.IsNullOrEmpty(products.ProductSize))
            {
                return BadRequest("Product Size is required for creating a product");
            }
            //return StatusCode(503, "Horrible Error CALL Tech Support");
            return _productService.CreateProduct(products);
        }
        
        // PUT api/customers/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<Products> Put(int id, [FromBody] Products products)
        {
            if (id < 1 || id != products.Id)
            {
                return BadRequest("Parameter Id and product ID must be the same");
            }

            return Ok(_productService.UpdateProduct(products));
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public ActionResult<Products> Delete(int id)
        {
            var customer = _productService.DeleteProduct(id);
            if (customer == null)
            {
                return StatusCode(404, "Did not find product with ID " + id);
            }

            return Ok($"Product with Id: {id} is Deleted");
        }
    }
}
