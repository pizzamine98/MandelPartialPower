using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class GraphSettings
    {
        public int height, width,totalpixels,zoomnum;
        public Polynomial poly;
        public Complex centerpoint,startpoint;
        public Decimal upy,deltax,deltay,zoomeach;
        /*
         * upy is the distance between the center point and the top of the screen. We can use this to calculate everything else.
         * 
         */
        public void Calculate()
        {
            width = (height * 16) / 9;
            totalpixels = height * width;
            deltay = -(upy * 2) / height;
            deltax = -deltay;
            startpoint = new Complex();
            startpoint.parts = new Decimal[2] { centerpoint.parts[0] - (deltax * width / 2),
            centerpoint.parts[1] - (deltay * height/2)};
           

        }
        public void Calculate2()
        {
            width = (height * 16) / 9;
            totalpixels = height * width;
            upy = -(deltay * height) / 2;
            startpoint = new Complex();
            startpoint.parts = new Decimal[2] { centerpoint.parts[0] - (deltax * width / 2),
            centerpoint.parts[1] - (deltay * height/2)};
            ComplexOp cop = new ComplexOp();
            cop.StartUp();

            if (false)
            {
                Console.WriteLine("STARTPOINT: " + cop.MakeComplexString(startpoint, false, 6));
                Console.WriteLine("UPY: " + upy + " DELTAY = " + deltay);
            }
        }

    }
}
