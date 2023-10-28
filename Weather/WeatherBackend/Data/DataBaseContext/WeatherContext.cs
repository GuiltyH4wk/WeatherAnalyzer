using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Weather.Data.DataBaseContext
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<Data.Weather> Weather { get; set; } = default!;
    }
}
