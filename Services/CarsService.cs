using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CarsService
    {
        private CarsContext _carsContext;

        public CarsService(CarsContext _context)
        {
            _carsContext = _context;
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _carsContext.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(Guid id)
        {
            return await _carsContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> AddCar(Car car)
        {
            Console.WriteLine(car);
           await _carsContext.Cars.AddAsync(car);
           await _carsContext.SaveChangesAsync();
           return car;
        }

        public async Task<Guid> UpdateCar(Car car)
        {
            
            var carDb = await _carsContext.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
            if (carDb == null)
            {
                return Guid.Empty;
            }
            carDb.Brand = car.Brand;
            carDb.Model = car.Model;
            carDb.Year = car.Year;
            carDb.Price = car.Price;
            
            _carsContext.Cars.Update(carDb);
            await _carsContext.SaveChangesAsync();

            return car.Id;
        }

        public async Task DeleteCar(Guid id)
        {
            var car = await _carsContext.Cars.FirstOrDefaultAsync(c=>c.Id == id);
            if (car != null)
            {
                _carsContext.Cars.Remove(car);
                await _carsContext.SaveChangesAsync();
            }
        }
        
    }
}