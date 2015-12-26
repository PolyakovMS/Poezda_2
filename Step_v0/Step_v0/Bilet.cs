using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
   public class Bilet
    {
        public string Nomer_poezda, Nomer_Vagona, Mesto_Nomer, Mesto_Bukva;

        public Bilet(string nomer_poezda, string nomer_vagona, string mesto_nomer, string mesto_bukva)
        {
            Nomer_poezda = nomer_poezda;
            Nomer_Vagona = nomer_vagona;
            Mesto_Nomer = mesto_nomer;
            Mesto_Bukva = mesto_bukva;
        }
        public void Vivod(ref DataGridView Vivod_bil, ref int i)
        {
            Vivod_bil.Rows.Add();
            Vivod_bil.Rows[0].Cells[0].Value = Nomer_poezda;
            Vivod_bil.Rows[0].Cells[1].Value = Nomer_Vagona;
            Vivod_bil.Rows[0].Cells[2].Value = Mesto_Nomer + " " + Mesto_Bukva;
            Vivod_bil.Visible = true;
        }
    }
}
