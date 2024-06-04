using courseWork_API.Model;
using courseWork_API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace courseWork_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetProducts();
            return new OkObjectResult(products);
            //return new string[] { "value1", "value2" };
        }


        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            //var product = _productRepository.GetProductById(id);
            //return new OkObjectResult(product);
            //return "value";
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            /*using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }*/

            if (product == null)
            {
                return BadRequest();
            }

            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            /*if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();*/

            if (product == null || product.Id != id)
            {
                return BadRequest();
            }

            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            using (var scope = new TransactionScope())
            {
                _productRepository.UpdateProduct(product);
                scope.Complete();
                return Ok();
            }
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //_productRepository.DeleteProduct(id);
            //return new OkResult();
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(id);
            return Ok();
        }

    }
}
