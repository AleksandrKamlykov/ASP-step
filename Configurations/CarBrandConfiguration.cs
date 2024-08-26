using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class CarBrandConfiguration
    {
        public void Configure(EntityTypeBuilder<CarBrand> builder)
        {
       builder.HasMany(b => b.Cars)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(Moc.brands);
        }
    }
}
