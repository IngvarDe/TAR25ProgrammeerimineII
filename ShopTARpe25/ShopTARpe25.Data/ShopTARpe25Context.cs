using Microsoft.EntityFrameworkCore;
using ShopTARpe25.Core.Domain;


namespace ShopTARpe25.Data
{
    //teha sellest classist DbContext, et saaks andmebaasi kasutada
    public class ShopTARpe25Context : DbContext
    {
        public ShopTARpe25Context(DbContextOptions<ShopTARpe25Context> options)
            : base(options)
        {
        }

        public DbSet<Spaceship> Spaceships { get; set; }
    }
}
