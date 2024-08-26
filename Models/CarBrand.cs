namespace WebApplication1.Models
{
    public class CarBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
