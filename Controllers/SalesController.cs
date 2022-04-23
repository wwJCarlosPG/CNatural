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
    public class SalesController : ControllerBase
    {
        private readonly CNaturalContext _context;
        private readonly Service _saleService;
        public SalesController(CNaturalContext context)
        {
            _context = context;
            _saleService = new Service(_context);
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            //return await _context.Sales.ToListAsync();
            var sales = await _saleService.GetAllSales();
            if(sales != null)
                return Ok(sales);
            return NotFound();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale= await _saleService.GetSale(id);
            if (sale != null)
                return sale;
            return NotFound();
            //var sale = await _context.Sales.FindAsync(id);

            //if (sale == null)
            //{
            //    return NotFound();
            //}

            //return sale;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if(!SaleExists(id))
                return NotFound();  
            if(id!= sale.Id)
                return BadRequest();
            var updatedSale = await _saleService.UpdateSale(id, sale);
            if (updatedSale != null)
                return Ok(updatedSale);
            else return BadRequest();
            //No se que response mandar aqui.

            ///if (id != sale.Id)
            ///{
            ///    return BadRequest();
            ///}

            ///_context.Entry(sale).State = EntityState.Modified;

            ///try
            ///{
            ///    await _context.SaveChangesAsync();
            ///}
            ///catch (DbUpdateConcurrencyException)
            ///{
            ///    if (!SaleExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            var createdSale = await _saleService.AddSale(sale);
            if (createdSale != null)
                return Ok(createdSale);
            else return BadRequest();
            //No se que devolver si no devuelvo ok

            //_context.Sales.Add(sale);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (!SaleExists(id))
                return NotFound();
            bool isDeleted = await _saleService.DeleteSale(id);
            if (isDeleted)
                return NoContent();
            else return BadRequest();
            //var sale = await _context.Sales.FindAsync(id);
            //if (sale == null)
            //{
            //    return NotFound();
            //}

            //_context.Sales.Remove(sale);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
