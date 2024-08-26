namespace WebApplication1.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public Guid BrandId { get; set; }
        public CarBrand Brand { get; set; }
        public int Year { get; set; }
        override public string ToString()
        {
            return $"Model: {Model}, Brand: {Brand.Name}, Year: {Year}";
        }
    }
}
