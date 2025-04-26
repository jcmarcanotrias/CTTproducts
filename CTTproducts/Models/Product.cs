namespace CTTproducts.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public Category[] Categories { get; set; }
        public float Price { get; set; }
    }
}