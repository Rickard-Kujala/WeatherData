using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherData.Model.EntityModels
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public ICollection<weatherData> WeatherDatas { get; set; }


    }
}
