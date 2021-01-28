using System;
using System.Collections.Generic;
using System.Linq;
using WeatherData.Model.EntityModels;

namespace WeatherData.CoreApp
{
    class WeatherCalculations
    {
        public static List<(DateTime data, double temp, int tests)> AvgTempPerDay(string sensorName, List<weatherData> weatherData)
        {

            var grouping = weatherData
            .Where(g => g.SensorName.ToLower() == sensorName.ToLower())
            .GroupBy(g => g.Date)
            .Select(g => new
            {
                average = g.Average(g => g.Temp),
                date = g.Key,
                measures = g.Count()

            }).OrderByDescending(g => g.average).ToList();


            var list = new List<(DateTime, double, int)>();

            foreach (var item in grouping)
            {
                list.Add((item.date, Math.Round(item.average, 2), item.measures));
            }


            return list;

        }
        public static List<(DateTime date, double result, int tests)> AvgHumidityPerDay(string sensorName, List<weatherData> weatherData)
        {
            var anus = weatherData
                     .Where(x => x.SensorName == sensorName)
                     .GroupBy(x => x.Date)
                     .Select(x => new
                     {
                         Date = x.Key,
                         AvgHumidity = x.Average(x => x.Humidity),
                         tests = x.Count()
                     }
                     )
                     .OrderByDescending(x => x.AvgHumidity);

            List<(DateTime date, double temp, int tests)> list = new();

            foreach (var böld in anus)
            {
                list.Add((böld.Date,Math.Round( böld.AvgHumidity, 2), böld.tests));
            }

            return list;

        }
        public static double AvarageTemByDate(DateTime startDate, DateTime? endDate, string sensorName, List<weatherData> weatherData)
        {
            double d=0;
            try
            {
                d = weatherData
                    .Where(x => x.SensorName == sensorName
                     && x.Date >= startDate
                     && x.Date <= endDate)
                    .Average(x => x.Temp);

            }
            catch (Exception)
            {
                //Something
            }
                return d;


        }
        public static List<(DateTime date, double moldIndex, int tests)> MoldRiskSort(List<weatherData> weatherData, string sensorName)
        {
            var list = weatherData
                .Where(g => g.SensorName.ToLower() == sensorName.ToLower())
                .GroupBy(l => l.Date)
                .Select(l => new
                {
                    humidityAverage = l.Average(l => l.Humidity),
                    tempAverage = l.Average(l => l.Temp),
                    date = l.Key
                });
           
            var result = new List<(DateTime date, double moldIndex, int tests)>();

            foreach (var item in list)
            {
                if (item.tempAverage <0 || item.humidityAverage <78)
                {
                    result.Add((item.date, 0, 0 ));
                }
                else
                {
                    double moldIndex =Math.Round (((item.humidityAverage - 78) * (item.tempAverage / 15)),2) / 0.22;
                    result.Add((item.date,Math.Round( moldIndex, 2), 0));

                }
            }
            result = result.OrderBy(x=>x.moldIndex).ToList();
            return result;

        }
        public static List<string> GetWinterDates(string sensorName, List<weatherData> weatherData)
        {
            //DateTime start = new(year, 08, 01);
            //DateTime stop = new(year + 1, 02, 15);
            List<DateTime> winterDates = new List<DateTime>();
            List<DateTime> fallDates = new List<DateTime>();

            var winterDatesstring = new List<string>();
            foreach (var year in GetYears(weatherData))
            {
                string winterstring;
                string fallString;
                string summerString;
                string springString;

                List<(DateTime date, double avgTempPerDay)> fallPeriod =
                GetPeriod(sensorName, weatherData, new DateTime(year, 08, 01), new DateTime(year + 1, 02, 15));
                //fallDates.Add(GetSeasonDates(fallPeriod, 10,5));


                List<(DateTime date, double avgTempPerDay)> winterPeriod =
                GetPeriod(sensorName, weatherData, new DateTime(year, 08, 01), new DateTime(year + 1, 02, 15));
                //winterDates.Add(GetSeasonDates(winterPeriod, 0,5));

                List<(DateTime date, double avgTempPerDay)> springPeriod =
                     GetPeriod(sensorName, weatherData, new DateTime(year,02,15), new DateTime(year,07,31));


                winterstring = GetSeasonDates(winterPeriod, 0, null, 5) == new DateTime(0001, 01, 01) ? "Winter: No winter ocurred according to existing data." : $"Winter: {GetSeasonDates(winterPeriod, 0,null,5).ToShortDateString()}";
                fallString=GetSeasonDates(fallPeriod, 10,-100, 5)== new DateTime(0001,01,01)? "Fall  : No fall ocurred according to existing data." : $"Fall: {GetSeasonDates(fallPeriod,10,-100,5).ToShortDateString()}";
                springString = GetSeasonDates(springPeriod,50,0, 7)==new DateTime(0001,01,01)? "Spring: No spring ocurred according to existing data" : $"Spring: {GetSeasonDates(springPeriod, 50,0 ,7).ToShortDateString()}";
                summerString = GetSeasonDates(springPeriod, 100, 10, 5) == new DateTime(0001, 01, 01) ? "Summer: No spring ocurred according to existing data" : $"Summer: {GetSeasonDates(springPeriod, 100, 10, 5).ToShortDateString()}";

                winterDatesstring.Add($"{year}\n\t{winterstring}\n" +
                                            $"\n\t{fallString}\n"+
                                            $"\n\t{springString}\n"+
                                            $"\n\t{summerString}\n");
                       


                //result = a > b ? "a is greater than b" : a < b ? "b is greater than a" : "a is equal to b";
            }
            return winterDatesstring; 
        }
        public static List<DateTime> GetFallDates(string sensorName, List<weatherData> weatherData)
        {
            
            List<DateTime> fallDates = new List<DateTime>();
            foreach (var year in GetYears(weatherData))
            {
                List<(DateTime date, double avgTempPerDay)> fallPeriod =
                GetPeriod(sensorName, weatherData, new DateTime(year, 08, 01), new DateTime(year + 1, 02, 15));
                fallDates.Add(GetSeasonDates(fallPeriod, 10,null, 5));
            }
            return fallDates;
        }
        private static DateTime GetSeasonDates(List<(DateTime date, double avgTempPerDay)> winterPeriods, int? maxTemp, int? mintemp, int dayLimit)
        {

            int winterCounter = 0;

            int rowCounter = 0;

            int glappCounter = 0;

            DateTime winterDate = new DateTime();
            foreach (var day in winterPeriods)
            {
                if (day.avgTempPerDay <= maxTemp && day.avgTempPerDay >= mintemp) 
                {
                    winterCounter++;
                    //day ska vara en dag mer än "grouping[rowCounter-1].Date" 

                    if (rowCounter > 0 && winterPeriods[rowCounter - 1].date.AddDays(+1) == day.date)
                    {
                        //här hamnar vi om det inte finns något glapp
                        if (winterCounter == dayLimit)
                        {
                            winterDate= day.date.AddDays(-(dayLimit-1)/*-4*/);

                            //Console.WriteLine(winterDate.ToShortDateString());

                            break;
                        }
                    }
                    else if (rowCounter == 0)
                    {

                    }
                    else
                    {
                        winterCounter = 0;

                        glappCounter++;
                        //=Finns lucka!!! 
                    }
                }
                else
                {
                    winterCounter = 0;
                }
                rowCounter++;

            }
            return winterDate ;
        }
        private static List<(DateTime date, double avgTemp)> GetPeriod(string sensorName, List<weatherData> weatherData, DateTime start, DateTime stop)
        {
            List<(DateTime date, double avgTemp)> winterPeriod = new();
            //DateTime start = new(year, 08, 01);
            //DateTime stop = new(year + 1, 02, 15);

            var grouping = weatherData
           .Where(s => s.SensorName.ToLower() == sensorName.ToLower())
           .GroupBy(g => g.Date.Date)
           .OrderBy(g => g.Key)
           .Where(g => g.Key >= start/*new DateTime(year, 08, 01)*/ && g.Key <= stop/*new DateTime(year + 1, 02, 15)*/)
            //.Where(g => g.Key.Month >= 08 &&  g.Key.Day >= 01 && g.Key <= new DateTime(g.Key.Year+1, 02,15))
            .Select(x => new
            {
                Date = x.Key,
                AvgTempPerDay = x.Average(x => x.Temp),
            }
                );
            foreach (var item in grouping)
            {
                winterPeriod.Add((item.Date, item.AvgTempPerDay));

            }
            
                return winterPeriod;

        }
        public static List<int> GetYears(List<weatherData>weatherData)
        {
            var years = weatherData
                .AsEnumerable()
                .GroupBy(g => g.Date.Date.Year)
                .Select(g=>g.Key).ToList()
                ;

            return years;
        }
     
        
    }
    

}
