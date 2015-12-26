using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step_v0
{
    class Vagon
    {
        public static int PoiskX(int mesto_nomer, string mesto_bukva)
        {
            int x, s = 0;
            if (mesto_bukva == "A")
            {
                x = 8;
                return x;
            }
            if (mesto_bukva == "B")
            {
                x = 30;
                return x;
            }
            if (mesto_bukva == "C")
            {
                x = 56;
                return x;
            }
            if (mesto_bukva == "D")
            {
                x = 78;
                return x;
            }
            return s;
        }
        public static int PoiskY(int mesto_nomer, string mesto_bukva)
        {
            int y, s = 0;
            if (mesto_bukva == "A")
            {
                y = 8 + 29 * (mesto_nomer - 1);
                return y;
            }
            if (mesto_bukva == "B")
            {
                y = 8 + 29 * (mesto_nomer - 1); ;
                return y;
            }
            if (mesto_bukva == "C")
            {
                y = 8 + 29 * (mesto_nomer - 1); ;
                return y;
            }
            if (mesto_bukva == "D")
            {
                y = 8 + 29 * (mesto_nomer - 1); ;
                return y;
            }
            return s;
        }

    }
}
