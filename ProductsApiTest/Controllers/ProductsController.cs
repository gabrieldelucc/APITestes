using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsApiTest.Domains;
using ProductsApiTest.Interfaces;
using ProductsApiTest.Repositories;

namespace ProductsApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository x)
        {
            _productsRepository = x;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_productsRepository.Get());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Products product = _productsRepository.GetById(id);

                // Se o produto não for encontrado, retorna NotFound
                if (product == null)
                {
                    return NotFound("Produto não encontrado");
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Products product)
        {
            try
            {
                _productsRepository.Post(product);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Products product)
        {
            try
            {
                Products prod = _productsRepository.GetById(id);

                // Se o produto não for encontrado, retorna NotFound
                if (prod == null)
                {
                    return NotFound("Produto não encontrado");
                }

                _productsRepository.Put(id, product);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                Products products = _productsRepository.GetById(id);

                // Se o produto não for encontrado, retorna NotFound
                if (products == null)
                {
                    return NotFound("Produto não encontrado");
                }

                _productsRepository.Delete(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
