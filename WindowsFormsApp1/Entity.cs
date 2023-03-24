using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Entity
    {
        PictureBox creature;
        int health;
        int damage;
        public Entity(PictureBox creature, int damage ,int max_health)
        {
            this.creature = creature;
            this.damage = damage;
            health = max_health;
        }

        public PictureBox Get_Creature
        {
            get
            {
                return creature;
            }
        }

        public int Get_health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int Get_Damage
        {
            get
            {
                return damage;
            }
        }
    }
}

