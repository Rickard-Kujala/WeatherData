using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
 


namespace WeatherData.Model.DataAcces
{
    public class DataContext : DbContext
    {

        private string connectionString; 

        public DataContext()
            : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            connectionString = configuration.GetConnectionString("sqlConnection");
        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<EntityModels.Sensor> Sensors { get; set; }
        public DbSet<EntityModels.weatherData> Datas { get; set; }




    }
}
