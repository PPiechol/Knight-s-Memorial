using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        Display Ekran_glowny;
        PictureBox background = new PictureBox();
        private List<Image> backgroundImages;
        int currentImageIndex = 0;
        public MainForm()
        {
            InitializeComponent();
            InitializeBackground();
        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
           Ekran_glowny.Movement(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowIntroMenu();
        }


        private void InitializeBackground()
        {
            backgroundImages = new List<Image>();
            // Dodaj tutaj ścieżki do animowanych obrazków
            backgroundImages.Add(Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\pic1.png"));
            backgroundImages.Add(Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\pic2.png"));
            backgroundImages.Add(Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\pic3.png"));

            
            background = new PictureBox();
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            background.Dock = DockStyle.Fill;
            Controls.Add(background);

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangeBackgroundImage();
        }

        private void ChangeBackgroundImage()
        {
            background.Image = backgroundImages[currentImageIndex];
            currentImageIndex = (currentImageIndex + 1) % backgroundImages.Count;
        }



        private void ShowIntroMenu()
        {

            ChangeBackgroundImage();
            int buttonWidth = 150;
            int buttonHeight = 40;
            
            Button editButton = new Button();
            editButton.Text = "Edytuj";
            editButton.Width = buttonWidth;
            editButton.Height = buttonHeight;
            editButton.Location = new Point((Width - buttonWidth) / 2, (Height - buttonHeight) / 2 - 20);
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.BackColor = Color.FromArgb(52, 152, 219);
            editButton.ForeColor = Color.White;
            editButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            editButton.FlatAppearance.BorderSize = 0;
            editButton.Click += EditButton_Click;
            Controls.Add(editButton);
            editButton.BringToFront();


            Button playButton = new Button();
            playButton.Text = "Graj";
            playButton.Width = buttonWidth;
            playButton.Height = buttonHeight;
            playButton.Location = new Point((Width - buttonWidth) / 2, (Height - buttonHeight) / 2 + editButton.Height);
            playButton.FlatStyle = FlatStyle.Flat;
            playButton.BackColor = Color.FromArgb(46, 204, 113);
            playButton.ForeColor = Color.White;
            playButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            playButton.FlatAppearance.BorderSize = 0;
            playButton.Click += PlayButton_Click;
            Controls.Add(playButton);
            playButton.BringToFront();

            Button exitButton = new Button();
            exitButton.Text = "Wyjście";
            exitButton.Width = buttonWidth;
            exitButton.Height = buttonHeight;
            exitButton.Location = new Point((Width - buttonWidth) / 2, playButton.Bounds.Bottom + buttonHeight/2);
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.BackColor = Color.FromArgb(255, 55, 49);
            exitButton.ForeColor = Color.White;
            exitButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += ExitButton_Click;
            Controls.Add(exitButton);
            exitButton.BringToFront();

            

            timer1.Start();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            // Otwarcie Form2 w trybie edycji
            Form2 form2 = new Form2(this);
            form2.Show();
            Hide();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            // Rozpoczęcie gry
            Ekran_glowny = new Display(Width, Height, this);
            Controls.Add(Ekran_glowny);
            Ekran_glowny.Load_Level(1, 0, 0);
        }

        
    }
}
