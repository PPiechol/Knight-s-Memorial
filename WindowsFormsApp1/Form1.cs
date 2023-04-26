using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        Display Ekran_glowny;
        
        public MainForm()
        {
            InitializeComponent();

        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
           Ekran_glowny.Movement(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*Form2 temp = new Form2();
            temp.Show();*/
            Ekran_glowny = new Display(this.Width, this.Height, this);
            this.Controls.Add(Ekran_glowny);
            Ekran_glowny.Load_Level(1, 0, 0);

        }
    }
}
