using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freezeapi.Models;

namespace freezeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreezeController : ControllerBase
    {
        private readonly FreezeTeqContext _context;

        public FreezeController(FreezeTeqContext context)
        {
            _context = context;
        }

        // GET: api/Freeze
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreezeItem>>> GetFreezeItems()
        {
            return await _context.FreezeItems.ToListAsync();
        }

        // GET: api/Freeze/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FreezeItem>> GetFreezeItem(long id)
        {
            var freezeItem = await _context.FreezeItems.FindAsync(id);

            if (freezeItem == null)
            {
                return NotFound();
            }

            return freezeItem;
        }

        // PUT: api/Freeze/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFreezeItem(long id, FreezeItem freezeItem)
        {
            if (id != freezeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(freezeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreezeItemExists(id))
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

        // POST: api/Freeze
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FreezeItem>> PostFreezeItem(FreezeItem freezeItem)
        {
            _context.FreezeItems.Add(freezeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFreezeItem", new { id = freezeItem.Id }, freezeItem);
        }

        // DELETE: api/Freeze/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreezeItem(long id)
        {
            var freezeItem = await _context.FreezeItems.FindAsync(id);
            if (freezeItem == null)
            {
                return NotFound();
            }

            _context.FreezeItems.Remove(freezeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FreezeItemExists(long id)
        {
            return _context.FreezeItems.Any(e => e.Id == id);
        }
    }
}
