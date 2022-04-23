#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNaturalApi.Context;
using CNaturalApi.Models;
using CNaturalApi.Repository.Services;
namespace CNaturalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CNaturalContext _context;
        private readonly Service _productService;
        public ProductController(CNaturalContext context)
        {
            _context = context;
            _productService = new Service(_context);
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products != null)
                return Ok(products);
            else return BadRequest();
            //return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await _productService.GetProduct(id);
            if(product !=null)
                return Ok(product);
            else return NotFound();
        ///antes estaba esto
        ///    var product = await _context.Products.FindAsync(id);
        ///    if (product == null)
        ///    {
        ///        return NotFound();
        ///    }
        ///    return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (product.Id != id)
                return BadRequest();
            else 
            {
                var p = await _productService.UpdateProduct(id, product);
                if (p != null)
                    return Ok(p);
                else return NotFound();
            }
            //ver bien lo que estaba antes, preguntar.

            ///antes estaba esto
            ///if (id != product.Id)
            ///{
            ///    return BadRequest();
            ///}
            ///_context.Entry(product).State = EntityState.Modified;
            
            ///try
            ///{
            ///    await _context.SaveChangesAsync();
            ///}
            ///catch (DbUpdateConcurrencyException)
            ///{
            ///    if (!ProductExists(id))
            ///    {
            ///        return NotFound();
            ///    }
            ///    else
            ///    {
            ///        throw;
            ///    }
            ///}
            ///return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //Comprobar si no existe un producto con ese nombre.
            var addedProduct = await _productService.AddProduct(product);
            if(addedProduct != null)
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            //return Ok(addedProduct);
            else return BadRequest();

            ///antes estaba esto
            ///_context.Products.Add(product);
            ///await _context.SaveChangesAsync();
            ///return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ///no se si un producto se pueda borrar
            ///lo digo porque si borras que pasa con la lista 
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
