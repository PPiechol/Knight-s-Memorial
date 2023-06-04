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
    public partial class FormNickname : Form
    {
        private HighScore highScore;
        public FormNickname()
        {
            InitializeComponent();
            highScore = new HighScore();
        }

        public HighScore HighScore { get => highScore; set => highScore = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            highScore.name = textBox1.Text;
        }
    }
}
