using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherDatabase.Data;
using Microsoft.EntityFrameworkCore;

namespace WeatherDatabase.DataBaseContext
{
    public class WeatherDatabaseContext : DbContext
    {
        public WeatherDatabaseContext(DbContextOptions<WeatherDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; }

    }
}
