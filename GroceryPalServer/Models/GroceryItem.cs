using System;
namespace GroceryPalServer.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool AlreadyPicked { get; set; }
        public DateTime Updated { get; set; }
    }
}
