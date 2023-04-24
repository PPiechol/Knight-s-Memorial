using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Interactive_Object
    {
        int level;
        int map_pos_x;
        int map_pos_y;
        int interaction_range;
        object type;
        PictureBox icon;
        bool interacted;
        EquipmentDataContext Edc = new EquipmentDataContext();

        public Interactive_Object(int level, int map_x, int map_y, int x, int y, int size, int range, string name, object type)
        {
            icon = new PictureBox();
            icon.SizeMode = PictureBoxSizeMode.Zoom;
            this.level = level;
            map_pos_x = map_x;
            map_pos_y = map_y;
            icon.Name = name;
            if(name == "Weapon")
            {
                var First_Weapon = from Item in Edc.Items
                                   where Item.Id == Convert.ToInt32(type)
                                   select Item;
                foreach (Items Querry in First_Weapon)
                {
                    icon.Image = Image.FromFile(Environment.CurrentDirectory + "\\Items\\" + Querry.Name.ToString() + ".png");
                }
                icon.Tag = Convert.ToInt32(type);
            }
            else
            {
                icon.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level " + level.ToString() + "\\" + name + ".png");
            }
            icon.BackColor = Color.Transparent;
            icon.Size = new Size(size, size);
            icon.Location = new Point(x, y);
            interaction_range = range;
            interacted = false;
            this.type = type;
        }

        public PictureBox Get_Icon
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
                return icon.Location.X;
            }
        }

        public int Get_Pos_Y
        {
            get
            {
                return icon.Location.Y;
            }
        }

        public int Get_Map_X
        {
            get
            {
                return map_pos_x;
            }
        }

        public int Get_Map_Y
        {
            get
            {
                return map_pos_y;
            }
        }

        public int Get_Range
        {
            get
            {
                return interaction_range;
            }
        }

        public bool Get_Interaction
        {
            get
            {
                return interacted;
            }
            set
            {
                interacted = value;
            }
        }

        public string Get_Name
        {
            get
            {
                return icon.Name;
            }
        }

        public object Get_Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public object Location { get; internal set; }
    }
}
