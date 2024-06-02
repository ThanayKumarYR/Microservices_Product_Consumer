using Customer.Microservice.ModelLayer;
using Customer.Microservice.RepositoryLayer.Entity;
using Customer.Microservice.RepositoryLayer.Interface;
using Microsoft.AspNetCore.Mvc;
namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositoryLayer _productRepository;

        public ProductController(IProductRepositoryLayer productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductEntity productEntity)
        {
            if (productEntity == null)
            {
                return BadRequest(new ResponseModel<ProductEntity>
                {
                    Success = false,
                    Message = "Product entity is null"
                });
            }

            var createdProduct = await _productRepository.Create(productEntity);
            return Ok(new ResponseModel<ProductEntity>
            {
                Success = true,
                Data = createdProduct,
                Message = "Product created successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();
            return Ok(new ResponseModel<IEnumerable<ProductEntity>>
            {
                Success = true,
                Data = products,
                Message = "Products retrieved successfully"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound(new ResponseModel<ProductEntity>
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ResponseModel<ProductEntity>
            {
                Success = true,
                Data = product,
                Message = "Product retrieved successfully"
            });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ProductEntity>>> UpdateProduct(int id, ProductEntity productEntity)
        {
            var response = new ResponseModel<ProductEntity>();
            if (id != productEntity.Id)
            {
                response.Success = false;
                response.Message = "Product ID mismatch";
                return BadRequest(response);
            }

            try
            {
                var updatedProduct = await _productRepository.Update(productEntity);
                if (updatedProduct == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    return NotFound(response);
                }
                response.Success = true;
                response.Data = updatedProduct;
                response.Message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            var deleteMessage = await _productRepository.Delete(product);
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Data = deleteMessage,
                Message = "Product deleted successfully"
            });
        }
    }
}
