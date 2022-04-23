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
    public class AccountanciesController : ControllerBase
    {
        private readonly CNaturalContext _context;
        private readonly Service _accountancyService;
        public AccountanciesController(CNaturalContext context)
        {
            _context = context;
            _accountancyService = new Service(_context);
        }

        // GET: api/Accountancies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accountancy>>> GetAccountancies()
        {
            var accountancies = await _accountancyService.GetAllAccountancies();
            if (accountancies != null)
                return Ok(accountancies);
            else return NotFound();
            //return await _context.Accountancies.ToListAsync();
        }

        // GET: api/Accountancies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accountancy>> GetAccountancy(int id)
        {
            var accountancy = await _accountancyService.GetAccountancy(id);
            if (accountancy != null)
                return Ok(accountancy);
            else return NotFound();
            //var accountancy = await _context.Accountancies.FindAsync(id);

            //if (accountancy == null)
            //{
            //    return NotFound();
            //}

            //return accountancy;
        }

        // PUT: api/Accountancies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountancy(int id, Accountancy accountancy)
        {
            if(id!= accountancy.Id)
                return BadRequest();
            var updatedAccountancy = await _accountancyService.UpdateAccountancy(id, accountancy);
            if (updatedAccountancy != null)
                return Ok(accountancy);
            else return BadRequest();
            ///if (id != accountancy.Id)
            ///{
            ///    return BadRequest();
            ///}

            ///_context.Entry(accountancy).State = EntityState.Modified;

            ///try
            ///{
            ///    await _context.SaveChangesAsync();
            ///}
            ///catch (DbUpdateConcurrencyException)
            ///{
            ///    if (!AccountancyExists(id))
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

        // POST: api/Accountancies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accountancy>> PostAccountancy(Accountancy accountancy)
        {
            var createdAccountancy = await _accountancyService.AddAccountancy(accountancy);
            if (createdAccountancy != null)
                return CreatedAtAction(nameof(GetAccountancy), new { id = accountancy.Id }, accountancy);
            else return BadRequest();
            //_context.Accountancies.Add(accountancy);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAccountancy", new { id = accountancy.Id }, accountancy);
        }

        
        private bool AccountancyExists(int id)
        {
            return _context.Accountancies.Any(e => e.Id == id);
        }
    }
}
