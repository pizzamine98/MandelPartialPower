using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class RunData
    {
        public TimeSpan timetaken;
        public TimeSpan timeperpoint;
        public GraphSettings gsettings;
        public Decimal maxto;
        public int nblack,maxitts,whichmethod;
    }
}
