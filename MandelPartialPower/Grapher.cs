using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace MandelPartialPower
{
    class Grapher
    {
        public GraphSettings gsettings;
        public void StartUp()
        {
            coldata = new byte[gsettings.totalpixels * 4];
            h = gsettings.height;
            w = gsettings.width;
            Console.WriteLine("H = " + h + " W = " + w);
            cop = new ComplexOp();
            cop.whichmethod = whichmethod;
            cop.StartUp();
            rando = new System.Random();
        }
        public int whichmethod;
        public int maxitts,ntot,nboundary;
        public Decimal maxto;
        public Complex startval,curval;
        public ComplexOp cop;
        public Color colon;
        public int rold;
        public Stopwatch watch2,watch3;
        public void GoThrough()
        {
            cbyte = 0;
            gsettings.poly.Setup(whichmethod, maxitts, maxto, startval);
            gsettings.poly.MakeAllPolar();
            curval = new Complex();
            isboundary = new bool[h, w];
            curval.parts = new Decimal[2] { gsettings.startpoint.parts[0], gsettings.startpoint.parts[1] };
            doom = new double[3];
            lastblack = false;
            nblack = 0;
            watch2 = new Stopwatch();
            watch3 = new Stopwatch();
            watch3.Start();
            watch2.Start();
            ntot = 0;
            nboundary = 0;
            bounds = new List<Complex>();
            rando = new System.Random();
            int nstored = 0;
            rold = gsettings.height / 20;
            int seed = rando.Next(0, rold);
            gsettings.poly.Setup2(root);
            for(int yy = 0; yy < h; yy++)
            {
               
                if(yy%20 == 19)
                {
                    watch2.Stop();
                    int linenum = yy + 1;
                    int nremaining = h - linenum;
                    Console.WriteLine("HEIGHT " + linenum + " out of " + h);
                    TimeSpan perline = watch3.Elapsed/linenum;
                    Console.WriteLine("TIME PER LINE: " + perline);
                    Console.WriteLine("TIME ELAPSED Set: " + watch2.Elapsed);
                    Console.WriteLine("TIME ELAPSED TOTAL: " + watch3.Elapsed);
                    if(false)
                    Console.WriteLine("YCOORD = " + curval.parts[1]);
                    Console.WriteLine("NBOUNDARY = " + nboundary);
                    TimeSpan timeremaining = perline * nremaining;
                    Console.WriteLine("NSTORED = " + nstored);
                    Console.WriteLine("TIME REMAINING: " + timeremaining);
                    if (false)
                    {
                        Console.WriteLine("NBLACK: " + nblack);
                        Console.WriteLine("NTOT: " + ntot);
                    }
                    watch2.Reset();
                    watch2.Start();
                }
                for(int xx = 0; xx < w; xx++)
                {
                    ntot++;
                    curitt=gsettings.poly.FindResult(curval);
                    if (yy == h / 2 || yy == (h / 2) + 1 || yy == (h / 2) - 1)
                    {
                        if (false)
                        {
                            Console.WriteLine(curval.parts[0] + "," + curval.parts[1] + " " + curitt);
                            Console.WriteLine(gsettings.startpoint.parts[0] + "," + gsettings.startpoint.parts[1]);
                            Console.WriteLine(gsettings.centerpoint.parts[0] + ":" + gsettings.centerpoint.parts[1]);
                        }
                        }
                    doom[0] = 360.0 * (double)curitt / (double)maxitts;
                    doom[1] = 1.0;
                    doom[2] = 1.0;
                    thisblack = false;
                    if(curitt == maxitts)
                    {
                        nblack++;
                        doom[2] = 0.0;
                        thisblack = true;
                    }
                    isboundary[yy, xx] = false;
                    if((thisblack == true && lastblack == false))
                    {
                        nboundary++;
                        if (nboundary % rold == seed)
                        {
                            bounds.Add(new Complex());
                            bounds[nstored].parts = new Decimal[2] { curval.parts[0], curval.parts[1] };
                            nstored++;
                        }
                        
                        isboundary[yy, xx] = true;
                    }
                    colon = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
                    coldata[cbyte] = 255;
                    coldata[cbyte + 1] = colon.R;
                    coldata[cbyte + 2] = colon.G;
                    coldata[cbyte + 3] = colon.B;
                    cbyte += 4;
                    lastblack = thisblack;
                    curval.parts[0] += gsettings.deltax;
                }
                curval.parts[0] = gsettings.startpoint.parts[0];
                curval.parts[1] += gsettings.deltay;
            }
            watch2.Stop();
            watch3.Stop();
        }
        public int nblack;
        public List<Complex> bounds;
        public void GetNewCenterPoint()
        {
            foolish = true;
            ranval = new int[2];
            rando = new System.Random();
            int index0 = rando.Next(0, bounds.Count);
            newcenter = new Complex();
            newcenter.parts = new Decimal[2] { bounds[index0].parts[0], bounds[index0].parts[1] };
           
        }
        public string root;
        public Complex newcenter;
        public int[] ranval;
        public bool foolish;
        public System.Random rando;
        public double[] doom;
        public int h, w,cbyte,curitt;
        public bool[,] isboundary;
        public bool lastblack, thisblack;
        public byte[] coldata;
    }
}
