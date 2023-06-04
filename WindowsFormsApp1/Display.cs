using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Utils;
using NAudio.FileFormats.Mp3;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using NAudio.Wave;
using NAudio.Gui;
using TrackBar = System.Windows.Forms.TrackBar;
using ProgressBar = System.Windows.Forms.ProgressBar;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using NAudio.Wave.SampleProviders;
using System.IO;

namespace WindowsFormsApp1
{

    class Display : Panel
    {
        PictureBox Game_Board;
        Player player;
        Label continueButton;
        Label exitButton;
        Label menu;
        Label menuText;
        public PictureBox Character;
        Bitmap Level_Hitbox;
        List<Interactive_Object> Objects;
        private const int Character_size = 50;
        private int speed = 5;
        private int enemySpeed = 7;
        int Display_Size;
        int MapPositionX;
        int MapPositionY;
        int Current_Level;
        public bool battle;
        Panel panelRight;
        TableLayoutPanel panelBottom;
        Panel panelMenu;
        Panel menuRight;
        Battle activeBattle;
        MainForm Source;
        List<int> Inventory;
        Timer timer = new Timer();
        int labelSize = 50;
        int spacing = 10;
        int enemyCount = 0;
        EquipmentDataContext Edc = new EquipmentDataContext(); //baza przedmiotów
        HighscoreDataContext hsdb = new HighscoreDataContext(); //baza wyników
        List<string> battleMusicThemes = new List<string>();
        

        //muzyka
        WaveOutEvent wo = new WaveOutEvent();
        AudioFileReader MainMusic = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\mainMusic.mp3");
        
        
        WaveOutEvent wo2 = new WaveOutEvent();
        AudioFileReader fight = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\battle1.mp3");
        

        WaveOutEvent wo3 = new WaveOutEvent();
        AudioFileReader messageSound = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\message.mp3");
        
        WaveOutEvent wo4 = new WaveOutEvent();
        AudioFileReader coinPicking = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\coinPicking.mp3");

        WaveOutEvent wo5 = new WaveOutEvent();
        AudioFileReader weaponPicking = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\weaponPicking.mp3");

        WaveOutEvent wo6 = new WaveOutEvent();
        AudioFileReader buttonClicking = new AudioFileReader(Environment.CurrentDirectory + "\\Sounds\\buttonsSound.mp3");

        
        

        public void PanelMenu()
        {
            

            menu = new System.Windows.Forms.Label();
            menu.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 50, 0);
            menu.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\menu.png");
            menu.Size = new Size(50, 50);
            menu.BackColor = Color.Gray;
            menu.Click += menuButton_Click;
            this.Controls.Add(menu);

            panelMenu = new Panel();
            panelMenu.Visible = false;
            panelMenu.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 5, 300);
            panelMenu.Location = new Point((Screen.PrimaryScreen.Bounds.Width - panelMenu.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - panelMenu.Height) / 2);
            panelMenu.BackColor = Color.Gray;
            this.Controls.Add(panelMenu);

            menuText = new System.Windows.Forms.Label();
            menuText.Text = "Menu";
            menuText.Visible = true;
            menuText.BackColor = Color.White;
            menuText.Padding = new Padding(5);
            menuText.Size = new Size(200, 50);
            menuText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            menuText.Font = new Font("Arial", 16, FontStyle.Bold);
            menuText.BackColor = Color.Transparent;
            menuText.Location = new Point((panelMenu.Width - menuText.Width) / 2, (panelMenu.Height / 20));
            panelMenu.Controls.Add(menuText);

            continueButton = new System.Windows.Forms.Label();
            continueButton.Text = "Powrót do gry";
            continueButton.Visible = true;
            continueButton.BackColor = Color.White;
            continueButton.Padding = new Padding(5);
            continueButton.Size = new Size(200, 50);
            continueButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            continueButton.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, continueButton.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            };
            continueButton.Font = new Font("Arial", 16, FontStyle.Bold);

            continueButton.Location = new Point((panelMenu.Width - menuText.Width) / 2, menuText.Location.Y + continueButton.Height * 6/5);
            panelMenu.Controls.Add(continueButton);


            exitButton = new System.Windows.Forms.Label();
            exitButton.Text = "Wyjdź";
            exitButton.Visible = true;
            exitButton.BackColor = Color.White; // Set background color to white
            exitButton.Padding = new Padding(5);
            exitButton.Size = new Size(200, 50);
            exitButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            exitButton.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, exitButton.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            };
            exitButton.Font = new Font("Arial", 16, FontStyle.Bold);

            exitButton.Location = new Point((panelMenu.Width - exitButton.Width) / 2, continueButton.Location.Y + exitButton.Height*6/5);
            panelMenu.Controls.Add(exitButton);

            TrackBar volumeSlider = new System.Windows.Forms.TrackBar();
            volumeSlider.Visible = true;
            volumeSlider.BackColor = Color.White;
            volumeSlider.Padding = new Padding(5);
            volumeSlider.Size = new Size(200, 20);
            volumeSlider.Location = new Point((panelMenu.Width - menuText.Width) / 2, exitButton.Location.Y + volumeSlider.Height*6/5);
            volumeSlider.Minimum = 0;
            volumeSlider.Maximum = 200;
            volumeSlider.SmallChange = 1;
            volumeSlider.LargeChange = 1;
            volumeSlider.BackColor = Color.White;
            volumeSlider.GotFocus += VolumeSlider_GotFocus;
            panelMenu.Controls.Add(volumeSlider);

            volumeSlider.ValueChanged += VolumeSlider_ValueChanged;
            volumeSlider.Value = (int)(volumeSlider.Maximum * Source.Get_Volume_Level / 4);
            VolumeSlider_ValueChanged(volumeSlider, null);

            continueButton.Click += ContinueButton_Click;
            exitButton.Click += ExitButton_Click;
            panelMenu.TabStop = true;


        }

        private void VolumeSlider_GotFocus(object sender, EventArgs e)
        {
            Source.Activate();
            Source.Focus();
        }

        private void VolumeSlider_ValueChanged(object sender, EventArgs e)
        {
            TrackBar volumeSlider = (TrackBar)sender;
            float volume = (float)volumeSlider.Value / volumeSlider.Maximum;
            wo.Volume = volume;
            wo2.Volume = volume;
            wo3.Volume = volume;
        }

        public void PanelHero()
        {


            if (Screen.PrimaryScreen.WorkingArea.Height < 1050 || Screen.PrimaryScreen.WorkingArea.Width < 1600)
            {
                labelSize = 35;
                spacing = 5;
                panelRight = new Panel();
                panelRight.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 2 - labelSize * 6, Screen.PrimaryScreen.Bounds.Height / 2 - labelSize * 6);
                panelRight.Location = new Point((Screen.PrimaryScreen.Bounds.Height - panelRight.Width) / 2, 0);
                panelRight.AutoSize = true;
                panelRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                panelRight.Visible = false;
                this.Controls.Add(panelRight);
            }
            else
            {
                panelRight = new Panel();
                panelRight.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 2 - labelSize * 3, Screen.PrimaryScreen.Bounds.Height / 2 - labelSize * 3);
                panelRight.Location = new Point(Screen.PrimaryScreen.Bounds.Height / 2, 0);

                panelRight.Visible = false;
                this.Controls.Add(panelRight);
            }
            panelRight.Paint += (sender, e) =>
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    panelRight.ClientRectangle,
                    Color.Black,
                    Color.LightGray,
                    LinearGradientMode.Vertical
                );

                e.Graphics.FillRectangle(gradientBrush, panelRight.ClientRectangle);
                gradientBrush.Dispose();
            };


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Label label = new Label();
                    if (6 * i + j < Inventory.Count)
                    {
                        var First_Weapon = from Item in Edc.Items
                                           where Item.Id == Inventory[6 * i + j]
                                           select Item;
                        foreach (Items Querry in First_Weapon)
                        {
                            label.Image = Image.FromFile(Environment.CurrentDirectory + "\\Items\\" + Querry.Name.ToString() + ".png");
                        }
                    }
                    label.Size = new Size(labelSize, labelSize);
                    label.BackColor = Color.LightGray; // Zmieniamy kolor na czarny
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.Location = new Point(j * (labelSize + spacing) + spacing, i * (labelSize + spacing) + labelSize / 2);
                    label.Margin = new Padding(spacing, 0, 0, 0);
                    label.AllowDrop = true;
                    panelRight.Controls.Add(label);
                }
            }
            PictureBox heroImage = new PictureBox();
            heroImage.SizeMode = PictureBoxSizeMode.Zoom;
            heroImage.MinimumSize = new Size(panelRight.Width, panelRight.Height);
            heroImage.Location = new Point((panelRight.Bounds.Width + heroImage.Size.Width) / 2 + labelSize, panelRight.Height / 2 - heroImage.Height / 2);
            heroImage.Image = Image.FromFile(Environment.CurrentDirectory + "\\Characters\\hero.png");
            heroImage.BackColor = Color.Transparent;
            panelRight.Controls.Add(heroImage);





            panelBottom = new TableLayoutPanel();
            panelBottom.Size = new Size(panelRight.Width, panelRight.Height / 2);
            panelBottom.AutoSize = true;
            panelBottom.MinimumSize = new Size(panelRight.Width, panelRight.Height);
            panelBottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelBottom.Location = new Point(panelRight.Location.X, panelRight.Height);
            panelBottom.Padding = new Padding(0, panelBottom.Height / 4, 0, panelBottom.Height / 4);
            panelBottom.ColumnCount = 5;
            panelBottom.Visible = false;
            panelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.Controls.Add(panelBottom);

            panelBottom.Paint += (sender, e) =>
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    panelBottom.ClientRectangle,
                    Color.LightGray,
                    Color.Black,

                    LinearGradientMode.Vertical
                );

                e.Graphics.FillRectangle(gradientBrush, panelBottom.ClientRectangle);
                gradientBrush.Dispose();
            };



            menuRight = new Panel();
            menuRight.Location = new Point(0, 0);
            menuRight.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\menu.png");
            menuRight.Size = new Size(50, 50);
            menuRight.BackColor = Color.Gray;
            menuRight.BackgroundImageLayout = ImageLayout.Center;
            menuRight.Click += menuRightButton_Click;
            menuRight.Visible = true;
            this.Controls.Add(menuRight);
        }

        public void AddItem(int eq, PictureBox pic)
        {
            Inventory.Add(eq);
            foreach (var obj in panelRight.Controls)
            {
                if (obj is Label)
                {
                    Label Slot = obj as Label;
                    if (Slot.Image == null)
                    {
                        Slot.Image = pic.Image;
                        break;
                    }
                }
            }
        }
        private void menuRightButton_Click(object sender, EventArgs e)
        {
            buttonClicking.Position = 0;
            wo6.Init(buttonClicking);
            wo6.Play();
            if (wo.PlaybackState == PlaybackState.Playing) {
                wo6.Stop();
                buttonClicking.Position = 0;
                wo6.Dispose();
            }
            else
            {
                wo6.Stop();

                wo6.Dispose();
            }
            
            panelRight.Visible = !panelBottom.Visible;
            panelBottom.Visible = !panelBottom.Visible;
            if (panelRight.Visible && panelBottom.Visible)
            {
                speed = 0;
                timer.Enabled = false;
                menu.Visible = false;

            }
            else
            {
                speed = 5;
                timer.Enabled = true;
                menu.Visible = true;
            }
        }
        private void menuButton_Click(object sender, EventArgs e)
        {
            buttonClicking.Position = 0;
            wo6.Init(buttonClicking);
            wo6.Play();
            if (wo.PlaybackState == PlaybackState.Playing)
            {
                wo6.Stop();
                buttonClicking.Position = 0;
                wo6.Dispose();
            }
            else
            {
                wo6.Stop();

                wo6.Dispose();
            }
            panelMenu.Visible = !panelMenu.Visible;
            if (panelMenu.Visible)
            {
                speed = 0;
                timer.Enabled = false;
                menuRight.Visible = false;
            }
            else
            {
                speed = 5;
                timer.Enabled = true;
                menuRight.Visible = true;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            buttonClicking.Position = 0;
            wo6.Init(buttonClicking);
            wo6.Play();
            if (wo.PlaybackState == PlaybackState.Playing) {
                wo6.Stop();
                buttonClicking.Position = 0;
                wo6.Dispose();
            }
            else
            {
                wo6.Stop();

                wo6.Dispose();
            }
            menuButton_Click(null, null);
        }

        public Display(int Width, int Height, MainForm Source_Form)
        {
            string battle1 = "\\Sounds\\battle1.mp3";
            string battle2 = "\\Sounds\\battle2.mp3";
            string battle3 = "\\Sounds\\battle3.mp3";
            string battle4 = "\\\\Sounds\\battle4.mp3";
            string battle5 = "\\Sounds\\battle5.mp3";
            string battle6 = "\\Sounds\\battle6.mp3";

            battleMusicThemes.Add(battle1);
            battleMusicThemes.Add(battle2);
            battleMusicThemes.Add(battle3);
            battleMusicThemes.Add(battle4);
            battleMusicThemes.Add(battle5);
            battleMusicThemes.Add(battle6);

            wo.Volume = Source_Form.Get_Volume_Level;
            
            FadeInOutSampleProvider fade = new FadeInOutSampleProvider(MainMusic, true);
            fade.BeginFadeIn(2000);
            wo.Init(fade);
            wo.Play();

            Inventory = new List<int>();
            string SectionName = "Player_Items";
            var ApplicationConfig = ConfigurationManager.GetSection(SectionName) as NameValueCollection;
            foreach (var key in ApplicationConfig.AllKeys)
            {
                string[] Player_Items = ApplicationConfig[key].Split(',');
                foreach (string Item_id in Player_Items)
                {
                    Inventory.Add(Convert.ToInt32(Item_id));
                }
            }



            Source = Source_Form;

            this.AutoSize = true;
            this.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\Background.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Dock = DockStyle.Fill;

            Game_Board = new PictureBox();
            Display_Size = Width;
            if (Width > Height)
            {
                Display_Size = Height;
            }
            Game_Board.Size = new Size(Display_Size, Display_Size);
            Game_Board.BackColor = Color.Red;
            Game_Board.SizeMode = PictureBoxSizeMode.Zoom;
            Game_Board.Location = new Point((Width - Display_Size) / 2, (Height - Display_Size) / 2);
            this.Controls.Add(Game_Board);

            timer = new Timer();
            timer.Interval = 100; // Set the interval to 100 milliseconds
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();
        }

        public void Load_Level(int level, int Sx, int Sy)
        {
            MapPositionX = Sx;
            MapPositionY = Sy;
            Current_Level = level;
            
            Character = new PictureBox();

            Game_Board.Image = SetBackGround(level, MapPositionX, MapPositionY);
            Character.Image = Image.FromFile(Environment.CurrentDirectory + "\\Characters\\Hero.png");

            Character.Size = new Size(Character_size, Character_size);

            Character.SizeMode = PictureBoxSizeMode.Zoom;

            Character.BackColor = Color.Transparent;

            Game_Board.Controls.Add(Character);
            Character.Location = new Point(Display_Size / 2, Display_Size / 2);

            //Beta HitBox'a
            Level_Hitbox = SetHitbox(level, MapPositionX, MapPositionY);

            //Punkty interakcji
            Objects = new List<Interactive_Object>();

            string SectionName = "Map_" + Current_Level.ToString() + "_Data/Map_Objects";
            Retrive_Interactive_Objects(SectionName, Current_Level, Objects);

            foreach (Interactive_Object IO in Objects)
            {
                if (MapPositionX == IO.Get_Map_X && MapPositionY == IO.Get_Map_Y && !IO.Get_Interaction)
                {
                    Game_Board.Controls.Add(IO.Get_Icon);
                }
                else
                {
                    Game_Board.Controls.Remove(IO.Get_Icon);
                }
            }
            
            battle = false;
            
            
            PanelMenu();
            PanelHero();

            player = new Player(Character, 1, 60, 8, 10, 1, panelBottom);


            panelBottom.BringToFront();
            panelRight.BringToFront();
            panelMenu.BringToFront();
            menuRight.BringToFront();
            
            showMessage("Tylko poprzez pokonanie bestii i potworów zdoła ocalić królestwo i przywrócić spokój jego mieszkańcom. Teraz Ty wcielasz się w jego rolę. Dasz radę temu podołać?");
        }

        public static void Retrive_Interactive_Objects(string SectionName, int Current_Level, List<Interactive_Object> Object_list)
        {
            var ApplicationConfig = ConfigurationManager.GetSection(SectionName) as NameValueCollection;
            foreach (var key in ApplicationConfig.AllKeys)
            {
                string[] Object_Data = ApplicationConfig[key].Split(',');
                int[] Int_data = new int[6];
                int i = 0;
                foreach (string Int_num in Object_Data)
                {
                    Int_data[i] = Convert.ToInt32(Int_num);
                    i++;
                    if (i == Int_data.Length)
                    {
                        break;
                    }
                }

                Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], Convert.ToInt32(Object_Data[7]));

                if (Object_Data[6].Length >= 5 && Object_Data[6].Substring(0, 5) == "Enemy")
                {
                    Entity Monster = new Entity(New_Object.Get_Icon, Convert.ToInt32(Object_Data[7]), Convert.ToInt32(Object_Data[8]));
                    New_Object.Get_Type = Monster;
                }
                Object_list.Add(New_Object);
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            // Check if there's an enemy in the current scene
            foreach (Interactive_Object Single_Enemy in Objects)
            {
                if (Single_Enemy.Get_Type is Entity && Single_Enemy.Get_Map_X == MapPositionX && Single_Enemy.Get_Map_Y == MapPositionY)
                {
                    int EnemyX = Single_Enemy.Get_Pos_X;
                    int EnemyY = Single_Enemy.Get_Pos_Y;
                    int dx = Character.Location.X - EnemyX;
                    int dy = Character.Location.Y - EnemyY;

                    int directionX = 0;
                    int directionY = 0;
                    int length = (int)Math.Sqrt(dx * dx + dy * dy);

                    if (length != 0 && length > 200)
                    {
                        enemySpeed = 7;
                        directionX = dx * enemySpeed / length;
                        directionY = dy * enemySpeed / length;

                    }
                    else if (length != 0 && length <= 200 && length >= 50)
                    {

                        enemySpeed = 15;
                        directionX = dx * enemySpeed / length;
                        directionY = dy * enemySpeed / length;

                    }
                    else if (length == 0 || (length != 0 && length < 50))
                    {
                        KeyEventArgs Move_up = new KeyEventArgs(Keys.Up);
                        Movement(Move_up);
                        PictureBox temp = new PictureBox();
                        temp.SizeMode = Game_Board.SizeMode;
                        temp.Size = Game_Board.Size;
                        temp.BackColor = this.BackColor;

                        temp.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + Current_Level.ToString() + "\\Battle.png");
                        battle = true;

                        wo.Pause();
                        Random randomMusicTrack = new Random();
                        
                        fight = new AudioFileReader(Environment.CurrentDirectory+battleMusicThemes[randomMusicTrack.Next(0, 6)]);
                        wo2.Volume = wo.Volume;
                        wo2.Init(fight);
                        wo2.Play();
                        menu.Visible = false;
                        menuRight.Visible = false;
                        Battle Prepare_Battle = new Battle(temp, Inventory, this);
                        Character.Visible = false;

                        foreach (Interactive_Object IO in Objects)
                        {
                            IO.Get_Icon.Visible = false;
                        }

                        Game_Board.Controls.Add(Prepare_Battle.Get_Background);

                        //Dodawanie gracz
                        PictureBox fighter = new PictureBox();
                        fighter.SizeMode = PictureBoxSizeMode.Zoom;
                        fighter.BackColor = Color.Transparent;
                        fighter.Size = new Size(200, 200);

                        fighter.Image = Character.Image;
                        fighter.Location = new Point(150, Display_Size / 2 - fighter.Height + 150);

                        Prepare_Battle.Add_entity('p', 8, player.Health, fighter);

                        //Dodawanie stworzeń
                        int Position_x = Display_Size;
                        for (int i = 0; i < Objects.Count; i++)
                        {
                            Interactive_Object enemy_nearby = Objects[i];
                            if (enemy_nearby.Get_Type is Entity && enemy_nearby.Get_Map_X == MapPositionX && enemy_nearby.Get_Map_Y == MapPositionY
                                && Math.Pow(Single_Enemy.Get_Pos_X - enemy_nearby.Get_Pos_X, 2) + Math.Pow(Single_Enemy.Get_Pos_Y - enemy_nearby.Get_Pos_Y, 2) <= Math.Pow(4 * Single_Enemy.Get_Range, 2))
                            {
                                PictureBox Opponent = new PictureBox();
                                Opponent.SizeMode = PictureBoxSizeMode.Zoom;
                                Opponent.BackColor = Color.Transparent;

                                Entity Next_Enemy = (enemy_nearby.Get_Type as Entity);

                                Opponent.Size = new Size(Next_Enemy.Get_Creature.Width * 4, Next_Enemy.Get_Creature.Height * 4);
                                Opponent.Image = Next_Enemy.Get_Creature.Image;

                                Position_x = Position_x - Opponent.Width;
                                Opponent.Location = new Point(Position_x, Display_Size / 2 - Opponent.Height + 150);

                                if(Prepare_Battle.Get_Opponents.Count >= 2)
                                {
                                    Opponent.Visible = false;
                                }
                                
                                Prepare_Battle.Add_entity('e', Next_Enemy.Get_Damage, Next_Enemy.Get_health, Opponent);
                                enemyCount++;
                                Objects.Remove(enemy_nearby);
                                if(Prepare_Battle.Get_Opponents.Count % 2 == 0)
                                {
                                    Position_x = Display_Size;
                                }
                            }
                        }

                        foreach (Entity el in Prepare_Battle.Get_Our_team)
                        {
                            Prepare_Battle.Get_Background.Controls.Add(el.Get_Creature);
                        }

                        foreach (Entity el in Prepare_Battle.Get_Opponents)
                        {
                            Prepare_Battle.Get_Background.Controls.Add(el.Get_Creature);
                        }

                        Objects.Remove(Single_Enemy);
                        activeBattle = Prepare_Battle;
                        timer.Enabled = false;
                        break;
                    }

                    EnemyX += directionX;
                    EnemyY += directionY;
                    Single_Enemy.Get_Icon.Location = new Point(EnemyX, EnemyY);
                }
            }

        }

        private Image SetBackGround(int level, int x, int y)
        {
            Image BackGround = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + level.ToString() + "\\Map " + x.ToString() + "-" + y.ToString() + ".png");
            BackGround = Scale(BackGround, Display_Size, Display_Size);
            return BackGround;
        }

        private Bitmap SetHitbox(int level, int x, int y)
        {
            Bitmap Source = new Bitmap(Environment.CurrentDirectory + "\\Map parts\\Level " + level.ToString() + "\\Hitbox_Map " + x.ToString() + "-" + y.ToString() + ".png");
            Bitmap Hitbox = (Bitmap)Scale(Source, Display_Size, Display_Size);
            return Hitbox;
        }

        public static Image Scale(Image sourceImage, int destWidth, int destHeight)
        {
            var toReturn = new Bitmap(destWidth, destHeight);

            using (var graphics = Graphics.FromImage(toReturn))
            using (var attributes = new ImageAttributes())
            {
                toReturn.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

                attributes.SetWrapMode(WrapMode.TileFlipXY);

                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(sourceImage, Rectangle.FromLTRB(0, 0, destWidth, destHeight), 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attributes);
            }

            return toReturn;
        }


        public void Movement(KeyEventArgs e)
        {
            
            if (battle)
            {
                return;
            }
            int x = Character.Location.X;
            int y = Character.Location.Y;
            int Border = Display_Size - Character_size - 1;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        Character.Location = new Point(x - TestHitbox(x, x - speed, y, y), y);
                        break;
                    }
                case Keys.Right:
                    {
                        Character.Location = new Point(x + TestHitbox(x, x + speed, y, y), y);
                        break;
                    }
                case Keys.Up:
                    {
                        Character.Location = new Point(x, y - TestHitbox(x, x, y, y - speed));

                        break;
                    }
                case Keys.Down:
                    {
                        Character.Location = new Point(x, y + TestHitbox(x, x, y, y + speed));
                        break;
                    }
            }

            x = Character.Location.X;
            y = Character.Location.Y;

            bool Scene_Switch = false;
            if (y == 0)
            {
                MapPositionY--;
                Scene_Switch = true;
                Character.Location = new Point(x, Border - 1);
            }
            else if (y == Border)
            {
                MapPositionY++;
                Scene_Switch = true;
                Character.Location = new Point(x, 1);
            }
            else if (x == 0)
            {
                MapPositionX--;
                Scene_Switch = true;
                Character.Location = new Point(Border - 1, y);
            }
            else if (x == Border)
            {
                MapPositionX++;
                Scene_Switch = true;
                Character.Location = new Point(1, y);
            }
            foreach (Interactive_Object IO in Objects)
            {
                if (Game_Board.Controls.Contains(IO.Get_Icon))
                {
                    if (Math.Pow(x - IO.Get_Pos_X, 2) + Math.Pow(y - IO.Get_Pos_Y, 2) <= Math.Pow(IO.Get_Range, 2))
                    {
                        IO.Get_Interaction = true;
                        Game_Board.Controls.Remove(IO.Get_Icon);
                        if (IO.Get_Name == "Coin")
                        {
                            coinPicking.Position = 0;
                            wo4.Init(coinPicking);
                            wo4.Play();
                            if(wo.PlaybackState == PlaybackState.Playing) { }
                            else
                            {
                                wo4.Stop();
                                
                                wo4.Dispose();
                            }
                            player.AddCoins((int)IO.Get_Type);
                        }
                        else if (IO.Get_Name == "Weapon")
                        {
                            AddItem((int)IO.Get_Type, IO.Get_Icon);
                            weaponPicking.Position = 0;
                            wo5.Init(weaponPicking);
                            wo5.Play();
                            if (wo.PlaybackState == PlaybackState.Playing) { }
                            else
                            {
                                wo5.Stop();

                                wo5.Dispose();
                            }
                        }
                        else if (IO.Get_Name == "EOL")
                        {
                            //pokazanie tablicy wyników (top 10)
                            UserControlHighScores uchst = new UserControlHighScores();
                            
                            FlowLayoutPanel flphs = new FlowLayoutPanel();
                            flphs.FlowDirection = FlowDirection.TopDown;
                            flphs.Size = new Size(749, 750);
                            flphs.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - flphs.Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - flphs.Size.Height / 2);
                            flphs.BackColor = Color.Transparent;
                            FormNickname fn = new FormNickname();
                            if (fn.ShowDialog() == DialogResult.OK)
                            {
                                fn.HighScore.score = player.Score;
                                hsdb.HighScore.InsertOnSubmit(fn.HighScore);
                                hsdb.SubmitChanges();
                            }

                            flphs.Controls.Add(uchst);
                            int counter = 1;
                            foreach(HighScore hst in hsdb.HighScore.OrderByDescending(xx => xx.score))
                            {
                                if (counter <= 10)
                                {
                                    UserControlHighScores uchs = new UserControlHighScores(counter, hst.name, hst.score);
                                    flphs.Controls.Add(uchs);
                                    counter++;
                                }
                            }
                            Game_Board.SendToBack();
                            this.Controls.Add(flphs);
                            flphs.BringToFront();
                            timer.Stop();
                        }
                    }
                }
            }


            if (Scene_Switch)
            {
                Game_Board.Image = SetBackGround(Current_Level, MapPositionX, MapPositionY);
                Level_Hitbox = SetHitbox(Current_Level, MapPositionX, MapPositionY);

                foreach (Interactive_Object IO in Objects)
                {
                    if (MapPositionX == IO.Get_Map_X && MapPositionY == IO.Get_Map_Y && !IO.Get_Interaction)
                    {
                        Game_Board.Controls.Add(IO.Get_Icon);
                    }
                    else
                    {
                        Game_Board.Controls.Remove(IO.Get_Icon);
                    }
                }
            }
        }
        private int TestHitbox(int old_x, int new_x, int old_y, int new_y)
        {

            int distance = 0;
            int start_x, start_y, end_x, end_y, direction_x = 1, direction_y = 1;
            int temp = 0;
            if (new_x != old_x)
            {
                start_y = old_y;
                end_y = old_y + Character_size;
                direction_y = 1;
                if (new_x < old_x)
                {
                    start_x = old_x - 1;
                    end_x = new_x - 1;
                    direction_x = -1;
                }
                else
                {
                    start_x = old_x + Character_size + 1;
                    end_x = new_x + Character_size + 1;
                    direction_x = 1;
                }
                temp = start_y;
                while (start_x != end_x)
                {
                    start_y = temp;
                    while (start_y != end_y)
                    {
                        if (start_x < Display_Size && start_x >= 0)
                        {
                            if (Level_Hitbox.GetPixel(start_x, start_y).ToArgb() != Color.White.ToArgb())
                            {
                                return distance;
                            }
                            start_y += direction_y;
                        }
                        else
                        {
                            return distance;
                        }
                    }
                    start_x += direction_x;
                    distance++;
                }
            }
            else if (new_y != old_y)
            {
                start_x = old_x;
                end_x = old_x + Character_size;
                direction_x = 1;
                if (new_y < old_y)
                {
                    start_y = old_y - 1;
                    end_y = new_y - 1;
                    direction_y = -1;
                }
                else
                {
                    start_y = old_y + Character_size + 1;
                    end_y = new_y + Character_size + 1;
                    direction_y = 1;
                }

                temp = start_x;
                while (start_y != end_y)
                {
                    start_x = temp;
                    while (start_x != end_x)
                    {
                        if (start_y < Display_Size && start_y >= 0)
                        {
                            if (Level_Hitbox.GetPixel(start_x, start_y).ToArgb() != Color.White.ToArgb())
                            {
                                return distance;
                            }
                            start_x += direction_x;
                        }
                        else
                        {
                            return distance;
                        }
                    }
                    start_y += direction_y;
                    distance++;
                }
            }

            return distance;
        }

        
        public void StopMovement()
        {
            speed = 0;
            enemySpeed = 0;
            timer.Enabled = false;
        }

        public void EnableMovement()
        {
            speed = 5;
            enemySpeed = 7;
            timer.Enabled = true;
        }

        public async void showMessage(string message)
        {

            wo3.Init(messageSound);
            wo3.Play();

            StopMovement();
            Panel panelText = new Panel();
            panelText.Width = Screen.PrimaryScreen.Bounds.Width /2;
            panelText.Height = 300;
            panelText.Location = new Point((Game_Board.Width - panelText.Width)/2, Game_Board.Height - panelText.Height);
            panelText.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\textBackGround.png");
            panelText.Visible = true;
            panelText.Name = "message box";
            panelText.BackgroundImageLayout = ImageLayout.Stretch;
            panelText.BackColor = Color.Transparent;
            Game_Board.Controls.Add(panelText);

            Label text = new Label();

            text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            text.Width = Convert.ToInt32(panelText.Width*0.8);
            text.Height = panelText.Height;
            text.Location = new Point((panelText.Width - text.Width)/2, text.Height / 4);
            text.Font = new Font("Ink Free", 18, FontStyle.Bold);
            text.BackColor = Color.Transparent;
            panelText.Controls.Add(text);

            string currentText = string.Empty;

            text.Click += messageBox_Click;

            foreach (char letter in message)
            {
                currentText += letter;
                text.Text = currentText;

                await Task.Delay(20);
            }
        }

        private void messageBox_Click(object sender, EventArgs e)
        {
            EnableMovement();
            foreach(Control ctrl in Game_Board.Controls)
            {
                if(ctrl is Panel panel && panel.Name == "message box")
                {
                    Game_Board.Controls.Remove(ctrl);
                }
            }

            Source.Activate();
            Source.Focus();
        }

        public void stopBattle()
        {
            
            wo2.Pause();
            wo2.Dispose();
            fight.Position = 0;
            wo2.Init(fight);
            
            Thread.Sleep(1000);           
            MainMusic.Position = 0;
            wo.Play();           

            Game_Board.Controls.Remove(activeBattle.Get_Background);
            activeBattle.Dispose();
            activeBattle = null;
            Character.Visible = true;
            battle = false;
            Source.Activate();
            Source.Focus();
            timer.Enabled = true;
            menu.Visible = true;
            menuRight.Visible = true;
            player.AddCoins(10);
            player.AddExp(10);
            player.AddScore(100*enemyCount);
            enemyCount = 0;
            foreach (Interactive_Object IO in Objects)
            {
                IO.Get_Icon.Visible = true;
            }
        }
    }

}

