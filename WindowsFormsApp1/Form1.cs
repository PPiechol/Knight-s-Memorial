using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Display Ekran_glowny;
        public Form1()
        {
            InitializeComponent();

            Ekran_glowny = new Display(1, 0, 0);
            this.Controls.Add(Ekran_glowny);
            //Display.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + 1.ToString() + "\\Map " + 0.ToString() + "-" + 1.ToString() + ".png");
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            Ekran_glowny.Movement(e);
        }
    }
}
