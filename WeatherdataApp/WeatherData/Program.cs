using System;
using System.Collections.Generic;
using System.IO;
using WeatherData.Model.EntityModels;
using System.Linq;
using WeatherData.Model.DataAcces;
using WeatherData.Model;
using System.Threading;
using System.Threading.Tasks;


namespace WeatherData.CoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Spinner spinner = new Spinner(/*"Loading weatherdata...",*/ 0, 0);

            spinner.Start();

            DataBaseCheck();
            List<weatherData> allWeatherData = SqlService.LoadFromDataBase();

            spinner.Stop();
            bool isRunning = true;
            while (isRunning)
            {
               isRunning= MainMeny(allWeatherData);

            }
        }

        private static void DataBaseCheck()
        {
            using (var db = new DataContext())
            {

                if (db.Datas.Count() == 0)
                {
                    var m = new Meny();
                    m.Options.Add(" Load To database from CSV-file.");
                    m.Options.Add(" Continue ");
                    int input = m.Run();

                    switch (input)
                    {
                        case 0:

                            SqlService.LoadToDatabase();

                            break;
                        case 1:


                            break;
                    }

                    Console.Clear();
                }
            }
        }

        public static bool MainMeny(List<weatherData> allWeatherData)
        {


            Console.Clear();
            var mainMeny = new Meny();
            mainMeny.Options.Add("     temperature.");//+search by dadte
            mainMeny.Options.Add("     Humidity.");//+MoldIndex
            mainMeny.Options.Add("     Seasons.");
            mainMeny.Options.Add("     Extra.");
            mainMeny.Options.Add("     Exit.");
            int input = mainMeny.Run();
            switch (input)
            {
                case 0:
                    Console.Clear();
                    //Välj sensor
                    string sensorChoice = DisplaySensorOptions(allWeatherData);
                    //Ta fram Medeltemperaturer
                    var resultSet = WeatherCalculations.AvgTempPerDay(sensorChoice, allWeatherData);

                    TempMeny(resultSet, allWeatherData, sensorChoice, "Date\t\tTemperature");

                    //DisplayAvgTempMainmeny(allWeatherData);

                    break;
                case 1:
                    HumidityMeny(allWeatherData);
                    break;
                case 2:
                    SeasonsMeny(allWeatherData);
                    break;
                case 3:
                    ExtraMeny(allWeatherData);

                    break;
                case 4:
                    mainMeny.Go = false;


                    break;

            }
            return mainMeny.Go;
        }

        private static void ExtraMeny(List<weatherData> allWeatherData)
        {

            var ExtraMey = new Meny();

            ExtraMey.Options.Add("Show data for balcony door.");
            ExtraMey.Options.Add("Show temperature differences.");

            int input = ExtraMey.Run();
            switch (input)
            {
                case 0:
                    Console.Clear();

                    OpenDoorClass o =new OpenDoorClass();
                    o.WeatherData = allWeatherData;
                    o.CalculateTime();



                    break;
                case 1:
                    Console.Clear();

                    OpenDoorClass o2 = new OpenDoorClass();
                    o2.WeatherData = allWeatherData;
                    o2.OutsideTempVsInsideTemp();


                    break;

            }
        }

        private static void SeasonsMeny(List<weatherData> allWeatherData)
        {


            string sensorChoice = DisplaySensorOptions(allWeatherData);
            var winters = WeatherCalculations.GetWinterDates(sensorChoice, allWeatherData);
            Console.Clear();

            foreach (var item in winters)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
           


        }

        private static void HumidityMeny(List<weatherData> allWeatherData)
        {
            Console.Clear();

            

            string sensorChoice = DisplaySensorOptions(allWeatherData);


            var humidityMeny = new Meny();
            humidityMeny.Options.Add(      "Show humidity.");
            humidityMeny.Options.Add(      "Show moldindex.");

            int input = humidityMeny.Run();
            switch (input)
            {
                case 0:

                    HumiditySubMeny( sensorChoice, allWeatherData);


                    break;
                case 1:

                    HumiditySubMenyMold(sensorChoice, allWeatherData);


                    break;
                   
            }

        }

        private static void HumiditySubMenyMold(string sensorChoice, List<weatherData> allWeatherData)
        {
            var resultSet = WeatherCalculations.MoldRiskSort(allWeatherData, sensorChoice);

            var m = new Meny();

            m.Options.Add("     Show all data.");
            m.Options.Add("     Select number of rows to display.");
            m.Resultset = resultSet;
            int input = m.Run();
            switch (input)
            {
                case 0:
                    Console.Clear();

                    foreach (var result in resultSet)
                    {
                        Meny.PrintResult(result,3);
                        //Console.WriteLine($"{result.date.ToShortDateString()}\t{result.moldIndex} ");
                    }
                    m.OrderResultset(3);


                    break;
                case 1:

                    Console.Clear();

                    int nbrOfRowsToDisplay = Meny.TrySelectRowsToDisplay(m.Resultset);
                    m.Filtered = true;
                    m.Rows = nbrOfRowsToDisplay;
                    var filteredResultset = Meny.ReduceResultset(resultSet, nbrOfRowsToDisplay);

                    foreach (var result in filteredResultset)
                    {
                        //Console.WriteLine($"Date: {item.data.ToShortDateString()}\t{item.result} ");
                        Meny.PrintResult(result, 3);

                    }
                    m.OrderResultset(3);
                    break;

            }
        }

        private static void HumiditySubMeny(string sensorChoice, List<weatherData>allweatherdata)
        {
            Console.Clear();

            var resultSet = WeatherCalculations.AvgHumidityPerDay(sensorChoice, allweatherdata); ;

            var m = new Meny();

            m.Options.Add("     Show all data.");
            m.Options.Add("     Select number of rows to display.");
            m.Resultset=resultSet;

            int input = m.Run();
            switch (input)
            {
                case 0:
                    foreach (var result in resultSet)
                    {
                        Meny.PrintResult(result, 2);
                    }
                    m.OrderResultset(2);


                    break;
                case 1:

                    Console.Clear();

                    int nbrOfRowsToDisplay = Meny.TrySelectRowsToDisplay(m.Resultset);
                    m.Filtered = true;
                    m.Rows = nbrOfRowsToDisplay;
                    var filteredResultset = Meny.ReduceResultset(resultSet, nbrOfRowsToDisplay);

                    foreach (var result in filteredResultset)
                    {
                        Meny.PrintResult(result, 2);
                    }
                    m.OrderResultset(2);
                    break;

            }
        }


        private static void TempMeny(List<(DateTime data, double temp, int tests)> resultSet, List<weatherData> allWeatherData, string sensorChoice, string menyText)
        {
                var subMenyTemp = new Meny();
            
                subMenyTemp.Resultset = resultSet;

                subMenyTemp.Options.Add("     Show all data.");
                subMenyTemp.Options.Add("     Search by date.");
                subMenyTemp.Options.Add("     Select number of rows to display.");

                int input = subMenyTemp.Run();
                switch (input)
                {

                    case 0:
                        Console.Clear();

                        Console.WriteLine(menyText);
                        foreach (var item in resultSet)
                        {
                            Meny.PrintResult(item, 1);
                        }
                        subMenyTemp.OrderResultset(1);

                        //DisplayOrderListOptionsConsoleKey(resultSet);


                        break;
                case 1:

                    Console.Clear();
                    Console.WriteLine("from: ");

                    DateTime startDate = TryDateInput();

                    Console.WriteLine("To: ");
                    DateTime endDate = TryDateInput();

                    var foundDate =
                    WeatherCalculations.AvarageTemByDate(startDate, endDate, sensorChoice, allWeatherData);
                    Console.WriteLine((foundDate!=0)? $"The avarage temperature is {Math.Round( foundDate,2)} °C. " : "Date or timespan not found.");
                    Console.ReadLine();
                    break;
                case 2:
                        Console.Clear();
                        //hur många rader ska visas?
                        int nbrOfRowsToDisplay = Meny.TrySelectRowsToDisplay(subMenyTemp.Resultset);
                        //bool för att kolla av innan sortering
                        subMenyTemp.Filtered = true;

                        subMenyTemp.Rows = nbrOfRowsToDisplay;
                        //filtrera resultatmängden
                        var filteredResultset = Meny.ReduceResultset(resultSet, nbrOfRowsToDisplay/*, 10*/);

                        foreach (var item in filteredResultset)
                        {
                            Meny.PrintResult(item, 1);
                        }

                        subMenyTemp.OrderResultset(1);

                        break;
                }
            
            
        }

        private static DateTime TryDateInput()
        {
            bool succes = false;
            DateTime d = new DateTime() ;
            while (!succes)
            {
                string input = Console.ReadLine();
                bool valid = DateTime.TryParse(input, out d);

                //bool valid = int.TryParse(input, out totalRows);

                if (valid)
                {
                    succes = true;
                }
                else
                {
                    Console.WriteLine("Please try again..");

                }
            }
            return d;
        }


        private static string DisplaySensorOptions(List<weatherData> allWeatherData)
        {
            var m = new Meny();
            m.Options = GetListOfAviableSensors(allWeatherData);
            int input = m.Run();
            string s = m.Options[input];
            return s;
        }

        private static List<string> GetListOfAviableSensors(List<weatherData> allWeatherData)
        {

            var sensors = new List<string>();
            foreach (var item in allWeatherData)
            {
                sensors.Add($"{item.SensorName}");
            }
            sensors = sensors
               .Distinct()
               .ToList();
            return sensors;
        }
        public static void LoadToDatabaseCheck(List<Sensor>sensors)
        {

            using (var db = new DataContext())
            {
                if (db.Sensors.Count()==0)
                {
                    Console.WriteLine("Loading to database...");

                    int nmrOfAddedRows=SqlService.LoadToDatabase();

                    Console.WriteLine("Done!\n");
                    Console.WriteLine($"Added {nmrOfAddedRows} to database");
                }
            }
        }
    }
    
}
