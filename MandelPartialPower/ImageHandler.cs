using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MandelPartialPower
{
    class ImageHandler
    {
        public Bitmap GetDataPicture(int w, int h, byte[] data)
        {
            /*
             * Found this code online, converts our byte data into a bitmap.
             */
            Bitmap pic = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int aria = 0;
            for (int x = 0; x < h; x++)
            {
                for (int y = 0; y < w; y++)
                {


                    Color c = Color.FromArgb(
                       data[aria],
                       data[aria + 1],
                       data[aria + 2],
                       data[aria + 3]
                    );
                    if (false)
                        Console.WriteLine("PIXEL " + y + " " + x + " ");
                    pic.SetPixel(y, x, c);
                    aria += 4;
                }
            }

            return pic;
        }

    }
}
