using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MandelPartialPower
{
    class ComplexFibPoint
    {
        
        public Complex output;
        public Color colon;
        public double maxup;
        public byte[,] colmap;
        public double[] doom;
        public void Setup()
        {
            doom = new double[3];
            colmap = new byte[3,4];
            colon = new Color();
            rando = new System.Random();
        }
        public System.Random rando;
        public double GetMag()
        {
            return Math.Sqrt(Math.Pow((double)output.parts[0], 2) + Math.Pow((double)output.parts[1], 2));
        }
        public void CalculateColors()
        {
            if(false)
            Console.WriteLine(output.parts[0]);

            
            doom[0] = Math.Log10(Math.Abs((double)output.parts[0])) * 10.0;
           
            
            doom[2] = 1.0;
            if(doom[0] >= 360.0)
            {
                doom[0] = 360.0;
            } else if(doom[0] <= 0.0)
            {
                doom[0] = 0.0;
            }
            doom[1] = 1.0;
            if(doom[0] == 360.0)
            {
                doom[2] = 0.0;
            }
            colon = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
            colmap[0, 0] = 255;
            colmap[0, 1] = colon.R;
            colmap[0, 2] = colon.G;
            colmap[0, 3] = colon.B;
            if (rando.Next(0, 100000) == 0)
            {
                Console.WriteLine(colmap[0, 0] + " " + colmap[0, 1] + " " + colmap[0, 2] + " " + colmap[0, 3] + " " + doom[0] + " " + output.parts[0]);
            }
            doom[0] = Math.Log10(Math.Abs((double)output.parts[1])) * 10.0;

            if (doom[0] >= 360.0)
            {
                doom[0] = 360.0;
            }
            else if (doom[0] <= 0.0)
            {
                doom[0] = 0.0;
            }
            doom[1] = 1.0;
            if (doom[0] == 360.0)
            {
                doom[2] = 0.0;
            }
            colon = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
            colmap[1, 0] = 255;
            colmap[1, 1] = colon.R;
            colmap[1, 2] = colon.G;
            colmap[1, 3] = colon.B;
            doom[0] = 360.0 * (Math.Log10((double)GetMag())) / 200.0;

            if (doom[0] >= 360.0)
            {
                doom[0] = 360.0;
            }
            else if (doom[0] <= 0.0)
            {
                doom[0] = 0.0;
            }
            doom[1] = 1.0;
            if (doom[0] == 360.0)
            {
                doom[2] = 0.0;
            }
            colon = ColorUtils.HsvToRgb(doom[0], doom[1], doom[2]);
            colmap[2, 0] = 255;
            colmap[2, 1] = colon.R;
            colmap[2, 2] = colon.G;
            colmap[2, 3] = colon.B;
        }
    }
}
