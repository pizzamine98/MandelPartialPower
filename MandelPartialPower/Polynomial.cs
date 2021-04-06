using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class Polynomial
    {
        public PolyTerm[] terms;
        public int nterms;
        public int id;
        public string polystring;
        public string coeffstring;
        private ComplexOp cop;
        private int maxitts;
        private Decimal maxto;
        private Complex startval;
        public void Setup(int whichmethodin,int maxittsin, Decimal maxtoin,Complex startvalin)
        {
            cop = new ComplexOp();
            cop.whichmethod = whichmethodin;
            cop.StartUp();
            Console.WriteLine("NOOO " + cop.powol.pows.Count);
            maxitts = maxittsin;
            maxto = maxtoin;
            startval = startvalin;
            cop.FindPolar(startval);
        }
        public void Setup2(string rootin)
        {
            cop.root = rootin;
        }
        public void MakeAllPolar()
        {
            if (maxitts == 0)
            {
                cop = new ComplexOp();
                cop.whichmethod = 1;
                cop.StartUp();
            }
            for (int ihh = 0; ihh < nterms; ihh++)
            {
                cop.FindPolar(terms[ihh].coefficient);
            }
        }
        public void MakePolyString()
        {
            cop = new ComplexOp();
            cop.whichmethod = 0;
            cop.StartUp();
            polystring = "";
            for(int ih = nterms-1; ih > -1; ih--)
            {
               
              
                polystring = polystring + cop.MakeComplexString(terms[ih].coefficient, true, 3);
                if(terms[ih].cpow[0] != 0)
                {
                 
                    if(terms[ih].cpow[1] == 1)
                    {
                        if (terms[ih].cpow[0] == 1)
                        {
                            polystring = polystring + "c";
                        }
                        else
                        {
                            polystring = polystring + "c^" + terms[ih].cpow[0];
                        }
                        } else
                    {
                      
                        polystring = polystring + "c^(" + terms[ih].cpow[0] + "/" + terms[ih].cpow[1] + ")";
                    }
                }
                if (terms[ih].zpow[0] != 0)
                {
                 
                    if (terms[ih].zpow[1] == 1)
                    {
                        if (terms[ih].zpow[0] == 1)
                        {
                            polystring = polystring + "z";
                        }
                        else
                        {
                            polystring = polystring + "z^" + terms[ih].zpow[0];
                        }
                    }
                    else
                    {
                        
                        polystring = polystring + "z^(" + terms[ih].zpow[0] + "/" + terms[ih].zpow[1] + ")";
                    }
                }
                
                if(ih != 0)
                {
                    polystring = polystring + " ";
                    string fool = cop.MakeComplexString(terms[ih - 1].coefficient,true,3);
                    if (!fool.StartsWith("-"))
                    {
                        polystring = polystring + "+ ";
                    }
                }
            }
        }
        private Complex output,term,inputin;
        
        public int FindResult( Complex cvalin)
        {
            inputin = new Complex();
            inputin.parts = new Decimal[2] { startval.parts[0], startval.parts[1] };
            cop.FindPolar(cvalin);
            cop.FindPolar(inputin);
            int curitt = 0;
            output = new Complex();
            output.polar = new Decimal[2] { 0, 0 };
            output.parts = new Decimal[2] { 0, 0 };

            while(curitt < maxitts && output.polar[0] < maxto)
            {
                curitt++;
                output.polar = new Decimal[2] { 0, 0 };
                output.parts = new Decimal[2] { 0, 0 };
                for (int ih = 0; ih < nterms; ih++)
                {
                    if (false)
                    {
                        Console.WriteLine("COEFFICIENT " + ih);
                        cop.PrintNumber(terms[ih].coefficient);
                        Console.WriteLine("INPUTIN " + ih);
                        cop.PrintNumber(inputin);
                    }
                    if (terms[ih].zpow[0] == 0 && terms[ih].zpow[1] == 1)
                    {
                        term = new Complex();
                        term.parts = new Decimal[2] { 1, 0 };
                        cop.FindPolar(term);
                    }
                    else
                    {
                        term = cop.MultiplyP(terms[ih].coefficient, cop.PowP(inputin, terms[ih].zpow[0], terms[ih].zpow[1]));
                    }
                    if (terms[ih].cpow[0] != 0)
                    {
                        term = cop.MultiplyP(cop.PowP(cvalin, terms[ih].cpow[0], terms[ih].cpow[1]), term);
                    }
                    if(false)
                    Console.WriteLine("TERM " + ih);

                    cop.FindCartesian(term);
                    if(false)
                    cop.PrintNumber(term);
                    output=cop.Add(output, term);
                }
                cop.FindPolar(output);
                inputin.parts = new Decimal[2] { output.parts[0],output.parts[1]};
                cop.FindPolar(inputin);
                if (false)
                {
                    Console.WriteLine("ITTERATION " + curitt);
                    cop.PrintNumber(output);
                }
            }
            if(false)
            Console.WriteLine(curitt);
            return curitt;
        }
    }
}
