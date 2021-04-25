using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class ComplexFib
    {
        public Complex z0, z1, z2, z3, z4;
        public Decimal phi;
        public double maxup;
        public ComplexOp cop;
        public ComplexPower comp;
        public ComplexFibPoint cfp;
        public void SetupComplexOp()
        {
            phi = (1 + (Decimal)Math.Sqrt(5)) / 2;
            comp = new ComplexPower();
            comp.StartUp();
            cop = new ComplexOp();
            // Binet Formula: ((phi^n) - (-1/phi)^n)/Sqrt(5)
            z0 = new Complex();
            z0.parts = new Decimal[2] { phi,0};
            z1 = new Complex();
            z1.parts = new Decimal[2] { -1 / phi, 0 };
            z4 = new Complex();
            z4.parts = new Decimal[2] { 1 / (Decimal)Math.Sqrt(5), 0 };
            cfp = new ComplexFibPoint();
            cfp.maxup = maxup;
            cfp.Setup();
        }
        public void GetResult(Complex valin)
        {
            // Implements binet formula
            cop.FindPolar(valin);
            z2 = comp.TakeComplexPower(z0, valin);
            z3 = comp.TakeComplexPower(z1, valin);
            z3.parts[0] = -z3.parts[0];
            z3.parts[1] = -z3.parts[1];
            Complex cono = new Complex();
            cono.parts = new Decimal[2] { 0,0};
            cono = cop.Add(z2, z3);
            cono = cop.Multiply(cono, z4);
            if(false)
            Console.WriteLine(valin.parts[0] + " " + valin.parts[1] + " " + cono.parts[0] + " " + cono.parts[1]);
            cfp.output = cono;
            cfp.CalculateColors();

        }
        public byte[,] GetColors()
        {
            return cfp.colmap;
        }
    }
}
