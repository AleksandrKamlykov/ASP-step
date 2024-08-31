using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class Moc
    {
        
        public static List<Car> _cars = new List<Car>()
        {
            new Car { Id = Guid.NewGuid(), Brand = "Toyota", Model = "Camry", Year = 2020, Price = 25000 },
            new Car { Id = Guid.NewGuid(), Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000 },
            new Car { Id = Guid.NewGuid(), Brand = "Ford", Model = "Focus", Year = 2020, Price = 22000 },
            new Car { Id = Guid.NewGuid(), Brand = "Ford", Model = "Fusion", Year = 2020, Price = 25000 },
            new Car { Id = Guid.NewGuid(), Brand = "Chevrolet", Model = "Cruze", Year = 2020, Price = 23000 },
            new Car { Id = Guid.NewGuid(), Brand = "Chevrolet", Model = "Malibu", Year = 2020, Price = 28000 },
            new Car { Id = Guid.NewGuid(), Brand = "BMW", Model = "X5", Year = 2020, Price = 60000 },
            new Car { Id = Guid.NewGuid(), Brand = "BMW", Model = "X6", Year = 2020, Price = 65000 },
            new Car { Id = Guid.NewGuid(), Brand = "Honda", Model = "Accord", Year = 2020, Price = 30000 },
            new Car { Id = Guid.NewGuid(), Brand = "Honda", Model = "Civic", Year = 2020, Price = 25000 },
            new Car { Id = Guid.NewGuid(), Brand = "Volkswagen", Model = "Golf", Year = 2020, Price = 22000 },
            new Car { Id = Guid.NewGuid(), Brand = "Volkswagen", Model = "Jetta", Year = 2020, Price = 25000 }
        };
        
    }
}
