using System;

namespace PiExplore
{
    class Program
    {
        static void Main(string[] args)
        {
            PiLoad load = new PiLoad();
            load.LoadIt();
            load.watch0.Reset();
            load.watch0.Start();
            for (int ll = 1; ll < 200000; ll++)
            {
                if ((ll) % 100 == 0)
                {
                    Console.WriteLine("STARTVAL " + ll);
                    
                }
                load.StartUpLoop(ll);
            }
            load.PrintInteresting();
            load.watch0.Stop();
            Console.WriteLine("TIMETAKEN: " + load.watch0.Elapsed);
            
        }
    }
}
