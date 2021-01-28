using System;
using System.Threading;
using System.Drawing;

namespace WeatherData.CoreApp
{
    class Spinner
    {
        private int counter;
        private bool Active;
        private readonly Thread Thread;
        private readonly string LoadingText;
        private int Left;
        private int Top;
        public Spinner(/*string loadingText,*/ int left, int top)
        {
            //LoadingText = loadingText;
            counter = 0;
            Thread = new Thread(Cloud);
            Left = left;
            Top = top;
        }
        public void Start()
        {
            Console.WriteLine(LoadingText);
            Console.SetCursorPosition(Console.CursorLeft = Left, Console.CursorTop = Top);

            Active = true;
            if (!Thread.IsAlive)
            {
                Thread.Start();
            }
        }
        public void Stop()
        {
            Active = false;
        }
        public void Spin()
        {
            Console.CursorVisible = false;

            while (Active)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

            }

        }
        public void Bounce()
        {
            Console.CursorVisible = false;
            while (Active)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("(*--------)"); break;
                    case 1: Console.Write("(----*----)"); break;
                    case 2: Console.Write("(--------*)"); break;
                    case 3: Console.Write("(---*-----)"); break;
                    case 4: Console.Write("(*--------)"); break;

                }
                Console.SetCursorPosition(Console.CursorLeft - 11, Console.CursorTop);

                Thread.Sleep(300);

            }
            
        }
        public void progress()
        {
            Console.CursorVisible = false;
            while (Active)
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);

                //Console.Write("(           )");

                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("\r          "); break;
                    case 1: Console.Write("\r==>       "); Console.ForegroundColor = ConsoleColor.Red; ; break;
                    case 2: Console.Write("\r=====>    "); Console.ForegroundColor = ConsoleColor.Yellow; ; break;
                    case 3: Console.Write("\r========> "); Console.ForegroundColor = ConsoleColor.Green; ; break;
                }
                Thread.Sleep(200);

            }
            Console.ResetColor();

        }
        public void Loading()
        {
            Console.CursorVisible = false;
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("Loading   "); break;
                case 1: Console.Write("Loading.  "); break;
                case 2: Console.Write("Loading.. "); break;
                case 3: Console.Write("Loading..."); break;

            }
            Thread.Sleep(200);

            Console.SetCursorPosition(Console.CursorLeft - 10, Console.CursorTop);
        }
        public void Thermometer()
        {
            Console.WriteLine("Loading weatherdata...");

            Console.CursorVisible = false;
            while (Active)
            {
                counter++;
                Console.Clear();
                //Console.WriteLine("Loading weatherdata...");

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop+10);
                


                switch (counter % 4)
                {

                    case 0:

                        Console.WriteLine("  ║  ║ - 40  ");
                        Console.WriteLine("  ║  ║ -     ");
                        Console.WriteLine("  ║  ║ - 30  ");
                        Console.WriteLine("  ║  ║ -     ");
                        Console.WriteLine("  ║  ║ - 20  ");
                        Console.WriteLine("  ║  ║ -     ");
                        Console.WriteLine("  ║  ║ - 0   ");
                        Console.WriteLine("  ║[]║ -     ");
                        Console.WriteLine("  ║[]║ - 10  ");
                        Console.WriteLine(" ║    ║      ");
                        Console.WriteLine("║      ║     ");
                        Console.WriteLine(" ║    ║      ");
                        Console.WriteLine("  ║__║       ");


                        break;
                    case 1:
                        {

                            Console.WriteLine("  ║  ║ - 40  ");
                            Console.WriteLine("  ║  ║ -     ");
                            Console.WriteLine("  ║  ║ - 30  ");
                            Console.WriteLine("  ║  ║       ");
                            Console.WriteLine("  ║  ║ - 20  ");
                            Console.WriteLine("  ║[]║ -     ");
                            Console.WriteLine("  ║[]║ - 0   ");
                            Console.WriteLine("  ║[]║ -     ");
                            Console.WriteLine("  ║[]║ - 10  ");
                            Console.WriteLine(" ║    ║      ");
                            Console.WriteLine("║      ║     ");
                            Console.WriteLine(" ║    ║      ");
                            Console.WriteLine("  ║__║       ");
                            break;

                        }
                    case 2:
                        {

                            Console.WriteLine("  ║  ║ - 40 ");
                            Console.WriteLine("  ║  ║ -    ");
                            Console.WriteLine("  ║[]║ - 30 ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ - 20 ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ - 0  ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ - 10 ");
                            Console.WriteLine(" ║    ║     ");
                            Console.WriteLine("║      ║    ");
                            Console.WriteLine(" ║    ║     ");
                            Console.WriteLine("  ║_ ║      ");
                            break;

                        }
                    case 3:
                        {

                            Console.WriteLine("  ║[]║ - 40 ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ - 30 ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ - 20 ");
                            Console.WriteLine("  ║[]║ -    ");
                            Console.WriteLine("  ║[]║ --0  ");
                            Console.WriteLine("  ║[]║-    ");
                            Console.WriteLine("  ║[]║ - 10 ");
                            Console.WriteLine(" ║    ║     ");
                            Console.WriteLine("║      ║    ");
                            Console.WriteLine(" ║    ║     ");
                            Console.WriteLine("  ║__║      ");
                            break;

                        }
                }
                //Console.SetCursorPosition(Console.CursorLeft - 11, Console.CursorTop);
                Thread.Sleep(100);

            }

        }
        public void Cloud()
        {

            Console.CursorVisible = false;
            while (Active)
            {
                counter++;
                Console.Clear();
                 Console.WriteLine("Loading weatherdata...");

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);



                switch (counter % 4)
                {

                    case 0:
                        Console.WriteLine("          ,~'``¨~.           "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("        (   ,      )_        "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("       _(  (         )_      "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("     (          _)     )     "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("   _(     ,           )      "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("  (      (       )   )       "); Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("   `_'~~=~~-~~=~~-~~         "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r   ,  ,  , ,,  ,,   "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\r   ,  ,  , ,,  ,,   "); Console.ForegroundColor = ConsoleColor.Blue;
                        Console.ResetColor();

                        break;
                    case 1:
                        {
                            Console.WriteLine("          ,~'``¨~.           "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("        (   ,      )_        "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("       _(  (         )_      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("     (          _)     )     "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   _(     ,           )      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("  (      (       )   )       "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   `_'~~=~~-~~=~~-~~         "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  ,,   "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.ResetColor();

                            break;

                        }
                    case 2:
                        {
                            Console.WriteLine("          ,~'``¨~.           "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("        (   ,      )_        "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("       _(  (         )_      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("     (          _)     )     "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   _(     ,           )      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("  (      (       )   )       "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   `_'~~=~~-~~=~~-~~         "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  ,,   "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.ResetColor();

                            break;

                        }
                    case 3:
                        {
                            Console.WriteLine("          ,~'``¨~.           "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("        (   ,      )_        "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("       _(  (         )_      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("     (          _)     )     "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   _(     ,           )      "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("  (      (       )   )       "); Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("   `_'~~=~~-~~=~~-~~         "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r  ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r  ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r  ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,, , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r    ,  ,  , ,,  , , "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r     ,  ,  , ,,  , ,"); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\r   ,  ,  , ,,  ,,   "); Console.ForegroundColor = ConsoleColor.Blue;
                            Console.ResetColor();

                            break;

                        }
                }
                Thread.Sleep(100);

            }

        }
    }
    

}
