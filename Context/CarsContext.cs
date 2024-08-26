using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class CarsContext: DbContext
    {



        private readonly IConfiguration _configuration;

        public CarsContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }


        public CarsContext(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5, // Number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Delay between retries
                    errorNumbersToAdd: null // List of additional error numbers to consider transient
                );
            });

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }

    }
}
