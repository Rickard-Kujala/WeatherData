using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.CoreApp
{
    class Meny
    {
        public bool Go;
        public bool Filtered;
        public int Rows;
        public List<string> Options;
        public List<(DateTime data, double result, int tests)>Resultset;
        public Meny()
        {
            Rows = 0;
            Filtered = false;
            Resultset = new List<(DateTime data, double temp, int tests)>();
            Go = true;
            Options = new List<string>();
        }
        public int Run()
        {

            int scroll = 0;

            bool done = false;

            while (!done)
            {
                Console.Clear();

                int counter = 0;

                foreach (var item in Options)
                {

                    if (counter == scroll)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"==> {item}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {item}");
                    }
                    counter++;
                }
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Z && scroll == Options.Count - 1)
                {
                    scroll = Options.Count - 1;

                }
                else if (key == ConsoleKey.A && scroll == 0)
                {
                    scroll = 0;

                }

                else if (key == ConsoleKey.Z)
                {
                    scroll++;

                }
                else if (key == ConsoleKey.A)
                {
                    scroll--;

                }
                else if (key == ConsoleKey.Enter)
                {
                    done = true;
                }

            }

            return scroll;
        }
        public void OrderResultset(int resultData)
        {
            while (Go==true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                //Console.WriteLine(menytext);
                switch (key)
                {
                    case ConsoleKey.A:
                        Console.Clear();
                        if (Filtered)
                        {
                            var q = Resultset.OrderBy(r => r.result).ToList();
                            var q2=ReduceResultset(q, Rows);
                            foreach (var item in q2)
                            {
                                PrintResult(item, resultData);
                                //Console.WriteLine($"Date: {item.data.ToShortDateString()}\t{item.result} ");
                            }
                        }
                        else
                        {
                            var q = Resultset.OrderBy(r => r.result).ToList();
                            foreach (var item in q)
                            {
                                PrintResult(item, resultData);
                                //Console.WriteLine($"Date: {item.data.ToShortDateString()}\t{item.result} ");
                            }
                        }
                        break;
                    case ConsoleKey.D:
                        Console.Clear();
                        if (Filtered)
                        {
                            var q = Resultset.OrderByDescending(r => r.result).ToList();
                            var q2=ReduceResultset(q, Rows);
                            foreach (var item in q2)
                            {
                                PrintResult(item, resultData);

                                //Console.WriteLine($"Date: {item.data.ToShortDateString()}\t{item.result} ");
                            }
                        }
                        else
                        {
                            var q = Resultset.OrderByDescending(r => r.result).ToList();
                            foreach (var item in q)
                            {
                                PrintResult(item, resultData);

                                //Console.WriteLine($"Date: {item.data.ToShortDateString()}\t{item.result} ");
                            }
                        }

                        break;
                    case ConsoleKey.Escape:
                        Go = false;
                        break;
                }
            }
           

        }
        public static List<(DateTime data, double result, int tests)> ReduceResultset(List<(DateTime date, double result, int tests)> resultSet, int totalRows)
        {
            var filteredResultset = new List<(DateTime data, double result, int tests)>();

            for (int i = 0; i < totalRows+1; i++)
            {
                filteredResultset.Add((resultSet[i].date, resultSet[i].result, resultSet[i].tests));
            }
            return filteredResultset;




        }

        public static int TrySelectRowsToDisplay(List<(DateTime data, double temp, int tests)> resultSet)
        {
            Console.WriteLine("Select number of rows to display:  ");
            bool succes = false;
            int totalRows=0;

            while (!succes)
            {
                string input = Console.ReadLine();

                bool valid = int.TryParse(input, out totalRows);

                if (valid && totalRows <= resultSet.Count)
                {

                    Console.Clear();
                    succes = true;
                }
                else
                {
                    Console.WriteLine("Please try again..");

                }
            }
            return totalRows;

        }
        public static void PrintResult((DateTime date, double result, int tests) result, int resultData)
        {
            switch (resultData)
            {
                case 1://Print avgTemp

                    Console.WriteLine($"Date: {result.date.ToShortDateString()}\tAvg.temp: {result.result} °C");
                    break;
                case 2://Print humidity

                    Console.WriteLine($"Date: {result.date.ToShortDateString()}\tHumidity: {result.result} %");
                    break;
                case 3://MoldIndex

                    Console.WriteLine($"Date: {result.date.ToShortDateString()}\tMoldindex: {result.result} %");
                    break;

            }
        }
    }
}



