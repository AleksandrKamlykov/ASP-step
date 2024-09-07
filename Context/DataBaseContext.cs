using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class DataBaseContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public DataBaseContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        public DataBaseContext(IConfiguration configuration)
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
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
