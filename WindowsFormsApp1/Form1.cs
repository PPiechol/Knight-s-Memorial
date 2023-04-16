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
        PictureBox pictureBox1;


        public MainForm()
        {
            InitializeComponent();

            Panel panelLeft = new Panel();
            panelLeft.Location = new Point(0, 0);
            panelLeft.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 5, 250);
            panelLeft.Visible = true;
            
            this.Controls.Add(panelLeft);
            pictureBox1 = new PictureBox();
            Player player = new Player(pictureBox1, 1, 35, 8, 10, 1, panelLeft);

        }




        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            Ekran_glowny.Movement(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Ekran_glowny = new Display(this.Width, this.Height, this);
            this.Controls.Add(Ekran_glowny);
            Ekran_glowny.Load_Level(1, 0, 0);
        }
    }
}
