using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class Moc
    {
        public static List<Car> cars = new List<Car>();



        public static List<CarBrand> brands = new List<CarBrand>() { 
        new CarBrand { Id = Guid.NewGuid(), Name = "Toyota" },
        new CarBrand { Id = Guid.NewGuid(), Name = "Ford" },
        new CarBrand { Id = Guid.NewGuid(), Name = "Chevrolet" },
        new CarBrand { Id = Guid.NewGuid(), Name = "BMW" },
        new CarBrand { Id = Guid.NewGuid(), Name = "Honda" },
        new CarBrand { Id = Guid.NewGuid(), Name = "Volkswagen" },
        };
    }
}
