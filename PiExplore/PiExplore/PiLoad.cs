using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PiExplore
{
    class PiLoad
    {
        public string root,pistr,temp;
        public Stopwatch watch0;
        public char[] splot;
        public List<int> tagos;
        public string[] lines;
        public int log10,ndig,colo;
        public void LoadIt()
        {
            root = @"C:\Users\Pizzamine98\Desktop\piexplore";
            watch0 = new Stopwatch();
            lines = System.IO.File.ReadAllLines(root + "\\e.dat");
            pistr = "";
            for(int ii = 0; ii < lines.Length; ii++)
            {
                pistr = pistr + lines[ii];
            }
            
            pistr.Remove(' ');
            splot = pistr.ToCharArray();
            ndig = splot.Length;
            Console.WriteLine("NDIGITS: " + splot.Length);
            tagos = new List<int>();
            temp = "";
            watch0.Start();
            
            for(int ii = 0; ii < ndig; ii++)
            {
                
                log10 = (int)Math.Log10((double)(ii + 1));
                if (ii + log10+1 <= ndig - 1)
                {
                    temp = "";
                    
                    temp = pistr.Substring(ii,log10+1);
                    
                    tagos.Add(int.Parse(temp));
                    if (ii < 1001 && false)
                    {
                        int fill = ii + 1;
                        Console.WriteLine(fill + " " + tagos[ii] + " " + log10 + " " + temp.Length + " " + temp);
                    }
                }
            }
            watch0.Stop();
            Console.WriteLine("TIMETAKEN: " + watch0.Elapsed);
            loops = new List<Loop>();
        }
        public int index0,state,nextnum;
        public List<int> loom;
        public List<Loop> loops;
        public Loop loop;
        public void StartUpLoop(int valin)
        {
            loom = new List<int>();
            state = 1;
            loop = new Loop();
            loop.startval = valin;
            loop.loop = new List<int>();
            loop.loop.Add(valin);
            
            FindLoop(valin);
            loop.loopl = loop.loop.Count;
            loop.state = state;
            loops.Add(loop);
        }
        public void PrintInteresting()
        {
            foreach (Loop loon in loops)
            {
                if(loon.state != -1 && loon.state != 3 && loon.state != 0)
                {
                    bool mils = true;
                    for(int kk = 0; kk < loon.loop.Count; kk++)
                    {
                        if(loon.loop[kk] < loon.startval)
                        {
                            mils = false;
                        }
                    }
                    if (mils)
                    {
                        Console.WriteLine("VALUE " + loon.startval);
                        string mall = "";
                        foreach (int mill in loon.loop)
                        {
                            mall = mall + mill + "->";
                        }
                        mall = mall.Substring(0, mall.Length - 2);
                        Console.WriteLine(mall);
                        if (loon.state == 0)
                        {
                            Console.WriteLine("Ends in singularity");
                        }
                        else
                        {
                            Console.WriteLine("Loops around");
                        }
                    }
                }
            }
        }
        
        public void FindLoop(int valin)
        {
            index0 = pistr.IndexOf(valin.ToString());
            
            nextnum = index0 + 1;
            
            if(index0 == -1)
            {
                state = -1;
                if(false)
                Console.WriteLine("Out of Bounds");
            }
            else
            {
                if (nextnum < loop.startval && false)
                {
                    foreach (int noom in loops[nextnum - 1].loop)
                    {
                        loop.loop.Add(noom);
                    }
                    if (loops[nextnum - 1].state == 3)
                    {
                        loop.state = 3;
                    }
                    else if (loops[nextnum - 1].loop.Contains(loop.startval) && loops[nextnum - 1].state == 0)
                    {
                        loop.state = 0;
                    }
                    else
                    {
                        loop.state = loops[nextnum - 1].state;
                    }
                    }
                else
                {
                    if (nextnum == valin)
                    {
                        state = 0;// Self loop(endpoint)
                        if (false)
                            Console.WriteLine("Singularity");
                    }
                    else if (nextnum == loop.loop[0])
                    {
                        state = 2; // Full loop
                        if (false)
                            Console.WriteLine("LOOPS");
                    }
                    else if (loop.loop.Contains(nextnum))
                    {
                        state = 3;
                        if (false)
                            Console.WriteLine("Partial Loop");
                    }
                    else
                    {
                        state = 1;
                        loop.loop.Add(nextnum);

                        FindLoop(nextnum);
                    }
                }
            }
        }
    }
}
