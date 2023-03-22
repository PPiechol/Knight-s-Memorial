using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Display : Panel
    {
        PictureBox Game_Board;
        PictureBox Character;
        Bitmap Level_Hitbox;
        List<Interactive_Object> Objects;
        PictureBox temp;
        private const int Character_size = 50;
        private const int speed = 5;
        int Display_Size;
        int MapPositionX;
        int MapPositionY;
        int Current_Level;
        bool picked;

        public Display(int Width, int Height)
        {
            this.AutoSize = true;
            this.BackColor = Color.Orange;
            this.Dock = DockStyle.Fill;

            Game_Board = new PictureBox();
            Display_Size = Width;
            if(Width > Height)
            {
                Display_Size = Height;
            }
            Game_Board.Size = new Size(Display_Size, Display_Size);
            Game_Board.BackColor = Color.Red;
            Game_Board.SizeMode = PictureBoxSizeMode.Zoom;
            Game_Board.Location = new Point((Width - Display_Size)/2, (Height - Display_Size) / 2);
            this.Controls.Add(Game_Board);
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
            Character.Location = new Point(Display_Size/2, Display_Size / 2);

            //Beta HitBox'a
            Level_Hitbox = SetHitbox(level, MapPositionX, MapPositionY);
            Objects = new List<Interactive_Object>();
            Interactive_Object Coin = new Interactive_Object(1, 1, 0, 350, 200, 100, "Coin");
            Objects.Add(Coin);

            temp = new PictureBox();
            temp.SizeMode = PictureBoxSizeMode.Zoom;
            temp.Size = new Size(15, 15);
            temp.BackColor = Color.Transparent;
            temp.Image = Objects[0].GetIcon;
            int temp_x = Objects[0].Get_Pos_X;
            int temp_y = Objects[0].Get_Pos_Y;
            temp.Location = new Point(temp_x, temp_y);
            temp.Visible = false;
            Game_Board.Controls.Add(temp);
            picked = false;
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

            if(temp.Visible)
            {
                if(Math.Pow(x - Objects[0].Get_Pos_X, 2) + Math.Pow(y - Objects[0].Get_Pos_Y, 2) <= Math.Pow(Objects[0].Get_Range,2))
                {
                    picked = true;
                    temp.Visible = false;
                }
            }

            if(Scene_Switch)
            {
                Game_Board.Image = SetBackGround(Current_Level, MapPositionX, MapPositionY);
                Level_Hitbox = SetHitbox(Current_Level, MapPositionX, MapPositionY);

                if (MapPositionX == 1 && MapPositionY == 0 && !picked)
                {
                    temp.Visible = true;
                }
                else
                {
                    temp.Visible = false;
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
                while(start_x != end_x)
                {
                    start_y = temp;
                    while(start_y != end_y)
                    {
                        if (start_x < Display_Size && start_x >= 0)
                        {
                            if(Level_Hitbox.GetPixel(start_x, start_y).ToArgb() != Color.White.ToArgb())
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
    }
}
