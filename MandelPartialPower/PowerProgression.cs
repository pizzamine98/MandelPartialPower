using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace MandelPartialPower
{
    class PowerProgression
    {
        public int nround,nsteps,w,h,curitt,maxitts,totalpixels,cbyte,crunid,baseid;
        public Complex startpow, endpow,delta,centerpoint,curval;
        public Complex[] powers;
        public Decimal upy;
        public ComplexOp cop;
        public Stopwatch watch, watch2;
        public Polynomial basepoly;
        public double[] doom;
        public byte[] coldata;
        public GraphSettings set0;
        public Color colon;
        public bool thisblack;
        public ImageHandler handle;
        public int[] nblack;
        public Bitmap mapo;
        public string root, root2,root3,root4,path0,path1;
        public string[] files0,powlines;
        public PolyMaker makeit;
        public bool circle;
        public void SetupStuff()
        {
            set0 = new GraphSettings();
            set0.height = h;
            set0.centerpoint = centerpoint;
            set0.upy = upy;
            set0.Calculate();
            w = set0.width;
            totalpixels = h * w;
            makeit = new PolyMaker();
            makeit.root = root;
            makeit.Setup();
            for(int ii = 0; ii < makeit.polys.Count; ii++)
            {
                if(makeit.polys[ii].id == baseid)
                {
                    basepoly = makeit.polys[ii];
                }
            }
            root2 = root + "\\powprogrundata";
            root3 = root2 + "\\runinfos";
            files0 = Directory.GetFiles(root3);
            crunid = files0.Length;
            root4 = root2 + "\\runimages\\run_" + crunid;
            Directory.CreateDirectory(root4);
            path0 = root3 + "\\runinfo_" + crunid + ".json";
            pee = new PPRunData();
            pee.w = w;
            pee.h = h;
            pee.runid = crunid;
            pee.nsteps = nsteps;
            pee.nround = nround;
            powers = new Complex[nsteps];
            powers[0] = startpow;
            cop = new ComplexOp();
            pee.maxitts = maxitts;
        
            coldata = new byte[4 * totalpixels];
            cop.whichmethod = 0;
            cop.StartUp();
            cop.FindPolar(powers[0]);
            cop.RoundIt(powers[0], nround);
            watch = new Stopwatch();
            watch2 = new Stopwatch();
            pee.starttime = DateTime.Now;
            watch.Start();
            delta = new Complex();
            Complex temp0 = new Complex();
            temp0.parts = new Decimal[2] { -startpow.parts[0], -startpow.parts[1] };
            delta = cop.Add(temp0, endpow);
            temp0.parts = new Decimal[2] { (Decimal)1 / (Decimal)(nsteps - 1), 0 };
            delta = cop.Multiply(delta, temp0);
            nblack = new int[nsteps];
        }
        public PPRunData pee;
        public int maxdoom;
        public double rat0;
        public void SetupParameters()
        {
            powlines = new string[nsteps];
            if (circle)
            {
                cop.FindPolar(powers[0]);
                Decimal r = powers[0].polar[0];
                Decimal theta = powers[0].polar[1];
                Decimal dtheta = (Decimal)(Math.PI*2) / nsteps;
                for (int ii = 1; ii < nsteps; ii++)
                {
                    powers[ii] = new Complex();
                    powers[ii].polar = new Decimal[2];
                    theta += dtheta;
                    if(theta > (Decimal)Math.PI)
                    {
                        theta -= (Decimal)(2 * Math.PI);
                    }
                    powers[ii].polar[0] = r;
                    powers[ii].polar[1] = theta;
                    cop.FindCartesian(powers[ii]);
                    cop.RoundIt(powers[ii], nround);
                    Console.WriteLine("POWER " + ii + ":" + cop.MakeComplexString(powers[ii], false, nround));
                }
            } else {
                for (int ii = 1; ii < nsteps; ii++)
                {
                    powers[ii] = cop.Add(powers[ii - 1], delta);
                    cop.FindPolar(powers[ii]);
                    cop.RoundIt(powers[ii], nround);
                    Console.WriteLine("POWER " + ii + ":" + cop.MakeComplexString(powers[ii], false, nround));
                }
            }
            doom = new double[3];
            Complex zero = new Complex();
            zero.parts = new Decimal[2] { 0, 0 };
            basepoly.Setup(0, maxitts, (Decimal)2000.0, zero);
            handle = new ImageHandler();
            pee.basepoly = basepoly;
            pee.nblacks = new int[nsteps];
            pee.timestaken = new TimeSpan[nsteps];
            pee.timesperpixel = new TimeSpan[nsteps];
            pee.powers = new Complex[nsteps];
            
            set0.startpoint.parts[0] = set0.startpoint.parts[0];
            set0.startpoint.parts[1] = set0.startpoint.parts[1];
            Console.WriteLine(set0.startpoint.parts[0] + " " + set0.startpoint.parts[1] + " " + set0.centerpoint.parts[0] + " " + set0.centerpoint.parts[1]);
            for (int ii = 0; ii < nsteps; ii++)
            {
                maxdoom = 0;
                powlines[ii] = ii + "," + cop.MakeComplexString(powers[ii], false, nround);
                Console.WriteLine("DX = " + set0.deltax + " DY = " + set0.deltay);
                Console.WriteLine("POWLINE " + powlines[ii]);
                watch2.Start();
                pee.powers[ii] = powers[ii];
                nblack[ii] = 0;
                basepoly.SetTempoPow(powers[ii]);
                curval = new Complex();
                curval.parts = new Decimal[2] { set0.startpoint.parts[0], set0.startpoint.parts[1] };
                cop.FindPolar(curval);
                cop.RoundIt(curval, nround);
                Console.WriteLine(set0.startpoint.parts[0] + " " + set0.startpoint.parts[1] + " XXX");
                cbyte = 0;
                Console.WriteLine("Step " + ii );
                for (int yy = 0; yy < h; yy++)
                {
                    cop.RoundIt(curval, nround);
                    if (yy%100 == 99)
                    {
                        int binge = yy + 1;
                        Console.WriteLine("LINE " + binge + " out of " + h + " NBLACK " + nblack[ii]);
                        Console.WriteLine("Y = " + curval.parts[0] + " " + curval.parts[1]);
                    }
                    for(int xx = 0; xx < w; xx++)
                    {
                        cop.RoundIt(curval, nround);
                        curitt = basepoly.FindResult(curval);
                        doom[0] = 360.0 * (double)curitt / (double)maxitts;
                        doom[1] = 1.0;
                        doom[2] = 1.0;
                        if(curitt == maxitts)
                        {
                            
                            nblack[ii]++;
                            doom[2] = 0.0;
                        } else
                        {
                            if(curitt > maxdoom)
                            {
                                maxdoom = curitt;
                            }
                        }
                        colon = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
                        coldata[cbyte] = 255;
                        coldata[cbyte + 1] = colon.R;
                        coldata[cbyte + 2] = colon.G;
                        coldata[cbyte + 3] = colon.B;
                        cbyte += 4;
                        curval.parts[0] += set0.deltax;

                    }
                    curval.parts[0] = set0.startpoint.parts[0];
                    curval.parts[1] += set0.deltay;
                }
                if ((maxdoom + 1) != maxitts && false) {
                    cbyte = 0;
                    rat0 = (double)(maxitts-1)/(double)(maxdoom);
                    Console.WriteLine("MAXDOOM: " + maxdoom + " " + rat0);
                    double mane = 0;
                    for(int yy = 0; yy < totalpixels; yy++)
                    {
                        colon = ColorUtils.GetRgb((double)coldata[cbyte + 1], (double)coldata[cbyte + 2],
                            (double)coldata[cbyte + 3]);
                        mane = (double)colon.GetHue();
                        if(mane != 0)
                        {
                            mane *= rat0;
                            if(mane > 360.0)
                            {
                                mane = 360.0;
                            }
                            colon = ColorUtils.HsvToRgb(mane, 1.0, 1.0);
                            coldata[cbyte] = 255;
                            coldata[cbyte + 1] = colon.R;
                            coldata[cbyte + 2] = colon.G;
                            coldata[cbyte + 3] = colon.B;
                            cbyte += 4;
                        } else
                        {
                            coldata[cbyte] = 255;
                            coldata[cbyte + 1] = 0;
                            coldata[cbyte + 2] = 0;
                            coldata[cbyte + 3] = 0;
                            cbyte += 4;
                        }
                    }
                 }
                mapo = handle.GetDataPicture(w, h, coldata);
                path1 = root4 + "\\step_" + ii + ".jpeg";
                
                string texto = "power = " + cop.MakeComplexString(powers[ii], false, 6);
                PointF firstlocation = new PointF(10f, 10f);
                using (Graphics graphics = Graphics.FromImage(mapo))
                {
                    using (Font arialfont = new Font("Arial", 16))
                    {
                        graphics.DrawString(texto, arialfont, Brushes.White, firstlocation);
                    }
                }
                    mapo.Save(path1, System.Drawing.Imaging.ImageFormat.Jpeg);
                watch2.Stop();
                MakePPRunData(ii);
                watch2.Reset();
            }
            watch.Stop();
            pee.endtime = DateTime.Now;
            pee.timetaken = watch.Elapsed;
            pee.timeperpixel = watch.Elapsed / (totalpixels * nsteps);
            watch.Reset();
            string json = JsonConvert.SerializeObject(pee);
            System.IO.File.WriteAllText(path0, json);
            string path2 = root + "\\powersdata_" + crunid + ".csv";
            System.IO.File.WriteAllLines(path2, powlines);
        }
        public void MakePPRunData(int indexin)
        {
            pee.nblacks[indexin] = nblack[indexin];
            pee.timestaken[indexin] = watch2.Elapsed;
            Console.WriteLine("TIME TAKEN: " + watch2.Elapsed);
            pee.timesperpixel[indexin] = watch2.Elapsed / totalpixels;
            Console.WriteLine("TIME TAKEN PER PIXEL: " + pee.timesperpixel[indexin]);
        }
    }
}
