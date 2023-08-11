using System;

namespace Test2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string ProductType { get; set; }
        public List<Variant> Variants { get; set; }
        public string GifStillImageUrl { get; set; }
        public string GifUrl { get; set; }
    }
}
