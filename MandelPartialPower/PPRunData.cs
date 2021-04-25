using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class PPRunData
    {
        public int runid,w,h,nsteps,nround;
        public int maxitts;
        public int[] nblacks;
        public DateTime starttime, endtime;
        public TimeSpan[] timestaken,timesperpixel;
        public TimeSpan timetaken, timeperpixel;
        public Complex[] powers;
        public Polynomial basepoly;
        
    }
}
