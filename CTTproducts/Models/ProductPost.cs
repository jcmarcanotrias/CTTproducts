namespace CTTproducts.Models
{
    public class ProductPost
    {
        public string Description { get; set; }

        public Category[] Categories { get; set; }

        public float Price { get; set; }
    }
}