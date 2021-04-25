using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;

namespace MandelPartialPower
{
    class Program
    {
        static void Main(string[] args)
        {
            int which = 4;
            if (which == 0)
            {
                Stopwatch watch = new Stopwatch();
                Complex comp = new Complex();
                comp.parts = new Decimal[2] { -(Decimal)0.0005, -1 };
                ComplexOp cop = new ComplexOp();
                cop.whichmethod = 1;
                cop.StartUp();
                watch.Start();
                cop.FindPolar(comp);
                watch.Stop();
                Console.WriteLine("TIME TO FIND POLAR: " + watch.Elapsed);
                watch.Reset();
                cop.PrintNumber(comp);
                watch.Start();
                comp = cop.PowP(comp, 5, 4);
                cop.FindCartesian(comp);
                cop.PrintNumber(comp);
                watch.Stop();
                Console.WriteLine("TIME TO CONVERT: " + watch.Elapsed);
            } else if(which == 1)
            {
                ComplexOp cop = new ComplexOp();
                cop.whichmethod = 1;
                cop.StartUp();
                System.Random rando = new System.Random();
                Polynomial toad = new Polynomial();
                toad.terms = new PolyTerm[2];
                toad.nterms = 2;
                toad.terms[0] = new PolyTerm();
                toad.terms[0].coefficient = new Complex();
                Decimal d0 = (Decimal)((rando.NextDouble() * 4) - 2);
                Decimal d1 = (Decimal)((rando.NextDouble() * 4) - 2);
                toad.terms[0].coefficient.parts = new Decimal[2] { 1, 0 };
                cop.FindPolar(toad.terms[0].coefficient);
                toad.terms[0].cpow = new int[2] { 1, 1 };
                toad.terms[0].zpow = new int[2] { 0, 1 };
                toad.terms[1] = new PolyTerm();
                toad.terms[1].coefficient = new Complex();
                toad.terms[1].coefficient.parts = new Decimal[2] { 1, 0 };
                cop.FindPolar(toad.terms[1].coefficient);
                toad.terms[1].cpow = new int[2] { 0, 1 };
                toad.terms[1].zpow = new int[2] { 2, 1 };
                Complex crand = new Complex();
                Complex startval = new Complex();
                crand.parts = new Decimal[2] { d0,d1 };
                
                
                cop.FindPolar(crand);
                Console.WriteLine("CVALUE: ");
                cop.PrintNumber(crand);
                startval.parts = new Decimal[2] { 0, 0 };
                
                cop.FindPolar(startval);
                toad.Setup(0, 256, 20000, startval);
                toad.MakePolyString();
                Console.WriteLine("POLYSTRING:");
                Console.WriteLine(toad.polystring);
           
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int iii = 0; iii < 10000; iii++)
                {
                    d0 = (Decimal)((rando.NextDouble() * 4) - 2);
                    d1 = (Decimal)((rando.NextDouble() * 4) - 2);
                    crand.parts = new Decimal[2] { d0, d1 };
                    cop.FindPolar(crand);
                    int curitt0 = toad.FindResult(crand);
                }
                watch.Stop();
                TimeSpan permillion = watch.Elapsed * 100;
                Console.WriteLine("TIME FOR A MILLION = " + permillion);
            } else if(which == 2)
            {
                PolyMaker make = new PolyMaker();
                Console.WriteLine("Enter directory to load files from.");
                Console.WriteLine("Example: C:\\Users\\Pizzamine98\\Desktop\\partialmendel");
                make.root = @"" + Console.ReadLine();
                make.Setup();
                make.Execute();
            } else if(which == 3)
            {
                ComplexPower powo = new ComplexPower();
                powo.StartUp();
                powo.root = @"C:\Users\Pizzamine98\Desktop\partialmendel";
                powo.LoadFile();
                
                powo.MakePow();
            } else if(which == 4)
            {
                PowerProgression powprog = new PowerProgression();
                powprog.nround = 6;
                powprog.nsteps = (95*4)+1;
                if (true)
                {
                    powprog.nsteps = 360;
                }
                powprog.startpow = new Complex();
                powprog.startpow.parts = new Decimal[2] { (Decimal)2,0};
                powprog.endpow = new Complex();
                powprog.endpow.parts = new Decimal[2] {11, 0};
                powprog.baseid = 12;
                powprog.h = 1080;
                powprog.circle = true;
                powprog.maxitts = 64;
                powprog.centerpoint = new Complex();
                powprog.centerpoint.parts = new Decimal[2] { 0, 0 };
                powprog.upy = (Decimal)4;
                powprog.root = @"C:\Users\Pizzamine98\Desktop\partialmendel";
                powprog.SetupStuff();
                powprog.SetupParameters();
            }
        }
    }
}
