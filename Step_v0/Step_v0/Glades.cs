using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    class Glades
    {
       
        int f = 0;
        string mesto_b;
        int mesto_n; int count = 0; public static int K = 0;
        Train t; Passanger pas; Bilet bil; Distanation dist;
        public static List<Train> trains = new List<Train>();
        public static List<Distanation> marshruti = new List<Distanation>();
        public static List<Passanger> passangers = new List<Passanger>();
        public static List<Bilet> bilets = new List<Bilet>();
        public List<Train> current_trains = new List<Train>();
        PictureBox pictureBoxs = new PictureBox();
        public static ComboBox c_b = new ComboBox();

        public void PoiskVsexPoezdov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer = "", type = "", id = "";
            int Hour, Min;
            foreach (XmlNode node in Root)
            {
                List<string> distance = new List<string>();
                List<DateTime> time = new List<DateTime>();
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        nomer = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "type")
                    {
                        type = childnode.InnerText;
                    }
                    if (childnode.Name == "distanation")
                    {
                        id = childnode.InnerText;
                    }
                    if (childnode.Name == "time")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {
                            string[] info1 = (info[i].Split(':'));
                            Hour = int.Parse(info1[0]);
                            Min = int.Parse(info1[1]);
                            DateTime dt = new DateTime();
                            dt = dt.AddHours(Hour);
                            dt = dt.AddMinutes(Min);
                            time.Add(dt);
                        }
                    }
                }
                t = new Train(nomer, type, id, time);
                trains.Add(t);
                distance = null;
                time = null;
            }
        }

        public static Stops Poiskostanovoki(string name_ostanovki)
        {
            Stops ost;
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Ostanovki.xml");
            XmlElement Root = Doc.DocumentElement;
            string name = ""; int cor_x = 0, cor_y = 0;
            foreach (XmlNode node in Root)
            {
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        name = attr.Value;
                }
                if (name_ostanovki == name)
                {
                    foreach (XmlNode childnode in node.ChildNodes)
                    {
                        if (childnode.Name == "info")
                        {
                            string[] info = childnode.InnerText.Split(' ');
                            cor_x = int.Parse(info[0]);
                            cor_y = int.Parse(info[1]);
                        }
                    }
                    return ost = new Stops(name, cor_x, cor_y);
                }
            }
            return ost = new Stops("null", 0, 0);
        }

        public void PoiskVsexmarshrutov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("C:\\stp\\Poezd\\Poezd-master\\28.11 ST&P\\Step_v0\\Step_v0\\Distanation.xml");
            XmlElement Root = Doc.DocumentElement;
            string start = "", end = "", id = "";
            foreach (XmlNode node in Root)
            {
                List<Stops> ostanovki = new List<Stops>();
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("id");
                    if (attr != null)
                        id = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "Start")
                    {
                        start = childnode.InnerText;
                    }
                    if (childnode.Name == "End")
                    {
                        end = childnode.InnerText;
                    }
                    if (childnode.Name == "stops")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {
                            ostanovki.Add(Poiskostanovoki(info[i]));
                        }
                    }
                }
                dist = new Distanation(id, start, end, ostanovki);
                marshruti.Add(dist);
                ostanovki = null;
            }
        }

        public void PoiskVsexPasagirov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Passanger.xml");
            XmlElement Root = Doc.DocumentElement;
            string n = "", s = "", nomer_poezda = "", nomer_vagona = "", mesto_nomer = "", mesto_bukva = "";
            foreach (XmlNode node in Root)
            {
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "info")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        n = info[1]; s = info[0]; nomer_poezda = info[2]; nomer_vagona = info[3]; mesto_nomer = info[4]; mesto_bukva = info[5];
                    }
                }
                bil = new Bilet(nomer_poezda, nomer_vagona, mesto_nomer, mesto_bukva);
                bilets.Add(bil);
                pas = new Passanger(n, s, nomer_poezda, bilets);
                passangers.Add(pas);
            }
        }

        public void CtlVivod(ref TextBox t1, ref TextBox t4, ref TextBox t3, ref TextBox t6, ref DataGridView Vivod, ref DataGridView vivod_pas, ref DataGridView vivod_bil, int f)
        {
            if (f == 3)
            {
                string str = c_b.Text; bool fn = false; Vivod.Rows.Add();
                if (str == "")
                {
                    fn = true;
                    for (int i = 0; i < trains.Count; i++)
                    {
                        for (int j = 0; j < marshruti.Count; j++)
                        {
                            if (trains[i].ID == marshruti[j].ID)
                            {
                                trains[i].last_ostanovka(marshruti[j]);
                                current_trains.Add(trains[i]);
                               
                               // Vivod(ref t4, ref t3, ref Vivod, ref count, ref marshruti[j].Start, ref marshruti[j].End);
                                count++;
                            }
                        }
                    }
                }
                if (fn == false)
                {
                    for (int i = 0; i < trains.Count; i++)
                    {
                        if (str == trains[i].Nomer)
                        {
                            for (int j = 0; j < marshruti.Count; j++)
                            {
                                if (trains[i].ID == marshruti[j].ID)
                                {
                                    trains[i].last_ostanovka(marshruti[j]);
                                    current_trains.Add(trains[i]);
                                //    trains[i].Vivod(ref t4, ref t3, ref Vivod, ref count, ref marshruti[j].Start, ref marshruti[j].End);
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            if (f == 2)
            {
                string str1 = t4.Text, str2 = t3.Text; bool fn = false; Vivod.Rows.Add();
                string str_ob = str1 + str2;
                if (str_ob == "")
                {
                    fn = true;
                    for (int i = 0; i < trains.Count; i++)
                    {
                        for (int j = 0; j < marshruti.Count; j++)
                        {
                            if (trains[i].ID == marshruti[j].ID)
                            {
                                trains[i].last_ostanovka(marshruti[j]);
                                current_trains.Add(trains[i]);
                                //trains[i].Vivod(ref t4, ref t3, ref Vivod, ref count, ref marshruti[j].Start, ref marshruti[j].End);
                                count++;
                            }
                        }
                    }
                }
                if (fn == false)
                {
                    for (int i = 0; i < marshruti.Count; i++)
                    {
                        if ((marshruti[i].Start + marshruti[i].End).Contains(str_ob))
                        {
                            for (int j = 0; j < trains.Count; j++)
                            {
                                if (trains[j].ID == marshruti[i].ID)
                                {
                                    trains[j].last_ostanovka(marshruti[i]);
                                    current_trains.Add(trains[j]);
                                  //  trains[j].Vivod(ref t4, ref t3, ref Vivod, ref count, ref marshruti[i].Start, ref marshruti[i].End);
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            if (f == 1)
            {
                string str1 = t1.Text; string str2 = t6.Text; bool fn = false; vivod_pas.Rows.Add(); vivod_bil.Rows.Add();
                string str_ob = str1 + str2;
                if (str_ob == "")
                {
                    fn = true;
                    for (int i = 0; i < passangers.Count; i++)
                    {
                        pas = passangers[i];
                        pas.Vivod(ref vivod_pas, ref count);
                        count++;
                    }
                }
                if (fn == false)
                {
                    for (int i = 0; i < passangers.Count; i++)
                    {
                        if ((passangers[i].Surname + passangers[i].Name).Contains(str_ob))
                        {
                            passangers[i].Vivod(ref vivod_pas, ref count);
                            count++;
                        }
                    }
                }
            }
        }

       public void VivodPas(ref DataGridView vp, ref DataGridView vb, ref PictureBox pb, ref Label l10, ref Label l11, ref Label l12, ref Label l13)
        {
            if (K > 0)
            {
                string curSurname = vp[0, vp.CurrentRow.Index].Value.ToString();
                string curName = vp[1, vp.CurrentRow.Index].Value.ToString();
                vb.Rows.Clear();
                string Fio = ""; Fio += curSurname + curName;
                for (int i = 0; i < passangers.Count; i++)
                {
                    if (Fio == (passangers[i].Surname + passangers[i].Name))
                    {
                        bilets[i].Vivod(ref vb, ref i);
                        mesto_n = int.Parse(bilets[i].Mesto_Nomer); mesto_b = bilets[i].Mesto_Bukva;
                        pb.Visible = true;
                        pb.BackColor = Color.Transparent;
                        l10.Visible = true; l11.Visible = true; l12.Visible = true; l13.Visible = true;
                        pictureBoxs.Size = new Size(22, 22);
                        pictureBoxs.Load("seat.jpg");
                        pictureBoxs.BackColor = Color.Transparent;
                        int X = Vagon.PoiskX(mesto_n, mesto_b);
                        int Y = Vagon.PoiskY(mesto_n, mesto_b);
                        pictureBoxs.Location = new Point(X, Y);
                        pb.Controls.Add(pictureBoxs);
                    }
                }
            }
        }
    }
}
