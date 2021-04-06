using System;
using System.Collections.Generic;
using System.Text;

namespace MandelPartialPower
{
    class PolyTerm
    {
        public Complex coefficient;
        public int[] zpow, cpow;
        public string GetFromTerm()
        {
            return coefficient.parts[0] + "," + coefficient.parts[1] + "," + zpow[0] + "," + zpow[1] + "," + cpow[0] + "," + cpow[1];

        }
        public void GetFromString(string termstrin)
        {
            string[] party = termstrin.Split(",");
            coefficient = new Complex();
            coefficient.parts = new Decimal[2] { Decimal.Parse(party[0]), Decimal.Parse(party[1]) };
            zpow = new int[2] { int.Parse(party[2]), int.Parse(party[3])};
            cpow = new int[2] { int.Parse(party[4]), int.Parse(party[5]) };

        }
    }
}
