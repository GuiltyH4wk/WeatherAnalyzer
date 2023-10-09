using Microsoft.EntityFrameworkCore;

namespace Weather.Data.DataBaseContext
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<Model.Weather> Weather { get; set; } = default!;
    }
}
