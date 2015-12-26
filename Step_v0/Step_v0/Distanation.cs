using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step_v0
{
   public class Distanation
    {
        public string ID,Start,End;
        public List<Stops> Ostanovki = new List<Stops>();

        public Distanation(string id, string start, string end, List<Stops> ostanovki)
        {
            ID = id;
            Start = start;
            End = end;
            Ostanovki = ostanovki;
        }
        /*public void Vivod(ref DataGridView Vivod_bil, ref int i)
        {
            //Vivod_bil.Rows.Add();
            Vivod_bil.Rows[0].Cells[0].Value = Nomer_poezda;
            Vivod_bil.Rows[0].Cells[1].Value = Nomer_Vagona;
            Vivod_bil.Rows[0].Cells[2].Value = Mesto_Nomer + " " + Mesto_Bukva;
            Vivod_bil.Visible = true;
        }*/
    }
}
