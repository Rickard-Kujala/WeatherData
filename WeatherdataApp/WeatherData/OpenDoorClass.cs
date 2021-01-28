using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Model.EntityModels;

namespace WeatherData.CoreApp
{
    class OpenDoorClass
    {
        private bool IsOpen;
        public List<weatherData> WeatherData;
       
        public List<(DateTime day, double timeOpen)> CalculateTime()
        {
            List<(DateTime date, double timeOpen)> balconyData = new();

            var Total = new List<double>();

            var q=WeatherData
                            .Where(x => x.SensorName == "Inne")
                            .GroupBy(x => x.Date)
                            .OrderBy(x => x.Key);


            int counter = 0;
            int openCounter = 0;


            foreach (var day in q)
            {
                List<TimeSpan> totalTimeDoorWasOpenThisDay = new List<TimeSpan>();

                TimeSpan whenDoorOpened = new TimeSpan();
                TimeSpan whenDoorClosed = new TimeSpan();
                var DaysortedByTime = day.OrderBy(d => d.Time).ToList();
                foreach (var measure in DaysortedByTime)
                {

                    //Om temperaturen sjunker..Om föregående mätnng har högre temperatur än den nuvarande i loopen
                    if (counter > 0 && counter < DaysortedByTime.Count() && DaysortedByTime[counter - 1].Temp > measure.Temp)
                    {
                        IsOpen = true;

                        whenDoorOpened = TimeSpan.Parse(measure.Time);

                        TimeSpan tS = TimeSpan.Parse(measure.Time);

                        openCounter++;

                    }
                    //Om temperaturen slutar sjunka
                    if (counter > 0 && counter < DaysortedByTime.Count() && DaysortedByTime[counter - 1].Temp <= measure.Temp && IsOpen)
                    {
                        whenDoorClosed = TimeSpan.Parse(measure.Time);
                        TimeSpan totalTimeDoorWasOpenThisMeasurement = whenDoorClosed.Subtract(whenDoorOpened);

                        totalTimeDoorWasOpenThisDay.Add(totalTimeDoorWasOpenThisMeasurement);
                        openCounter = 0;

                        IsOpen = false;

                    }
                    else
                    {

                    }

                }

                var Sum = totalTimeDoorWasOpenThisDay.Sum(x => x.TotalMinutes);


                balconyData.Add((day.Key.Date, Sum));
                Total.Add(Sum);

                counter++;

            }

            PrintBalconyDoorResult(balconyData);

            return balconyData;
        }
        private void PrintBalconyDoorResult(List<(DateTime day, double timeOpen)>result)
        {
            var q = result.OrderByDescending(x=>x.timeOpen);
            foreach (var day in q)
            {
                Console.WriteLine($"{day.day.ToShortDateString()} Door was open {Math.Round( day.timeOpen,0)} minutes.");
            }
            Console.ReadLine();
        }
        //Sortering på då inne-och yttertemperaturen skiljt sig mest och minst.
        public void OutsideTempVsInsideTemp()
        {
            List<(DateTime date, double timeOpen)> result = new();

            var uteTemp = WeatherCalculations.AvgTempPerDay("Ute",WeatherData);
            var inneTemp = WeatherCalculations.AvgTempPerDay("Inne",WeatherData);

            int counter = 0;
            foreach (var day in uteTemp)
            {
                double diff = inneTemp[counter].temp -(day.temp);
                if (diff < 0)//för att inte få en negativ diff
                {
                    diff = diff * (-1);
                }
                result.Add((day.data, diff));
                counter++;
            }
            PrintOutsideVsInsideTemp(result);
            Console.ReadLine();
        
        }
        private void PrintOutsideVsInsideTemp(List<(DateTime day, double result)> result)
        {
            var q = result.OrderBy(x=>x.result);
            foreach (var day in result)
            {
                Console.WriteLine($"{day.day.ToShortDateString()}   Difference: {Math.Round(day.result, 2)} °C");
            }
        }


    }

}
