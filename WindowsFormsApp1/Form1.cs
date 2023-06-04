using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using NAudio.FileFormats.Mp3;
using NAudio.Utils;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using NAudio.Wave;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        Display Ekran_glowny;
        PictureBox background = new PictureBox();
        private List<Image> backgroundImages;
        int currentImageIndex = 0;
        Button exitButton;
        Button playButton;
        Button settingButton;
        Button editButton;
        Panel Settings;
        IWavePlayer wo = new WaveOutEvent();
        
        DirectSoundOut outputDevice = new DirectSoundOut();
        AudioFileReader intro = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\mainMenuIntro.mp3");


        public MainForm()
        {
            InitializeComponent();
            InitializeBackground();
        }

        public float Get_Volume_Level
        {
            get
            {
                return intro.Volume;
            }
            set
            {
                intro.Volume = value;
            }
        }

        private async void FadeIn()
        {
            while (Opacity < 1) // Pętla do momentu, gdy osiągniemy pełną widoczność
            {
                Opacity += 0.1; // Zwiększaj przezroczystość stopniowo
                await Task.Delay(150); // Poczekaj krótki czas przed kolejną iteracją
            }
            Opacity = 1; // Ustaw pełną widoczność na zakończenie animacji
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Opacity = 0; // Ustaw przezroczystość na 0 (niewidoczne)
            FadeIn();

            ShowIntroMenu();
            intro.Volume = 0.3f;
            outputDevice.Init(intro);
            outputDevice.Play();

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
            
            editButton = new Button();
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


            playButton = new Button();
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

            settingButton = new Button();
            settingButton.Text = "Opcje";
            settingButton.Width = buttonWidth;
            settingButton.Height = buttonHeight;
            settingButton.Location = new Point((Width - buttonWidth) / 2, playButton.Bounds.Bottom + buttonHeight / 2);
            settingButton.FlatStyle = FlatStyle.Flat;
            settingButton.BackColor = Color.FromArgb(245, 197, 1);
            settingButton.ForeColor = Color.White;
            settingButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            settingButton.FlatAppearance.BorderSize = 0;
            settingButton.Click += SwitchShownMenu;
            Controls.Add(settingButton);
            settingButton.BringToFront();

            exitButton = new Button();
            exitButton.Text = "Wyjście";
            exitButton.Width = buttonWidth;
            exitButton.Height = buttonHeight;
            exitButton.Location = new Point((Width - buttonWidth) / 2, settingButton.Bounds.Bottom + buttonHeight/2);
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.BackColor = Color.FromArgb(255, 55, 49);
            exitButton.ForeColor = Color.White;
            exitButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += ExitButton_Click;
            Controls.Add(exitButton);
            exitButton.BringToFront();

            
            Create_Setting_Window();

            timer1.Start();
        }

        private void SwitchShownMenu(object sender, EventArgs e)
        {
            foreach(Control Inside_Controls in this.Controls)
            {
                if(Inside_Controls is Panel || Inside_Controls is Button)
                    Inside_Controls.Visible = !Inside_Controls.Visible;
            }
        }

        private void Create_Setting_Window()
        {
            const int Panel_Heigth = 300;
            const int Panel_Width = 500;

            Settings = new Panel();
            Settings.Size = new Size(Panel_Width, Panel_Heigth);
            Settings.Location = new Point((Width - Panel_Width) / 2, (Height - Panel_Heigth) / 2);
            Settings.BackColor = Color.FromArgb(19, 133, 234);

            this.Controls.Add(Settings);
            Settings.BringToFront();

            System.Windows.Forms.TrackBar VolumeBar = new System.Windows.Forms.TrackBar();
            VolumeBar.Size = new Size((int)Math.Round(Settings.Width * 0.9), (int)Math.Round(Settings.Height * 0.3));
            VolumeBar.Location = new Point((Settings.Width - VolumeBar.Width) / 2, (int)Math.Round(Settings.Height * 0.55));
            VolumeBar.Minimum = 0;
            VolumeBar.Maximum = 200;
            VolumeBar.Value = 60;
            VolumeBar.TickStyle = TickStyle.None;
            VolumeBar.ValueChanged += VolumeBar_ValueChanged;
            Settings.Controls.Add(VolumeBar);

            Label LabelWindowName = new Label();
            LabelWindowName.Text = "Poziom głośności";
            LabelWindowName.ForeColor = Color.White;
            LabelWindowName.Font = new Font("MV Boli", 18, FontStyle.Bold);
            LabelWindowName.TextAlign = ContentAlignment.MiddleCenter;
            LabelWindowName.Size = new Size((int)Math.Round(Settings.Width * 0.5), (int)Math.Round(Settings.Height * 0.3));
            LabelWindowName.Location = new Point((Settings.Width - LabelWindowName.Width) / 2, (int)Math.Round(Settings.Height * 0.1));
            Settings.Controls.Add(LabelWindowName);

            Button ReturnButton = new Button();
            ReturnButton.Size = new Size((int)Math.Round(Settings.Width * 0.3), (int)Math.Round(Settings.Height * 0.16));
            ReturnButton.Location = new Point((Settings.Width - ReturnButton.Width) / 2, (int)Math.Round(Settings.Height * 0.8));
            ReturnButton.Text = "Powrót";
            ReturnButton.FlatStyle = FlatStyle.Flat;
            ReturnButton.BackColor = Color.FromArgb(10, 239, 141);
            ReturnButton.ForeColor = Color.White;
            ReturnButton.TextAlign = ContentAlignment.MiddleCenter;
            ReturnButton.Font = new Font("MV Boli", 18, FontStyle.Bold);
            ReturnButton.FlatAppearance.BorderSize = 0;
            ReturnButton.Click += SwitchShownMenu;
            Settings.Controls.Add(ReturnButton);

            Settings.Visible = false;
        }

        private void VolumeBar_ValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar TB = (sender as System.Windows.Forms.TrackBar);
            intro.Volume = (float)TB.Value / TB.Maximum;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            // Otwarcie Form2 w trybie edycji
            outputDevice.Stop();
            outputDevice.Dispose();
            Form2 form2 = new Form2(this);
            form2.Show();
            Hide();
        }
        
        
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            Ekran_glowny.Movement(e);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

            outputDevice.Stop();
            outputDevice.Dispose();
            Controls.Clear();
            // Rozpoczęcie gry
            
            Ekran_glowny = new Display(Width, Height, this);
            Ekran_glowny.KeyDown += KeyIsDown;
            Controls.Add(Ekran_glowny);
            Ekran_glowny.Load_Level(1, 0, 0);
            Ekran_glowny.BringToFront();
        }

        
    }
}
