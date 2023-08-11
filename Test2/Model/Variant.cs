using System;

namespace Test2.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int InventoryQuantity { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
