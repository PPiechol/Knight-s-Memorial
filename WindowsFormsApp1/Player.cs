using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Player
    {
        PictureBox Character;
        int health = 100;
        int damage = 2;
        int attackSpeed = 5;
        int criticHitChance = 10;
        int coins = 0;
        public Player(PictureBox Character, int damage, int health, int attackSpeed, int criticHitChance)
        {
            this.Character = Character;
            this.damage = damage;
            this.health = health;
            this.attackSpeed = attackSpeed;
            this.criticHitChance = criticHitChance;
        }

        public int Health 
        {
            get 
            { 
                return health; 
            } 
        }
        public int Damage
        {
            get
            {
                return damage;
            }
        }
        public int AttackSpeed
        { 
            get 
            { 
                return attackSpeed; 
            } 
        }
        public int CriticHitChance 
        {
            get 
            { 
                return criticHitChance; 
            }
        }

        public PictureBox Character1 
        { 
            get
            {
                return Character;
            } 
        }

        public int Coins 
        { 
           get 
            {
                return coins; 
            } 
        }
    }
}
