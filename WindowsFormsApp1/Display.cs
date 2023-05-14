﻿using System;
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
using System.Windows.Forms.VisualStyles;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace WindowsFormsApp1
{

    class Display : Panel
    {
        PictureBox Game_Board;
        Player player;

        TextBox messageBox;
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
        Panel menuLeft;
        Battle activeBattle;
        Form Source;
        List<int> Inventory;
        Timer timer = new Timer();
        int labelSize = 50;
        int spacing = 10;
        EquipmentDataContext Edc = new EquipmentDataContext();


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
            panelMenu.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 5, 250);
            panelMenu.Location = new Point((Screen.PrimaryScreen.Bounds.Width - panelMenu.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - panelMenu.Height) / 2);
            panelMenu.BackColor = Color.Gray;
            this.Controls.Add(panelMenu);

            menuText = new System.Windows.Forms.Label();
            menuText.Text = "Game paused";
            menuText.Visible = true;
            menuText.BackColor = Color.White;
            menuText.Padding = new Padding(5);
            menuText.Size = new Size(200, 50);
            menuText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            menuText.Font = new Font("Arial", 16, FontStyle.Bold);
            menuText.BackColor = Color.Transparent;
            menuText.Location = new Point((panelMenu.Width - menuText.Width) / 2, (panelMenu.Height / 2 - 60));
            panelMenu.Controls.Add(menuText);

            continueButton = new System.Windows.Forms.Label();
            continueButton.Text = "Continue";
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

            continueButton.Location = new Point((panelMenu.Width - menuText.Width) / 2, (panelMenu.Height / 2));
            panelMenu.Controls.Add(continueButton);




            exitButton = new System.Windows.Forms.Label();
            exitButton.Text = "Exit";
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

            exitButton.Location = new Point((panelMenu.Width - exitButton.Width) / 2, (panelMenu.Height / 2) + 60);
            panelMenu.Controls.Add(exitButton);

            ;
            continueButton.Click += ContinueButton_Click;
            exitButton.Click += ExitButton_Click;
            panelMenu.TabStop = true;

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
            /*if (Screen.PrimaryScreen.Bounds.Height < 1050 || Screen.PrimaryScreen.Bounds.Width < 1400)
            {
                heroImage.SizeMode = PictureBoxSizeMode.Zoom;
            }*/
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



            menuLeft = new Panel();
            menuLeft.Location = new Point(0, 0);
            menuLeft.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\menu.png");
            menuLeft.Size = new Size(50, 50);
            menuLeft.BackColor = Color.Gray;
            menuLeft.BackgroundImageLayout = ImageLayout.Center;
            menuLeft.Click += menuLeftButton_Click;
            menuLeft.Visible = true;
            this.Controls.Add(menuLeft);
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
        private void menuLeftButton_Click(object sender, EventArgs e)
        {
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
            panelMenu.Visible = !panelMenu.Visible;
            if (panelMenu.Visible)
            {
                speed = 0;
                timer.Enabled = false;
                menuLeft.Visible = false;
            }
            else
            {
                speed = 5;
                timer.Enabled = true;
                menuLeft.Visible = true;
            }


        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            menuButton_Click(null, null);
        }


        public Display(int Width, int Height, Form Source_Form)
        {

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
            menuLeft.BringToFront();

            //showMessage(
            //   "Tylko poprzez pokonanie bestii i potworów zdoła ocalić królestwo i przywrócić spokój jego mieszkańcom. Teraz Ty wcielasz się w jego rolę. Dasz radę temu podołać?");
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

                if (Object_Data[6] == "Coin")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], Convert.ToInt32(Object_Data[7]));
                    Object_list.Add(New_Object);
                }
                else if (Object_Data[6] == "Weapon")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], Convert.ToInt32(Object_Data[7]));
                    Object_list.Add(New_Object);
                }
                else if (Object_Data[6].Length >= 5 && Object_Data[6].Substring(0, 5) == "Enemy")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], null);
                    Entity Monster = new Entity(New_Object.Get_Icon, Convert.ToInt32(Object_Data[7]), Convert.ToInt32(Object_Data[8]));
                    New_Object.Get_Type = Monster;
                    Object_list.Add(New_Object);
                }
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            // Check if there's an enemy in the current scene
            foreach (Interactive_Object enemies in Objects)
            {
                if (enemies.Get_Type is Entity && enemies.Get_Map_X == MapPositionX && enemies.Get_Map_Y == MapPositionY)
                {
                    int EnemyX = enemies.Get_Pos_X;
                    int EnemyY = enemies.Get_Pos_Y;
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
                        menu.Visible = false;
                        menuLeft.Visible = false;
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
                            Interactive_Object enemies_nearby = Objects[i];
                            if (enemies_nearby.Get_Type is Entity && enemies_nearby.Get_Map_X == MapPositionX && enemies_nearby.Get_Map_Y == MapPositionY
                                && Math.Pow(enemies.Get_Pos_X - enemies_nearby.Get_Pos_X, 2) + Math.Pow(enemies.Get_Pos_Y - enemies_nearby.Get_Pos_Y, 2) <= 10 * Math.Pow(enemies_nearby.Get_Range, 2))
                            {
                                PictureBox Opponent = new PictureBox();
                                Opponent.SizeMode = PictureBoxSizeMode.Zoom;
                                Opponent.BackColor = Color.Transparent;

                                Entity Next_Enemy = (enemies_nearby.Get_Type as Entity);

                                Opponent.Size = new Size(Next_Enemy.Get_Creature.Width * 4, Next_Enemy.Get_Creature.Height * 4);
                                Opponent.Image = Next_Enemy.Get_Creature.Image;

                                Position_x = Position_x - Opponent.Width;
                                Opponent.Location = new Point(Position_x, Display_Size / 2 - Opponent.Height + 150);

                                Prepare_Battle.Add_entity('e', Next_Enemy.Get_Damage, Next_Enemy.Get_health, Opponent);

                                Objects.Remove(enemies_nearby);
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

                        Objects.Remove(enemies);
                        activeBattle = Prepare_Battle;
                        timer.Enabled = false;
                        break;
                    }

                    EnemyX += directionX;
                    EnemyY += directionY;
                    enemies.Get_Icon.Location = new Point(EnemyX, EnemyY);
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
                            player.AddCoins((int)IO.Get_Type);
                        }
                        else if (IO.Get_Name == "Weapon")
                        {
                            AddItem((int)IO.Get_Type, IO.Get_Icon);
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
        //wyświetlanie okna dialogowego, demo
        //brak wyświetlania zdjecia na tlo
        //godzina 24, a ja dostaje pierdolca
        public async void showMessage(string message)
        {
            StopMovement();
            Panel panel = new Panel();
            panel.Width = Screen.PrimaryScreen.Bounds.Width / 4;
            panel.Height = 300;
            panel.Location = new Point(panel.Width, Screen.PrimaryScreen.Bounds.Height - panel.Height);
            panel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\BackGround\\textBack.jpg");
            panel.Visible = true;
            this.Controls.Add(panel);
            panel.BringToFront();


            TextBox textBox = new TextBox();

            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.Width = panel.Width;
            textBox.Height = panel.Height;
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.Parent = panel;
            textBox.Location = new Point(panel.Location.X, panel.Location.Y);
            textBox.Font = new Font("Terminal", 18, FontStyle.Regular);
            textBox.BackColor = Color.LightGray;
            this.Controls.Add(textBox);
            textBox.BringToFront();

            string currentText = string.Empty;

            foreach (char letter in message)
            {
                currentText += letter;
                textBox.Text = currentText;

                await Task.Delay(20);
            }



        }

        private void messageBox_Click(object sender, EventArgs e)
        {
            EnableMovement();

        }

        public void stopBattle()
        {
            Game_Board.Controls.Remove(activeBattle.Get_Background);
            activeBattle.Dispose();
            activeBattle = null;
            Character.Visible = true;
            battle = false;
            Source.Activate();
            Source.Focus();
            timer.Enabled = true;
            menu.Visible = true;
            menuLeft.Visible = true;
            player.AddCoins(10);
            player.AddExp(10);
            foreach (Interactive_Object IO in Objects)
            {
                IO.Get_Icon.Visible = true;
            }
        }
    }

}

