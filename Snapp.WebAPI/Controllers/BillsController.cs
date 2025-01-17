﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core;
using Entity;
using System.Text.Json;

namespace Snapp.WebAPI.Controllers
{
    [Route("api/bills/")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly CoreContext _context;

        public BillsController(CoreContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBill()
        {
            return await _context.Bill.ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<Bill>> GetBill(string id)
        {
            var bill = await _context.Bill.FindAsync(id);


            //if (bill == null)
            //{
            //    return NotFound();
            //}

            //return bill;

            var billAsJson = JsonSerializer.Serialize(bill);

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BillExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(billAsJson);


        }

        // GET: api/Bills/5
        [HttpGet("getbyprojectid/{id}")]
        public async Task<ActionResult<List<Bill>>> GetBillByProjectId(string id)
        {
            return await _context.Bill.Where(s => s.ProjectId == id).ToListAsync();
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("updatebyid/{id}")]
        public async Task<IActionResult> PutBill(string id, Bill bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add")]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            _context.Bill.Add(bill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BillExists(bill.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("deletebyid/{id}")]
        public async Task<IActionResult> DeleteBill(string id)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillExists(string id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
