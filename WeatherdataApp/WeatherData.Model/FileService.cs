using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Model.EntityModels;

namespace WeatherData.Model
{
    public class FileService
    {
        public static List<weatherData> GetWeatherData(string[] data)
        {
            List<weatherData> weatherInfo = new List<weatherData>();
            foreach (var row in data)
            {
                var weatherdata = new weatherData();

                weatherdata.Date = GetDate(row);
                weatherdata.Time = GetTime(row);
                weatherdata.SensorName = GetSensorName(row);
                weatherdata.Temp = GetTemperature(row);
                weatherdata.Humidity = GetHumidity(row);
                weatherInfo.Add(weatherdata);

            }

            return weatherInfo;
        }

        private static int GetHumidity(string row)
        {
            int i = 0;


            string[] split = row.Split(' ', ',', ',', ',');
            i = int.Parse(split[4]);

            return i;
        }
        static string GetSensorName(string row)
        {

            string s = "";

            string[] split = row.Split(' ', ',', ',', ',');
            s = split[2];

            return s;
        }

        public static List<Sensor> GetSensor(string[] data)
        {
            List<Sensor> sensors = new List<Sensor>();
            List<string> myList = new List<string>();
            foreach (var row in data)
            {
                string[] split = row.Split(' ', ',', ',', ',');
                myList.Add(split[2]);
            }
            myList = myList
                .Distinct()
                .ToList();

            foreach (var sensorname in myList)
            {
                var sensor = new Sensor();
                sensor.SensorName = sensorname;
                sensors.Add(sensor);
            }

            return sensors;
        }
        public static double GetTemperature(string row)
        {
            double d = 0;
            string[] split = row.Split(' ', ',', ',', ',');
            string s = split[3];
            d = double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);

            return d;
        }

        public static string GetTime(string row)
        {
            string s = "";

            string[] split = row.Split(' ', ',', ',', ',');
            s = split[1];

            return s;
        }

        public static DateTime GetDate(string row)
        {
            var date = new DateTime();

            string[] split = row.Split(" ");
            date = DateTime.Parse(split[0]);

            return date;
        }

        public static string[] LoadFromCSVFile(string CsvfileName)
        {
            //"TemperaturData.csv"
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + CsvfileName);
            string[] data = File.ReadAllLines(path);

            return data;


        }

    }
}
