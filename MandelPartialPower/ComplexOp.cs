using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MandelPartialPower
{
    
        
    
    class ComplexOp
    {
        public Complex Multiply(Complex num0in, Complex num1in)
        {
            Complex result0 = new Complex();
            result0.parts = new Decimal[2] { (num0in.parts[0] * num1in.parts[0]) - (num0in.parts[1] * num1in.parts[1]), (num0in.parts[0] * num1in.parts[1]) + (num0in.parts[1] * num1in.parts[0]) };
            return result0;
        }
        public Decimal FindMagnitude(Complex num0in)
        {
            Decimal mag = 0;
            mag += num0in.parts[0] * num0in.parts[0];
            mag += num0in.parts[1] * num0in.parts[1];
            mag = (Decimal)(Math.Sqrt((double)mag));
            return mag;
        }
        public string MakeComplexString(Complex num0in, bool iscoeffin,int nroundin)
        {
            string compstr = "";
            /*
             * states:
             * 0 - 0
             * 1 - 1
             * 2 - -1
             * 3 - + anything
             * 4 - - anything
             */
            states = new int[2];
            num0 = new Complex();
            num0.parts = new Decimal[2] { Math.Round(num0in.parts[0], nroundin), Math.Round(num0in.parts[1], nroundin) };
            for (int ikk = 0; ikk < 2; ikk++) {
                states[ikk] = 4;
                if (num0in.parts[ikk] == 0)
                {
                    states[ikk] = 0;
                } else if(num0in.parts[ikk] == 1)
                {
                    states[ikk] = 1;
                } else if(num0in.parts[ikk] == -1)
                {
                    states[ikk] = 2;
                } else if(num0in.parts[ikk] > 0)
                {
                    states[ikk] = 3;
                }
            }
            if (states[0] == 0 && states[1] == 0)
            {
                compstr = "0";
            }
            else
            {
                if (!(states[0] == 0))
                {
                    if (iscoeffin && states[1] == 0 && states[0] == 1)
                    {
                        // case of 1 for coefficient

                    }
                    else
                    {
                        if (states[1] != 0)
                        {
                            compstr = compstr + "(";
                        }
                        compstr = compstr + num0.parts[0];
                    }
                }
                
                
                    if( states[1] == 4)
                    {
                        compstr = compstr + " - " + -num0.parts[1] + "i";
                    } else if(states[1] == 1)
                {
                    compstr = compstr + " + i";
                } else if(states[1] == 2)
                {
                    compstr = compstr + " - i";
                } else if(states[1] == 0)
                {

                } else if(states[1] == 3)
                {
                    compstr = compstr + " + " + num0.parts[1] + "i";
                }
                if (compstr.StartsWith("("))
                {
                    compstr = compstr + ")";
                }
                
            }
            return compstr;
        }
        public Complex num0;
        public int[] states;
        public Complex MultiplyP(Complex num0in, Complex num1in)
        {
            Complex result0 = new Complex();
            Decimal thetap = num0in.polar[1] + num1in.polar[1];
            int case0 = -1;
            if(thetap > pi)
            {
                case0 = 0;
               
            } else if(thetap <= -pi)
            {
                case0 = 1;
            }
            if(case0 == 0)
            {
                thetap -= 2*pi;
            } else if(case0 == 1)
            {
                thetap += 2*pi;
            }
            result0.polar = new Decimal[2] { num0in.polar[0] * num1in.polar[0], thetap};
            
            return result0;
        }
        public void StartUp()
        {
            random = new System.Random();
            pi = (Decimal)Math.PI;
            powol = new ComplexPower();
            powol.root = @"C:\Users\Pizzamine98\Desktop\partialmendel";
            powol.StartUp();
            powol.LoadFile();
        }
        public void StartUp2()
        {
            powol = new ComplexPower();
            powol.root = root;
            powol.StartUp();
        }
        
        public ComplexPower powol;
        public string root;
        public System.Random random;
        public int whichmethod,k,nrem;
        public Decimal pi;
        public Complex Add(Complex num0in, Complex num1in)
        {
            Complex result0 = new Complex();
            result0.parts = new Decimal[2] { num0in.parts[0] + num1in.parts[0], num0in.parts[1] + num1in.parts[1] };
            return result0;
        }
        public double minmag;
        public Complex result1;
        public double Distance(Complex in0, Complex in1)
        {
            double distance = 0;
            distance += Math.Pow((double)in1.parts[0] - (double)in0.parts[0],2);
            distance += Math.Pow((double)in1.parts[1] - (double)in0.parts[1], 2);
            
            
            distance = (Math.Sqrt(distance));
            return distance;
        }
        public Complex PowP(Complex num0in, int numeratorin, int denominatorin)
        {
            minmag = Double.MaxValue;
            Complex result0 = new Complex();
            result0.polar = new Decimal[2];
            double dist = 0;
            Complex result1 = new Complex();
            
            /*
             * Which method is what we use to find which root we use.
             * 0 - k = 0 root
             * 1 - random root
             */
            if (num0in.polar[0] == 0)
            {
                result0.polar[0] = 0;
                result0.polar[1] = 0;
            }
            else
            {
                if (numeratorin == 0)
                {
                    result0.polar[0] = 1;
                    result0.polar[1] = 0;
                }
                else
                {
                    if(numeratorin < 0)
                    {
                        Complex holy = powol.UsePow(numeratorin,num0in);
                        result0.polar[0] = holy.polar[0];
                        result0.polar[1] = holy.polar[1];

                    } else {
                        if (denominatorin == 1)
                        {
                            result0.polar[1] = numeratorin * num0in.polar[1];
                        }
                        else
                        {
                            if (whichmethod == 0)
                            {
                                k = 0;
                                result0.polar[1] = numeratorin * num0in.polar[1] / (Decimal)denominatorin;
                            }
                            else if (whichmethod == 1)
                            {
                                k = random.Next(0, denominatorin);
                                result0.polar[1] = ((numeratorin * num0in.polar[1]) + (2 * (Decimal)k * pi)) / (Decimal)denominatorin;

                            }
                            else if (whichmethod == 2)
                            {
                                result1 = new Complex();
                                result1.polar = new Decimal[2];
                                result0.polar[0] = (Decimal)Math.Pow((double)num0in.polar[0], (double)numeratorin / (double)denominatorin);
                                result1.polar[0] = result0.polar[0];

                                for (int kss = 0; kss < denominatorin; kss++)
                                {
                                    result1.polar[1] = ((numeratorin * num0in.polar[1]) + (2 * (Decimal)kss * pi)) / (Decimal)denominatorin;
                                    FindCartesian(result1);
                                    dist = Distance(result1, num0in);
                                    if (dist < minmag)
                                    {
                                        minmag = dist;
                                        result0.polar[1] = ((numeratorin * num0in.polar[1]) + (2 * (Decimal)kss * pi)) / (Decimal)denominatorin;
                                    }
                                }
                            }
                        }
                    }
                    if (result0.polar[1] <= -pi)
                    {
                        nrem = (int)(-result0.polar[1] / (2 * pi));

                        result0.polar[1] += 2 * nrem * pi;
                        if(result0.polar[1] <= -pi)
                        {
                            result0.polar[1] += 2 * pi;
                        }
                    }
                    else if (result0.polar[1] > pi)
                    {
                        nrem = (int)(result0.polar[1] / (2 * pi));

                        result0.polar[1] -= 2 * nrem * pi;
                        if(result0.polar[1] > pi)
                        {
                            result0.polar[1] -= 2 * pi;
                        }
                    }
                    if(numeratorin >= 0)
                    result0.polar[0] = (Decimal)Math.Pow((double)num0in.polar[0], (double)numeratorin / (double)denominatorin);
                }
            }
                return result0;
        }
        public void FindCartesian(Complex num1in)
        {
            num1in.parts = new Decimal[2] { num1in.polar[0] * (Decimal)Math.Cos((double)num1in.polar[1]), num1in.polar[0] * (Decimal)Math.Sin((double)num1in.polar[1]) };
        }
        public void FindPolar(Complex num0in)
        {
            num0in.polar = new Decimal[2];
            num0in.polar[0] = FindMagnitude(num0in);
            if(num0in.parts[0] == 0)
            {
                if(num0in.parts[1] == 0)
                {
                    num0in.polar[1] = 0;
                    
                } else if(num0in.parts[1] < 0)
                {
                    num0in.polar[1] = -pi;
                } else
                {
                    num0in.polar[1] = pi;
                }
            } else if(num0in.parts[0] > 0)
            {
                num0in.polar[1] = (Decimal)Math.Atan2((double)num0in.parts[1], (double)num0in.parts[0]);
            } else if(num0in.parts[0] < 0)
            {
                num0in.polar[1] = (Decimal)Math.Atan2((double)num0in.parts[1], (double)num0in.parts[0]);
                
            }
        }
        public void PrintNumber(Complex num0in)
        {
            Console.WriteLine(num0in.parts[0] + " + " + num0in.parts[1] + "i");
            Console.WriteLine("r = " + num0in.polar[0] + " , theta = " + num0in.polar[1]);
        }
    }
}
