using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Display : Panel
    {
        private const int size = 600;
        private const int Character_size = 40;
        private const int speed = 3;
        PictureBox Background;
        PictureBox Character;
        int MapPositionX;
        int MapPositionY;
        int MapSizeX = 2;
        int MapSizeY = 2;

        public Display(int level, int Sx, int Sy)
        {
            this.AutoSize = true;

            MapPositionX = Sx;
            MapPositionY = Sy;

            Background = new PictureBox();
            Character = new PictureBox();

            Background.Image = SetBackGround(level, MapPositionX, MapPositionY);
            Character.Image = Image.FromFile(Environment.CurrentDirectory + "\\Characters\\Hero.png");
            
            Background.Size = new Size(size, size);
            Character.Size = new Size(Character_size, Character_size);

            Background.SizeMode = PictureBoxSizeMode.Zoom;
            Character.SizeMode = PictureBoxSizeMode.Zoom;

            Character.BackColor = Color.Transparent;

            this.Controls.Add(Background);
            Background.Controls.Add(Character);
            Character.Location = new Point(size/2, size/2);
        }

        private Image SetBackGround(int level, int x, int y)
        {
            Image BackGround = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + level.ToString() + "\\Map " + x.ToString() + "-" + y.ToString() + ".png");
            return BackGround;
        }

        public void Movement(KeyEventArgs e)
        {
            int x = Character.Location.X;
            int y = Character.Location.Y;
            int Border = size - Character_size;
            switch(e.KeyCode)
            {
                case Keys.Left:
                    {
                        x -= speed;
                        if (x < 0)
                        {
                            x = 0;
                        }
                        Character.Location = new Point(x, y);
                        break;
                    }
                case Keys.Right:
                    {
                        x += speed;
                        if (x > Border)
                        {
                            x = Border;
                        }
                        Character.Location = new Point(x, y);
                        break;
                    }
                case Keys.Up:
                    {
                        y -= speed;
                        if (y < 0)
                        {
                            y = 0;
                        }
                        Character.Location = new Point(x, y);
                        break;
                    }
                case Keys.Down:
                    {
                        y += speed;
                        if (y > Border)
                        {
                            y = Border;
                        }
                        Character.Location = new Point(x, y);
                        break;
                    }
            }

            x = Character.Location.X;
            y = Character.Location.Y;

            if(y == 0 && MapPositionY > 0)
            {
                MapPositionY--;
                Background.Image = SetBackGround(1, MapPositionX, MapPositionY);
                Character.Location = new Point(x, Border - 1);
            }
            else if (y == Border && MapPositionY < MapSizeY - 1)
            {
                MapPositionY++;
                Background.Image = SetBackGround(1, MapPositionX, MapPositionY);
                Character.Location = new Point(x, 1);
            }
            else if (x == 0 && MapPositionX > 0)
            {
                MapPositionX--;
                Background.Image = SetBackGround(1, MapPositionX, MapPositionY);
                Character.Location = new Point(Border - 1, y);
            }
            else if (x == Border && MapPositionX < MapSizeX - 1)
            {
                MapPositionX++;
                Background.Image = SetBackGround(1, MapPositionX, MapPositionY);
                Character.Location = new Point(1, y);
            }
        }
    }
}
