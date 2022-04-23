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
    public class BuyersController : ControllerBase
    {
        private readonly CNaturalContext _context;
        private readonly Service _buyerService;
        public BuyersController(CNaturalContext context)
        {
            _context = context;
            _buyerService = new Service(_context);
        }

        // GET: api/Buyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        {
            var buyers = await _buyerService.GetBuyers();
            if (buyers != null)
                return Ok(buyers);
            else return NotFound();
            //return await _context.Buyers.ToListAsync();
        }

        // GET: api/Buyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var buyer = await _buyerService.GetBuyer(id);
            if (buyer!= null)
                return Ok(buyer);
            return NotFound();
            //var buyer = await _context.Buyers.FindAsync(id);

                //if (buyer == null)
                //{
                //    return NotFound();
                //}

                //return buyer;
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if(id!= buyer.Id)
                return BadRequest();
            var updatedBuyer = await _buyerService.UpdateBuyer(id, buyer);
            if (updatedBuyer != null)
                return Ok(updatedBuyer);
            else return NotFound();

            ///if (id != buyer.Id)
            ///{
            ///    return BadRequest();
            ///}

            ///_context.Entry(buyer).State = EntityState.Modified;

            ///try
            ///{
            ///    await _context.SaveChangesAsync();
            ///}
            ///catch (DbUpdateConcurrencyException)
            ///{
            ///    if (!BuyerExists(id))
            ///    {
            ///        return NotFound();
            ///    }
            ///    else
            ///    {
            ///        throw;
            ///    }
            ///}

            //return NoContent();
        }

        // POST: api/Buyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
        {
            var createdBuyer = await _buyerService.AddBuyer(buyer);
            if (createdBuyer != null)
                return CreatedAtAction("GetBuyer",new {id = buyer.Id}, buyer); //Parametros: 
            //el nombre de la accion que se usara para generar la url, los datos de la ruta que se usaran para generar la url
            //el valor del contenido al que se le va a dar formato en el cuerpo de la entidad.
            else return NotFound();


            //_context.Buyers.Add(buyer);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            if (!BuyerExists(id))
                return NotFound();
            else 
            {
                bool isDeleted = await _buyerService.DeleteBuyer(id);
                if (isDeleted)
                    return NoContent();
                else return BadRequest();
            }
            //var buyer = await _context.Buyers.FindAsync(id);
            //if (buyer == null)
            //{
            //    return NotFound();
            //}

            //_context.Buyers.Remove(buyer);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyers.Any(e => e.Id == id && e.IsDeleted == false);
        }
    }
}
