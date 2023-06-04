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
        EquipmentDataContext Edc = new EquipmentDataContext();
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
                Able_To_Attack = true;
                creature.Controls.Clear();
                if(Current_Effects.Count > 0)
                {
                    FlowLayoutPanel Show_effect = new FlowLayoutPanel();
                    int Icon_Size = 48;
                    int Offset = 4;
                    Show_effect.Width = Offset + 2 *(Offset + Icon_Size); // 4 48 4 48 4
                    foreach (Weapon_Effects temp in Current_Effects)
                    {
                        var Effect_Chance = from WE in Edc.Weapon_Effects
                                            let EN = WE.Effects
                                            where EN.Id == temp.Effect_Id
                                            select EN;
                        foreach (Effects Type in Effect_Chance)
                        {
                            PictureBox icon = new PictureBox();
                            icon.SizeMode = PictureBoxSizeMode.Zoom;
                            icon.Size = new Size(Icon_Size,Icon_Size);
                            icon.Image = Image.FromFile(Environment.CurrentDirectory + "\\Effects icons\\" + Type.Name.ToString() + ".png");
                            Show_effect.Controls.Add(icon);
                            if (Type.Name == "Stun")
                            {
                                Able_To_Attack = false;
                            }
                        }
                    }
                    creature.Controls.Add(Show_effect);
                }
                creature.Refresh();
            }
        }

        public int Walk_speed { get => walk_speed; set => walk_speed = value; }
    }
}

