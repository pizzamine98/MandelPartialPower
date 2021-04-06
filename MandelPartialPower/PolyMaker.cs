using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace MandelPartialPower
{
    class PolyMaker
    {
        public string root,root2,json;
        public string[] polypaths;
        public List<Polynomial> polys;
        public int npolys;
        public bool bored;
        public int nzoomy;
        public void Setup()
        {
            root2 = root + "\\polynomials";
            polypaths = Directory.GetFiles(root2);
            npolys = polypaths.Length;
            polys = new List<Polynomial>();
            if(npolys != 0)
            {
                for(int ii = 0; ii < npolys; ii++)
                {
                    json = System.IO.File.ReadAllText(polypaths[ii]);
                    polys.Add(JsonConvert.DeserializeObject<Polynomial>(json));
                }
            }

        }
        private string command;
        public Stopwatch watch;
        public void Execute()
        {
            bored = false;
            while (!bored)
            {
                Console.WriteLine("Enter \"newpoly\" to make new polynomial,\"run\" to run stuff,\"printpolys\" to print polynomials or \"exit\" to exit");
                command = Console.ReadLine();
                if (command.Equals("exit"))
                {
                    bored = true;
                } else if (command.Equals("newpoly"))
                {
                    Console.WriteLine("Enter coefficients/powers in following format: a0,b0,z0n,z0d,c0n,c0d a1,b1...." + "\n" + "where a is the real part of the coefficient, b is" +
                        "imaginary, z0n is numerator of z power, z0d is den of zpower and same for c");
                    string liney = Console.ReadLine();
                    string[] pool = liney.Split(" ");
                    Polynomial poly0 = new Polynomial();
                    poly0.nterms = pool.Length;
                    poly0.id = npolys;
                    poly0.terms = new PolyTerm[poly0.nterms];
                    for(int jj= 0; jj < poly0.nterms; jj++)
                    {
                        poly0.terms[jj] = new PolyTerm();
                        poly0.terms[jj].GetFromString(pool[jj]);
                    }
                    poly0.coeffstring = liney;
                    poly0.MakeAllPolar();
                    poly0.MakePolyString();
                    json = JsonConvert.SerializeObject(poly0);
                    System.IO.File.WriteAllText(root2 + "\\poly_" + poly0.id + ".json",json);
                    Setup();
                } else if (command.Equals("run"))
                {
                    MakeIdList();
                    Console.WriteLine("Enter polyid,nzoom,height,centerpointx,centerpointy,upy,startx,starty,zoomeach");
                    string liney = Console.ReadLine();
                    string[] things = liney.Split(",");
                    polyid = int.Parse(things[0]);
                    nzoom = int.Parse(things[1]);
                    width = int.Parse(things[2]);
                    cenx = Decimal.Parse(things[3]);
                    ceny = Decimal.Parse(things[4]);
                    upy = Decimal.Parse(things[5]);
                    startx = Decimal.Parse(things[6]);
                    starty = Decimal.Parse(things[7]);
                    zoomeach = Decimal.Parse(things[8]);
                    Decimal deltax = 0;
                    Decimal deltay = 0;
                    Grapher graphy = new Grapher();
                    graphy.root = root;
                    watch = new Stopwatch();
                    for (int ss = 0; ss < nzoom; ss++)
                    {
                        
                        
                        
                        graphy.whichmethod = 0;
                        graphy.maxitts = 360;
                        graphy.maxto = (Decimal)200.0;
                        graphy.gsettings = new GraphSettings();
                        graphy.gsettings.poly = polys[pids.IndexOf(polyid)];
                        graphy.gsettings.zoomnum = 0;
                        graphy.gsettings.zoomnum = ss;
                        graphy.gsettings.height = width;
                        if (ss == 0)
                        {
                            graphy.gsettings.centerpoint = new Complex();
                            graphy.gsettings.centerpoint.parts = new Decimal[2] { cenx, ceny };
                            graphy.gsettings.upy = upy;
                            
                            graphy.gsettings.Calculate();
                            deltax = graphy.gsettings.deltax;
                            deltay = graphy.gsettings.deltay;
                            graphy.startval = new Complex();
                            graphy.startval.parts = new Decimal[2] { startx, starty };
                            
                            

                        } else
                        {
                            deltax /= zoomeach;
                            deltay /= zoomeach;
                            graphy.gsettings.centerpoint = new Complex();
                            graphy.gsettings.centerpoint.parts = new Decimal[2] { graphy.newcenter.parts[0], graphy.newcenter.parts[1] };
                            Console.WriteLine(graphy.gsettings.centerpoint.parts[0] + ":::::" + graphy.gsettings.centerpoint.parts[1]);
                            graphy.gsettings.deltax = deltax;
                            graphy.gsettings.deltay = deltay;
                            graphy.startval = new Complex();
                            graphy.startval.parts = new Decimal[2] { startx, starty };
                            
                            graphy.gsettings.Calculate2();
                        }

                        watch.Start();
                       
                        graphy.StartUp();
                        
                        graphy.GoThrough();
                        graphy.GetNewCenterPoint();
                        watch.Stop();

                        data = new RunData();
                        data.timetaken = watch.Elapsed;
                        data.timeperpoint = data.timetaken / graphy.gsettings.totalpixels;
                        data.gsettings = graphy.gsettings;
                        data.maxto = graphy.maxto;
                        data.maxitts = graphy.maxitts;
                        data.whichmethod = graphy.whichmethod;
                        watch.Reset();
                        data.nblack = graphy.nblack;
                        handler = new RunHandler();
                        handler.root = root;
                        handler.data = data;
                        handler.SetupStuff();
                        
                        Console.WriteLine("RUN " + ss + " out of " + nzoom);
                        Console.WriteLine("TIME TAKEN: " + data.timetaken);
                        handler.WriteRun(graphy.coldata);
                        graphy.gsettings.zoomnum++;
                    }
                } else if (command.Equals("printpolys"))
                {
                    for(int iq = 0; iq < polys.Count; iq++)
                    {
                        polys[iq].MakePolyString();
                        Console.WriteLine(polys[iq].id + " " + polys[iq].polystring);
                    }
                }
            }
        }
        public RunHandler handler;
        public RunData data;
        public int polyid, nzoom, width;
        public Decimal cenx, ceny, upy,startx,starty,zoomeach;
        public List<int> pids;
        public void MakeIdList()
        {
            pids = new List<int>();
            for(int ii = 0; ii < npolys; ii++)
            {
                pids.Add(polys[ii].id);
            }
        }
    }
}
