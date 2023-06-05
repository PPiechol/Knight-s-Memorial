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
    public partial class UserControlHighScores : UserControl
    {
        public UserControlHighScores()
        {
            InitializeComponent();
        }
        public UserControlHighScores(int counter, string name, int score)
        {
            InitializeComponent();
            this.labelName.Text = name;
            this.labelScore.Text = score.ToString();
            this.labelNumber.Text = counter.ToString();
            labelName.BackColor = Color.Transparent;
            labelScore.BackColor = Color.Transparent;
            labelNumber.BackColor = Color.Transparent;
        }
    }
}
