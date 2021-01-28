using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherData.Model.DataAcces;
using WeatherData.Model.EntityModels;

namespace WeatherData.Model
{
    public class SqlService
    {
        public static List<weatherData> LoadFromDataBase()
        {
            List<weatherData> weatherData = new List<weatherData>();

            using (var db = new DataContext())
            {
                weatherData = db.Datas.ToList();
            }
                return weatherData;
        }
        public static List<(DateTime winter, DateTime fall)> Seasons(string sensorName)
        {
            using (var db = new DataContext())
            {
                var grouping = db.Datas
                    .Where(s => s.SensorName == sensorName)
                    .GroupBy(x => x.Date)
                    .Select(x => new
                    {
                        Date = x.Key,
                        AvgTemp = x.Average(x => x.Temp),
                        tests = x.Count()
                    }
                    )
                    .OrderByDescending(x => x.Date);


                List<(DateTime date, double avgTemp, int tests)> seasons = new();

                foreach (var böld in grouping)
                {
                    seasons.Add((böld.Date, böld.AvgTemp, böld.tests));
                }

                List <(DateTime winter, DateTime fall )> kuken = new();

                int winterCounter = 0;
                int fallCounter = 0;
                int rowCounter = 0;

                DateTime winterDate = new DateTime();
                DateTime fallDate = new DateTime();

                foreach (var row in seasons)
                {
                    if (row.avgTemp <= 10)
                    {
                        winterCounter++;

                    }
                    else
                    {
                        winterCounter = 0;
                    }
                    if (winterCounter == 5)
                    {
                        Console.WriteLine("vinter!!!!!!");
                        //winterDate = seasons[rowCounter-4].date;
                        //kuken.Add(winterDate);
                    }
                    if (true)
                    {

                    }
                    rowCounter++;
                }

                return kuken;
            }
            
        }
        public static List<(DateTime date, double temp, int tests)> SortedTempByday(string sensorName)
        {
            using (var db = new DataContext())
            {
                var grouping = db.Datas
                    .Where(s=>s.SensorName==sensorName)
                    .GroupBy(x => x.Date)
                    .Select(x => new 
                    { 
                        Date = x.Key,
                        AvgTemp = x.Average(x => x.Temp),
                        tests = x.Count()
                    }
                    )
                    .OrderByDescending(x => x.AvgTemp);

                List<(DateTime date, double avgTemp, int tests)> list = new();

                foreach (var item in grouping)
                {
                    list.Add((item.Date,Math.Round( item.AvgTemp, 2), item.tests));
                }

                return list;

            }

        }
        public static List<(DateTime date, double humidity, int tests)> SortedHumidity(string sensorName)
        {
            using (var db = new DataContext())
            {
                var anus = db.Datas
                    .Where(x=>x.SensorName==sensorName)
                    .GroupBy(x => x.Date)
                    .Select(x => new
                    {
                        Date = x.Key,
                        AvgHumidity = x.Average(x => x.Temp),
                        tests = x.Count()
                    }
                    )
                    .OrderByDescending(x => x.AvgHumidity);

                List<(DateTime date, double temp, int tests)>list= new();

                foreach (var böld in anus)
                {
                    list.Add((böld.Date,Math.Round( böld.AvgHumidity, 2), böld.tests));
                }

                return list;

            }

        }

        public static double AvarageTemByDate(DateTime startDate, DateTime? endDate, string sensorName)
        {
            using (var db = new DataContext())
            {
                //var sensor = db.Sensors.Include(s => s.WeatherDatas).First();
                //var datas = sensor.WeatherDatas.First();

                List<double> list = new List<double>();

                //.Select(x => new { Date = x.Key, AvgTemp = x.Average(x => x.Temp) })

                var data = db.Datas
                        .Where(x => x.SensorName == sensorName 
                         && x.Date >= startDate
                         && x.Date <= endDate)
                        .Average(x => x.Temp);
               
                return data;
            }
            
        }
       
        public static int LoadToDatabase(/*List<Sensor> sensors*/)
        {
            var data = FileService.LoadFromCSVFile("ballaballa.CSV");
            var weatherData = FileService.GetWeatherData(data);
            var sensors = FileService.GetSensor(data);

            var listOfSensorsWithWeatherData= AddWeatherDataToSensors(weatherData, sensors);

            using (var db = new DataContext())
            {
                Console.WriteLine("Storing data to database....");

                db.AddRange(listOfSensorsWithWeatherData);

                int i = db.SaveChanges();

                return i;

            }
        }

        private static List<Sensor> AddWeatherDataToSensors(List<weatherData> weatherData, List<Sensor> sensors)
        {
            //Initiate sensor-List
            foreach (var sensor in sensors)
            {
                sensor.WeatherDatas = new List<weatherData>();
            }
            //Match data to sensor
            foreach (var item in weatherData)
            {
                foreach (var sensor in sensors)
                {
                    if (item.SensorName == sensor.SensorName)
                    {
                        sensor.WeatherDatas.Add(item);
                    }

                }
            }
            return sensors;
        }
    }


}

