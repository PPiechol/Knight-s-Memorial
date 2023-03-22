using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Interactive_Object
    {
        int level;
        int map_pos_x;
        int map_pos_y;
        int interaction_range;
        //Od tąd
        int pos_x;
        int pos_y;
        Image icon;
        //Do tąd zamienić na picturebox

        public Interactive_Object(int level, int map_x, int map_y, int x, int y, int range, string name)
        {
            this.level = level;
            map_pos_x = map_x;
            map_pos_y = map_y;
            pos_x = x;
            pos_y = y;
            interaction_range = range;
            icon = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + level.ToString() + "\\" + name + ".png");
        }

        public Image GetIcon
        {
            get 
            {
                return icon;
            }
        }

        public int Get_Pos_X
        {
            get
            {
                return pos_x;
            }
        }

        public int Get_Pos_Y
        {
            get
            {
                return pos_y;
            }
        }

        public int Get_Range
        {
            get
            {
                return interaction_range;
            }
        }
    }
}
