﻿#nullable disable
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
    public class InvestmentsController : ControllerBase
    {
        private readonly CNaturalContext _context;
        private readonly Service _investmentService;
        public InvestmentsController(CNaturalContext context)
        {
            _context = context;
            _investmentService = new Service(_context);
        }

        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments()
        {
            var investments = await _investmentService.GetAllInvestments();
            if(investments!= null)
                return Ok(investments);
            else return NotFound();
        }

        // GET: api/Investments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestment(int id)
        {
            var investment  = await _investmentService.GetInvestment(id);
            if(investment!=null)
                return Ok(investment);
            else return NotFound();

            //var investment = await _context.Investments.FindAsync(id);

            //if (investment == null)
            //{
            //    return NotFound();
            //}

            //return investment;
        }

        // PUT: api/Investments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestment(int id, Investment investment)
        {
            if(id!=investment.Id)
                return BadRequest();

            var updatedInvestment = await _investmentService.UpdateInvestment(id, investment);
            if (updatedInvestment != null)
                return Ok(updatedInvestment);   
            else return BadRequest();   //significa que no cumple con la fecha o algo asi.

            ///if (id != investment.Id)
            ///{
            ///    return BadRequest();
            ///}
            
            ///_context.Entry(investment).State = EntityState.Modified;

            ///try
            ///{
            ///    await _context.SaveChangesAsync();
            ///}
            ///catch (DbUpdateConcurrencyException)
            ///{
            ///    if (!InvestmentExists(id))
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

        // POST: api/Investments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investment>> PostInvestment(Investment investment)
        {
            var addedInvestment = await _investmentService.AddInvestment(investment);
            if (addedInvestment != null)
                return CreatedAtAction(nameof(GetInvestment), new { id = investment.Id }, investment);// no se que pasar como 3er parametro
            else return BadRequest();
            //_context.Investments.Add(investment);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInvestment", new { id = investment.Id }, investment);
            
        }

        // DELETE: api/Investments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestment(int id)
        {
            bool isDeleted = await _investmentService.DeleteInvestment(id);
            if (isDeleted) 
                return NoContent();
            else return NotFound();
            //var investment = await _context.Investments.FindAsync(id);
            //if (investment == null)
            //{
            //    return NotFound();
            //}

            //_context.Investments.Remove(investment);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        private bool InvestmentExists(int id)
        {
            return _context.Investments.Any(e => e.Id == id);
        }
    }
}
