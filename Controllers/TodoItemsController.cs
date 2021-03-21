using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunProcApi.Models;

namespace RunProcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunProcItemsController : ControllerBase
    {
        private readonly RunProcContext _context;

        public RunProcItemsController(RunProcContext context)
        {
            _context = context;
        }

        // GET: api/RunProcItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunProcItem>>> GetRunProcItems()
        {
            return await _context.RunProcItems.ToListAsync();
        }

        // GET: api/RunProcItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RunProcItem>> GetRunProcItem(long id)
        {
            var RunProcItem = await _context.RunProcItems.FindAsync(id);

            if (RunProcItem == null)
            {
                return NotFound();
            }

            return RunProcItem;
        }

        // PUT: api/RunProcItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRunProcItem(long id, RunProcItem RunProcItem)
        {
            if (id != RunProcItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(RunProcItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RunProcItemExists(id))
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

        // POST: api/RunProcItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RunProcItem>> PostRunProcItem(RunProcItem RunProcItem)
        {
            _context.RunProcItems.Add(RunProcItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetRunProcItem", new { id = RunProcItem.Id }, RunProcItem);
            return CreatedAtAction(nameof(GetRunProcItem), new { id = RunProcItem.Id }, RunProcItem);
        }

        // DELETE: api/RunProcItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunProcItem(long id)
        {
            var RunProcItem = await _context.RunProcItems.FindAsync(id);
            if (RunProcItem == null)
            {
                return NotFound();
            }

            _context.RunProcItems.Remove(RunProcItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RunProcItemExists(long id)
        {
            return _context.RunProcItems.Any(e => e.Id == id);
        }
    }
}
