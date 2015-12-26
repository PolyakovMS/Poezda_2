using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
namespace Step_v0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 
        int f = 0;
        string mesto_b;
        int mesto_n;int count = 0;public static int K = 0;
        Train t; Passanger pas;Bilet bil;Distanation dist;
        public static List<Train> trains = new List<Train>();
        public static List<Distanation> marshruti = new List<Distanation>();
        public static List<Passanger> passangers = new List<Passanger>();
        public static List<Bilet> bilets = new List<Bilet>();
        public List<Train> current_trains = new List<Train>();
        PictureBox pictureBoxs = new PictureBox();
        public static ComboBox c_b = new ComboBox();
        public string Nomer, Type, ID;
        public List<DateTime> Time;
        public string last_stops;
        public bool motion;


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                f = 1;
            textBox1.Enabled = true; textBox6.Enabled = true; textBox3.Enabled = false; textBox4.Enabled = false; c_b.Enabled = false;;
            Vivod_ostanovok.Items.Clear();
            label1.ForeColor = Color.Gold;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            Vivod.Visible = false;count = 0;K = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                f = 2;
            textBox3.Enabled = true; textBox4.Enabled = true; textBox1.Enabled = false; textBox6.Enabled = false; c_b.Enabled = false;
            Vivod_ostanovok.Items.Clear();
            label5.ForeColor = Color.Gold;
            label1.ForeColor = Color.White;
            label6.ForeColor = Color.White;K = 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                f = 3;
            c_b.Enabled = true; textBox3.Enabled = false; textBox4.Enabled = false; textBox1.Enabled = false; textBox6.Enabled = false;
            Vivod_ostanovok.Items.Clear();
            label6.ForeColor = Color.Gold;
            label1.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            vivod_pas.Visible = false;count = 0;K = 0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)) || (Char.IsSeparator(e.KeyChar)))
                e.Handled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
            ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
        }

        

        Glades glades = new Glades();
        void Function()
        {
            glades.PoiskVsexPoezdov();
            glades.PoiskVsexmarshrutov();
            glades.PoiskVsexPasagirov();
            
        }

        private void Receive_data1_Click(object sender, EventArgs e)
        {
            glades.CtlVivod(ref  textBox1, ref textBox4, ref  textBox3, ref  textBox6, ref  Vivod, ref  vivod_pas, ref  vivod_bil, f);
            Vivod1();
        }

         private void Clean_Click(object sender, EventArgs e)
         {
             Vivod_ostanovok.Items.Clear();
             Vivod_ostanovok.Visible = false;
             textBox1.Clear(); c_b.Text=""; textBox4.Clear(); textBox6.Clear(); textBox3.Clear();
             label10.Visible = false; label11.Visible = false; label12.Visible = false; label13.Visible = false;
             pictureBoxs.Image = null;
             pictureBox.Visible = false;
             pictureBox.Refresh();
             current_trains.Clear();count = 0;
             Vivod.Rows.Clear();vivod_pas.Rows.Clear();vivod_bil.Visible = false;K = 0;
         }

         private void MMT_Click(object sender, EventArgs e)
         {
             MMT MMT_Form = new MMT(current_trains,marshruti);
             MMT_Form.ShowDialog();
            
         }

         private void Editor_Click(object sender, EventArgs e)
         {
             Form2 secondForm = new Form2(trains);
             secondForm.ShowDialog();
         }

         private void Form1_Load(object sender, EventArgs e)
         {
             glades.PoiskVsexmarshrutov();
             glades.PoiskVsexPoezdov(); 
             glades.PoiskVsexPasagirov();         
             c_b.Location= new Point(801,73);c_b.Enabled = false;
             this.Controls.Add(c_b);
             for (int i=0;i<trains.Count;i++)
             c_b.Items.Add(trains[i].Nomer);
         }


         private void Vivod_SelectionChanged(object sender, EventArgs e)
         {         
            /* string curItem = Vivod[1, Vivod.CurrentRow.Index].Value.ToString();
             Vivod_ostanovok.Items.Clear();
             if ((f == 3) || (f == 2))
             {
                 for (int i = 0; i < trains.Count; i++)
                 {
                     if (curItem == trains[i].Nomer)
                     {
                         for (int j = 0; j < trains[i]..Count; j++)
                             Vivod_ostanovok.Items.Add(trains[i].Distance[j]);
                     }
                 }
             Vivod_ostanovok.Visible = true;
            }*/

           
        }

        private void vivod_pas_SelectionChanged(object sender, EventArgs e)
        {
            glades.VivodPas(ref vivod_pas, ref vivod_bil, ref pictureBox, ref label10, ref label11, ref label12, ref label13);
           
        }




        public void Vivod1() {
            if (Form1.K > 0)
            {
                Vivod.Rows.Add();
            }
            Form1.K++;
            Vivod.Visible = true;
            for (int g = 0; g< glades.current_trains.Count ; g++)
            {
                
                Vivod.Rows[g].Cells[0].Value =glades.current_trains[g].last_stops;
                Vivod.Rows[g].Cells[1].Value = glades.current_trains[g].Nomer;
                Vivod.Rows[g].Cells[2].Value = glades.current_trains[g].Type;
                Vivod.Rows.Add();
                //  grid_vivod.Rows[i].Cells[3].Value = trains[i].start;
                //  grid_vivod.Rows[i].Cells[4].Value = trains[i].stop;
                //  grid_vivod.Rows[i].Cells[5].Value = Time[0].ToString("HH:mm");
                // grid_vivod.Rows[i].Cells[6].Value = Time[Time.Count - 1].ToString("HH:mm");

            }      
        }


    }
}
