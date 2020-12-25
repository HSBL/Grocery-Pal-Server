using System;
using Microsoft.EntityFrameworkCore;

namespace GroceryPalServer.Models
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions<GroceryContext> options) : base(options)
        {
        }

        public DbSet<GroceryItem> groceryItems { get; set; }
    }
}
