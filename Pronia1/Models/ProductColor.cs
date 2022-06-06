namespace Pronia1.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public Product product { get; set; }
        public Color color { get; set; }
    }
}
