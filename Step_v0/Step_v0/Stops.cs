using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Step_v0
{
   public class Stops
    {
        public string Name;
        public int X, Y;

        public Stops(string n, int x, int y)
        {
            Name = n;
            X = x;
            Y = y;           
        }

        public void Vivod(ref ListBox vivod)
        {
            List<string> collection = new List<string>();
            bool flag = true;
                vivod.Items.Add(Name);
                if (flag == true)
                {

                }
                vivod.Visible = true;
            
        }
    }
}
