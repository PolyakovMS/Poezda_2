using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class TrainControl : UserControl
    {

       public int ctrl_x=-1, ctrl_y=-1;
      public  List<Train>coll_train = new List<Train>();
       
        
        public TrainControl(int x, int y)
        {
            ctrl_x = x;
            ctrl_y = y;
            Location = new Point(ctrl_x, ctrl_y);
         //   Size = new Size(30, 30);
            Visible = true;
            
            InitializeComponent();
        }
       

    }
}
