using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.RequestHelpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> repo;

        public ProductsController(IGenericRepository<Product> repo)
        {
            this.repo = repo;
        }

        // =============================
        // GET PRODUCTS (WITH PAGINATION)
        // =============================
        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProducts(
            [FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

        

            return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);

        }

        // ============
        // GET BY ID
        // ============
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        // ============
        // CREATE
        // ============
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }

            return BadRequest("Problem creating product");
        }

        // ============
        // UPDATE
        // ============
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !await ProductExists(id))
                return BadRequest("Cannot update this product");

            if (await repo.SaveAllAsync())
                return NoContent();

            return BadRequest("Problem updating the product");
        }

        // ============
        // DELETE
        // ============
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null)
                return NotFound();

           repo.Remove(product);


            if (await repo.SaveAllAsync())
                return NoContent();

            return BadRequest("Problem deleting the product");
        }

        // ============
        // BRANDS
        // ============
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        // ============
        // TYPES
        // ============
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        private async Task<bool> ProductExists(int id)
        {
            return await repo.GetByIdAsync(id) != null;
        }
    }
}
