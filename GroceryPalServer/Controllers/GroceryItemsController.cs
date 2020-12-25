using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryPalServer.Models;

namespace GroceryPalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryItemsController : ControllerBase
    {
        private readonly GroceryContext _context;

        public GroceryItemsController(GroceryContext context)
        {
            _context = context;
        }

        // GET: api/GroceryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceryItem>>> GetgroceryItems()
        {
            return await _context.groceryItems.ToListAsync();
        }

        // GET: api/GroceryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryItem>> GetGroceryItem(int id)
        {
            var groceryItem = await _context.groceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return NotFound();
            }

            return groceryItem;
        }

        // PUT: api/GroceryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroceryItem(int id, GroceryItem groceryItem)
        {
            if (id != groceryItem.Id)
            {
                return BadRequest();
            }

            groceryItem.Updated = DateTime.UtcNow;
            _context.Entry(groceryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(id))
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

        [HttpPut("{id}/Picked")]
        public async Task<IActionResult> PickedPutGroceryItem(int id)
        {

            var groceryItem = await _context.groceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return BadRequest();
            }

            groceryItem.Updated = DateTime.UtcNow;
            groceryItem.AlreadyPicked = true;
            _context.Entry(groceryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(id))
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

        [HttpPatch("{id}/Picked")]
        public async Task<IActionResult> PickGroceryItem(int id)
        {
            var groceryItem = await _context.groceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return BadRequest();
            }

            groceryItem.Updated = DateTime.UtcNow;
            groceryItem.AlreadyPicked = true;
            _context.Entry(groceryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(id))
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

        // POST: api/GroceryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroceryItem>> PostGroceryItem(GroceryItem groceryItem)
        {
            groceryItem.Updated = DateTime.UtcNow;
            groceryItem.AlreadyPicked = false;
            _context.groceryItems.Add(groceryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroceryItem), new { id = groceryItem.Id }, groceryItem);
        }

        // DELETE: api/GroceryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryItem(int id)
        {
            var groceryItem = await _context.groceryItems.FindAsync(id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            _context.groceryItems.Remove(groceryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearGroceryItem()
        {
            var groceryItems = await _context.groceryItems.Where(x => x.AlreadyPicked == true).ToListAsync();
            if (groceryItems == null)
            {
                return NotFound();
            }

            _context.groceryItems.RemoveRange(groceryItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Destroy")]
        public async Task<IActionResult> DestroyGroceryItem()
        {
            var groceryItems = await _context.groceryItems.Where(x => true).ToListAsync();
            if (groceryItems == null)
            {
                return NotFound();
            }

            _context.groceryItems.RemoveRange(groceryItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroceryItemExists(int id)
        {
            return _context.groceryItems.Any(e => e.Id == id);
        }
    }
}
