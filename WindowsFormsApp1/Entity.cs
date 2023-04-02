using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
        int walk_speed;
        bool Able_To_Attack;
        List<Weapon_Effects> Current_Effects;
        public Entity(PictureBox creature, int damage, int max_health)
        {
            this.creature = creature;
            this.damage = damage;
            health = max_health;
            Able_To_Attack = true;
            Current_Effects = new List<Weapon_Effects>();
            
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

        public bool AbleToAttack
        {
            get
            {
                return Able_To_Attack;
            }
            set
            {
                Able_To_Attack = value;
            }
        }

        public List<Weapon_Effects> Effects
        {
            get
            {
                return Current_Effects;
            }
            set
            {
                Current_Effects = value;
            }
        }

        public int Walk_speed { get => walk_speed; set => walk_speed = value; }
    }
}

