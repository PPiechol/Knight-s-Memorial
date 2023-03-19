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
            Ekran_glowny = new Display(this.Width, this.Height);
            this.Controls.Add(Ekran_glowny);
            Ekran_glowny.Load_Level(1, 0, 0);
        }
    }
}
