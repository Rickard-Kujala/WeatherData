using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WeatherData.Model.EntityModels
{
    public class weatherData
    {
        public int Id { get; set; }
        public string SensorName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }

        

    }
}
