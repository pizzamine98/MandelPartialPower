﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class ComplexPower
    {
        public string root,path;
        public List<CompPow> pows;
        public int npows;
        public void LoadFile()
        {
            // loads complex powers file
            path = root + "\\comppows.json";
            pows = JsonConvert.DeserializeObject<List<CompPow>>(System.IO.File.ReadAllText(path));
            npows = pows.Count;
        }
        public void StartUp()
        {
            npows = 0;
            coop = new ComplexOp();
            coop.pi = (Decimal)Math.PI;
            
        }
        public void MakePow()
        {
            // initializes complex powers.
            path = root + "\\comppows.json";
            Console.WriteLine("Enter key,real,imaginary");
            string line = Console.ReadLine();
            string[] things = line.Split(",");
            if(npows == 0)
            {
                pows = new List<CompPow>();
                pows.Add(new CompPow());
                pows[0].numkey = int.Parse(things[0]);
                Complex comp = new Complex();
                comp.parts = new Decimal[2];
                comp.parts[0] = Decimal.Parse(things[1]);
                comp.parts[1] = Decimal.Parse(things[2]);
                ComplexOp cop = new ComplexOp();
                cop.StartUp();
                cop.FindPolar(comp);
                pows[0].pow = comp;
                string json = JsonConvert.SerializeObject(pows);
                System.IO.File.WriteAllText(path, json);
            } else
            {
                pows.Add(new CompPow());
                pows[npows].numkey = int.Parse(things[0]);
                Complex comp = new Complex();
                comp.parts = new Decimal[2];
                comp.parts[0] = Decimal.Parse(things[1]);
                comp.parts[1] = Decimal.Parse(things[2]);
                ComplexOp cop = new ComplexOp();
                cop.StartUp();
                cop.FindPolar(comp);
                pows[npows].pow = comp;
                string json = JsonConvert.SerializeObject(pows);
                System.IO.File.WriteAllText(path, json);
            }
        }
        public ComplexOp coop;
        public Decimal r, rp, theta, thetap,p0,p1;
        public Complex TakeComplexPower(Complex numin, Complex powin)
        {
            Complex como = new Complex();
            coop.FindPolar(numin);
            r = numin.polar[0];
            theta = numin.polar[1];
            p0 = (powin.parts[0] * ((Decimal)(Math.Log((double)r))) - (Decimal)((double)theta * (double)powin.parts[1]));
            if (Math.Exp((double)p0) > (Double)Decimal.MaxValue)
            {
                rp = 100;
            }
            else
            {
                rp = (Decimal)(Math.Exp((double)p0));
            }
            thetap = ((Decimal)Math.Log((double)r) * powin.parts[1]) + (theta * powin.parts[0]);

            como.polar = new Decimal[2] { rp, thetap };
            coop.FindCartesian(como);


            return como;
        }
        public Complex UsePow(Complex powin, Complex numin)
        {
            Complex como = new Complex();
            if (numin.parts.Length == 2)
            {
                coop.FindPolar(numin);
            }
            else
            {
                coop.FindCartesian(numin);
            }
            r = numin.polar[0];
            if (r == 0)
            {
                rp = 0;
                thetap = 0;
            }
            else
            {
                theta = numin.polar[1];
                if (false)
                {
                    Console.WriteLine(numin.parts[0] + " " + numin.parts[1]);
                    Console.WriteLine(powin.parts[0]);
                    Console.WriteLine(r);
                    Console.WriteLine(theta);
                    Console.WriteLine(powin.parts[1]);
                }
                p0 = (powin.parts[0] * ((Decimal)(Math.Log((double)r))) - (Decimal)((double)theta * (double)powin.parts[1]));
                rp = (Decimal)(Math.Exp((double)p0));
                thetap = ((Decimal)Math.Log((double)r) * powin.parts[1]) + (theta * powin.parts[0]);
            }
            como.polar = new Decimal[2] { rp, thetap };
            coop.FindCartesian(como);


            return como;
        }
        public Complex UsePow(int keyin,Complex numin)
        {
            // Special function to get complex power of a complex number, slower than fraction or integer.
            Complex como = new Complex();
            
            int index0 = 0;
            for(int iiz = 0; iiz < npows; iiz++)
            {
                if(keyin == pows[iiz].numkey)
                {
                    index0 = iiz;
                }
            }
            if (numin.parts.Length == 2)
            {
                coop.FindPolar(numin);
            }
            else
            {
                coop.FindCartesian(numin);
            }
            r = numin.polar[0];
            theta = numin.polar[1];
            p0 = (pows[index0].pow.parts[0] * ((Decimal)(Math.Log((double)r))) - (Decimal)((double)theta * (double)pows[index0].pow.parts[1]));
            rp = (Decimal)(Math.Exp((double)p0));
            thetap = ((Decimal)Math.Log((double)r) * pows[index0].pow.parts[1]) + (theta * pows[index0].pow.parts[0]);
            
            como.polar = new Decimal[2] { rp, thetap };
            coop.FindCartesian(como);
          
            
            return como;
        }
    }
}
