using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MandelPartialPower
{
    class FibGrapher
    {
        public Decimal nmax;
        public int height,width,cbyte,nytest;
        public Decimal deltax,deltay;
        public List<Decimal> yfixeds;
        public Decimal[] curpoint;
        public ImageHandler handle;
        public byte[,] coldata;
        public ComplexFibPoint cfp;
        public double[] doom;
        public string root;
        public void StartImage()
        {
            handle = new ImageHandler();
            width = (height * 16) / 9;
            deltax = nmax * 2 / (Decimal)width;
            deltay = -deltax;
            if(false)
            nytest = yfixeds.Count;
            // this is the data for the overall maps for real,imag,mag
            
            fibby = new ComplexFib();
            fibby.maxup = maxy;
            fibby.SetupComplexOp();
            
            doom = new double[3];
        }
        public ComplexFib fibby;
        public double maxy;
        public bool crossx, crossy;
        public void MakeImages0()
        {
            cbyte = 0;
            coldata = new byte[3, height * width * 4];
            crossy = false;
            curpoint = new Decimal[2] { -deltax * 2 / width, -deltay * 2 / height };
            for(int ii = 0; ii < height; ii++)
            {
                if(false)
                Console.WriteLine("II = " + ii + " out of " + height);
                crossx = false;
                for(int jj = 0; jj < width; jj++)
                {
                    Complex hold = new Complex();
                    hold.parts = new Decimal[2] {curpoint[0],curpoint[1] };
                    fibby.GetResult(hold);

                    for(int kk = 0;  kk < 4; kk++)
                    {
                        for(int ll = 0; ll < 3; ll++)
                        {
                            coldata[ll, kk + cbyte] = fibby.cfp.colmap[ll, kk];
                        }
                    }
                    cbyte += 4;
                    curpoint[0] += deltax;
                    if(!crossx && curpoint[0] >= 0)
                    {
                        curpoint[0] = 0;
                        crossx = true;
                    }
                }
                curpoint[1] += deltay;
                curpoint[0] = -deltax * 2 / width;
                if (!crossy && curpoint[1] <= 0)
                {
                    curpoint[1] = 0;
                    crossy = true;
                }
            }
            byte[] temp = new byte[height * width * 4];
            for(int ii = 0; ii < temp.Length; ii++)
            {
                temp[ii] = coldata[0, ii];
            }
            Bitmap bot = handle.GetDataPicture(width, height,temp );
            bot.Save(root + "\\fibby\\plots0\\realparts0.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            for (int ii = 0; ii < temp.Length; ii++)
            {
                temp[ii] = coldata[1, ii];
            }
            bot = handle.GetDataPicture(width, height, temp);
            bot.Save(root + "\\fibby\\plots0\\imaginaryparts0.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            for (int ii = 0; ii < temp.Length; ii++)
            {
                temp[ii] = coldata[1, ii];
            }
            bot = handle.GetDataPicture(width, height, temp);
            bot.Save(root + "\\fibby\\plots0\\magnitudes0.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }
    }
}
