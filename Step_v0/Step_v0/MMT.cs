using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class MMT : Form
    {
        Train testTR;
        List<Train> list_poezdov;
        List<Stops> list_ostanovok;
        List<Distanation> marshruts;
        Distanation marsh;
        List<TrainControl> list_ctrl_train = new List<TrainControl>();

        public MMT(List<Train> trains, List<Distanation> marshrut)
        {
            list_poezdov = trains;
            marshruts = marshrut;
            //list_ostanovok = marshrut.os;
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02d) == 1) timer.Stop();
            });
            timer.Interval = 3;
            timer.Start();
            // dataGridView1.AllowUserToAddRows = false;

        }

        private void MMT_Load(object sender, EventArgs e)
        {
            create_ctrl();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrainControl tr1;
            for (int i = 0; i < list_ctrl_train.Count; i++)
            {
                tr1 = list_ctrl_train[i];
                tr1.Location = new Point(20, 20);
                tr1.ctrl_x = 20;
                tr1.ctrl_y = 20;
            }



        }
        public void create_ctrl()
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            int count = 0;
            int i = 0, q = 0;
            while (flag1)
            {

                //count = 0;
                //  for (int count = 0; count < list_ostanovok.Count; count++)
                while (flag2)
                {

                    while (flag3)
                    {
                        if (list_poezdov[i].ID == marshruts[q].ID)
                        {
                            list_ostanovok = marshruts[q].Ostanovki;
                            flag3 = false;
                            q = 0;

                        }
                        else
                        {
                            q++;
                        }
                    }


                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        if (list_ctrl_train.Count == 0)
                        {
                            TrainControl tr = new TrainControl(list_ostanovok[count].X - 10, list_ostanovok[count].Y - 22);
                            pictureBox1.Controls.Add(tr);
                            tr.coll_train.Add(list_poezdov[i]);
                            i++;
                            flag2 = false;
                            list_ctrl_train.Add(tr);

                            tr.Click += new System.EventHandler(tr_Click);
                            tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                            tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                        }
                        else
                        {
                            for (int k = 0; k < list_ctrl_train.Count; k++)
                            {
                                TrainControl test_tr;
                                test_tr = list_ctrl_train[k];
                                if ((test_tr.ctrl_x == (list_ostanovok[count].X - 10)) & (test_tr.ctrl_y == (list_ostanovok[count].Y - 22)))
                                {
                                    test_tr.coll_train.Add(list_poezdov[i]);
                                    i++;
                                    flag2 = false;

                                }
                            }

                            if (flag2 == true)
                            {
                                TrainControl tr = new TrainControl(list_ostanovok[count].X - 10, list_ostanovok[count].Y - 22);
                                tr.Click += new System.EventHandler(tr_Click);
                                tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                                tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                                pictureBox1.Controls.Add(tr);
                                tr.coll_train.Add(list_poezdov[i]);
                                i++;
                                flag2 = false;
                                list_ctrl_train.Add(tr);

                            }

                        }

                    }
                    else
                    {
                        count++;
                    }

                }
                //count++;
                if (i == list_poezdov.Count)
                {
                    flag1 = false;
                    flag2 = false;
                    flag3 = false;
                }
                else
                {
                    flag2 = true;
                    flag3 = true;
                    count = 0;
                }

            }

        }

        private void tr_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            TrainControl poezd = (TrainControl)sender;
            // poezd.Location = new Point(20, 20);
            //  poezd.ctrl_x = 20;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
                i--;
            }


            //dataGridView1.Rows.Add();
            listBox1.Items.Clear();
            for (int i = 0; i < poezd.coll_train.Count; i++)
            {
                dataGridView1.Rows.Add();
                if (poezd.coll_train[i].motion)
                {

                    dataGridView1.Visible = true;

                    // listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "В пути");

                    //  dataGridView1.Size = new Size(319, 58 * i);
                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "В пути";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                    // dataGridView1.AllowUserToAddRows = false;

                }
                else
                {
                    dataGridView1.Visible = true;
                    //   dataGridView1.Size = new Size(319, 58 * i);
                    //listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "Стоит");


                    dataGridView1.Rows[i].Cells[0].Value = poezd.coll_train[i].Nomer;
                    dataGridView1.Rows[i].Cells[1].Value = "Cтоит";
                    dataGridView1.Rows[i].Cells[2].Value = poezd.coll_train[i].last_stops;
                    //  dataGridView1.AllowUserToAddRows = false;

                }
            }

            dataGridView1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);
            // dataGridView1.AllowUserToAddRows = false;
            //   dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);


            // listBox1.Size = new Size(115, 75);
            //  listBox1.Visible = true;
            // listBox1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);


        }


        private void tr_MouseEnter(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(30, 30);
        }
        private void tr_MouseLeave(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(21, 23);
        }



        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // button1.Size = new Size(20, 20);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Size = new Size(20, 20);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Size = new Size(75, 23);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            MessageBox.Show("ddd");
        }
        public void poisk_procent()
        {
            Train tr;
            for (int i = 0; i < list_poezdov.Count; i++)
            {
                tr = list_poezdov[i];


            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listBox1.Items.Clear();
            for (int i2 = 0; i2 < dataGridView2.RowCount; i2++)
            {


                dataGridView2.Rows.RemoveAt(i2);
                i2--;

            }

            //dataGridView1.AllowUserToAddRows = false;
            listBox1.Visible = true;
            string s = "";
            string id = "";
            int d = 427;
            int i = 0, count = 0;
            bool f = false, f2 = false;
            Distanation marh;
            DateTime dt2 = new DateTime();
            DateTime realtime = new DateTime();
            int hour, min;
            hour = int.Parse(DateTime.Now.ToString("HH"));
            min = int.Parse(DateTime.Now.ToString("mm"));
            realtime = realtime.AddHours(hour);
            realtime = realtime.AddMinutes(min);


            // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //  if(dataGridView1.Rows[].Cells[0] == "427")
            //   {

            // }
            int index = dataGridView1.CurrentCell.RowIndex;
            string number = "";
            if (dataGridView1.Rows[index].Cells[0].Value != null)
            {
                number = dataGridView1.Rows[index].Cells[0].Value.ToString();
                f = true;
            }


            while (f)
            {
                if (number == list_poezdov[i].Nomer)
                {
                    id = list_poezdov[i].ID;

                    f = false;
                    f2 = true;

                }
                else
                {
                    i++;
                }
            }


            while (f2)
            {
                if (id == marshruts[count].ID)
                {
                    //  for (int k = 0; k < marshruts[count].Ostanovki.Count; k++)
                    //  {
                    // listBox1.Items.Add(marshruts[count].Ostanovki[k].Name);

                    for (int dddd = 0; dddd < marshruts[count].Ostanovki.Count; dddd++)
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[dddd].Cells[0].Value = marshruts[count].Ostanovki[dddd].Name;
                        s = list_poezdov[i].Time[dddd].ToString("HH:mm");
                        if (realtime >= list_poezdov[i].Time[dddd])
                        {
                            dataGridView2.Rows[dddd].Cells[1].Value = s;
                            dataGridView2.Rows[dddd].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {

                            dataGridView2.Rows[dddd].Cells[1].Value = s;
                            dataGridView2.Rows[dddd].DefaultCellStyle.BackColor = Color.Green;
                            // dataGridView2.DefaultCellStyle.BackColor = Color.Green;
                        }
                        f2 = false;
                        f = false;
                    }



                    // }
                }
                else
                {
                    count++;

                }
            }

            dataGridView1.Rows[index].Selected = true;
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.Red;

            // listBox1.Items.Add(index);
        }
    }


}
