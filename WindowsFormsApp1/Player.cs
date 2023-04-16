using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Player
    {
        PictureBox Character;

        int health;
        int attackSpeed;
        int critHitChance;
        int coins;
        int level;



        public Player(PictureBox Character, int level, int health, int attackSpeed, int critHitChance, int coins, Panel panelLeft)
        {
            this.Character = Character;
            this.level = level;
            this.health = health;
            this.attackSpeed = attackSpeed;
            this.critHitChance = critHitChance;
            this.coins = coins;

            Label Level = new Label();
            Level.Text = "Level: " + level;
            Level.Font = new Font("Arial", 24, FontStyle.Bold);
            Level.Top = 15;
            Level.Left = 10;
            Level.Location = new Point(10, 10);
            Level.AutoSize = true;
            
            
            panelLeft.Controls.Add(Level);

            Label Health = new Label();
            Health.Text = "Health: " + health;
            Health.Font = new Font("Arial", 24, FontStyle.Bold);
            Health.Top = Level.Bottom + 15;
            Health.Left = 10;
            Health.AutoSize = true;
            panelLeft.Controls.Add(Health);



            Label AttackSpeed = new Label();
            AttackSpeed.Text = "Attack Speed: " + attackSpeed;
            AttackSpeed.Font = new Font("Arial", 24, FontStyle.Bold);
            AttackSpeed.Top = Health.Bottom + 15;
            AttackSpeed.Left = 10;
            AttackSpeed.AutoSize = true;
            panelLeft.Controls.Add(AttackSpeed);

            Label crit = new Label();
            crit.Text = "Critic: " + critHitChance + "%";
            crit.Font = new Font("Arial", 24, FontStyle.Bold);
            crit.AutoSize = true;
            crit.Top = AttackSpeed.Bottom + 15;
            crit.Left = 10;
            panelLeft.Controls.Add(crit);

            Label Coins = new Label();
            Coins.Text = "Coins: " + coins;
            Coins.Font = new Font("Arial", 24, FontStyle.Bold);
            Coins.Top = crit.Bottom + 15;
            Coins.Left = 10;
            Coins.AutoSize = true;
            panelLeft.Controls.Add(Coins);
            panelLeft.Refresh();


        }
        public int Health
        {
            get
            {
                return health;
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
                return critHitChance;
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

        public int Level
        {
            get
            {
                return level;
            }
        }

    }
}