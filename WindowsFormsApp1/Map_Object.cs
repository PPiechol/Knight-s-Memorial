using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Map_Object : PictureBox
    {
        Interactive_Object Object;
        string Key;
        public Map_Object(Interactive_Object Object, string Key_name)
        {
            this.Object = Object;
            Key = Key_name;
            this.Image = (System.Drawing.Bitmap)Object.Get_Icon.Image.Clone();
            this.Size = Object.Get_Icon.Size;
            this.Location = Object.Get_Icon.Location;
            this.SizeMode = Object.Get_Icon.SizeMode;
            this.BackColor = Object.Get_Icon.BackColor;
        }

        public string Get_Key
        {
            get
            {
                return Key;
            }
        }

        public Interactive_Object Get_Object
        {
            get
            {
                return Object;
            }
        }
    }
}
