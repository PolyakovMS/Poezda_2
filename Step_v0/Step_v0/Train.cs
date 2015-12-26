using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    public class Train
    {
       public string Nomer, Type,ID;
        public List<DateTime> Time;
        public string last_stops;
        public  bool motion;

        public Train(string n, string t, string id, List<DateTime> Vreme_ostanovkok) {
            Nomer = n;
            Type = t;
            ID = id;
            Time = Vreme_ostanovkok;
        }


        

        public void last_ostanovka(Distanation marshrut)
        {
            int count=0,i = 0;
            int hour, min;
            Boolean flag = true;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DateTime realtime = new DateTime();
            hour = int.Parse(DateTime.Now.ToString("HH"));
            min = int.Parse(DateTime.Now.ToString("mm"));
           realtime = realtime.AddHours(hour);
           realtime= realtime.AddMinutes(min);

            if (realtime <= Time[0])//поезl не выехал
            {
                motion = false;
                last_stops = marshrut.Start;
                flag = false;
                count=0;
            }
            if (realtime >= Time[Time.Count - 1])//поезд приехал
            {
                last_stops =marshrut.End;
                flag = false;
                count=marshrut.Ostanovki.Count-1;
                motion = false;
            }


            //for (int i = 0; i < Time.Count; i++)
            while(flag)
            {
                dt1 = Time[i];
                dt2 = Time[i + 1];
                if((realtime>dt1)&(realtime>=dt2))
                {
                    i++;
                    count++;
                }
                if((realtime>=dt1)&(realtime<dt2))
                {
                    if (realtime == dt1)
                        motion = false;
                    else
                        motion = true;

                    last_stops = marshrut.Ostanovki[count].Name;
                    flag = false;
                }
               
            }
        }   

    }
}

