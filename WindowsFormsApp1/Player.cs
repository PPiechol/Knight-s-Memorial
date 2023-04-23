using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Player
    {
        PictureBox Character;
        Label heroLevel;
        Label heroHealth;
        Label heroCrit;
        Label heroCoins;
        Label heroAttackSpeed;
        Label Coinstext;
        Label crittext;
        Label Leveltext;
        Label Healthtext;
        Label AttackSpeedtext;
        int health;
        int attackSpeed;
        int critHitChance;
        int coins;
        int level;
        int exp;


        public Player(PictureBox Character, int level, int health, int attackSpeed, int critHitChance, int coins, Panel panelBottom, int exp=0)
        {
            this.Character = Character;
            this.level = level;
            this.health = health;
            this.attackSpeed = attackSpeed;
            this.critHitChance = critHitChance;
            this.coins = coins;


            

            heroLevel = new Label();
            PictureBox levelPictureBox = new PictureBox();
            
            levelPictureBox.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\levelIcon.png");
            levelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            heroLevel.Controls.Add(levelPictureBox);
            heroLevel.Size = new Size(levelPictureBox.Width, levelPictureBox.Height);
            heroLevel.BackColor = Color.Transparent;
            panelBottom.Controls.Add(heroLevel);

            
            heroHealth = new Label();
            PictureBox healthPictureBox = new PictureBox();
            healthPictureBox.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\healthIcon.png");
            healthPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            heroHealth.Controls.Add(healthPictureBox);
            heroHealth.Size = new Size(healthPictureBox.Width, healthPictureBox.Height);
            heroHealth.BackColor = Color.Transparent;
            panelBottom.Controls.Add(heroHealth);

            heroAttackSpeed = new Label();
            PictureBox attackSpeedPictureBox = new PictureBox();
            attackSpeedPictureBox.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\attackSpeedIcon.png");
            attackSpeedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            heroAttackSpeed.Controls.Add(attackSpeedPictureBox);
            heroAttackSpeed.Size = new Size(attackSpeedPictureBox.Width, attackSpeedPictureBox.Height);
            heroAttackSpeed.BackColor = Color.Transparent;
            panelBottom.Controls.Add(heroAttackSpeed);

            heroCrit = new Label();
            PictureBox critPictureBox = new PictureBox();

            critPictureBox.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\critIcon.png");
            critPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            heroCrit.Controls.Add(critPictureBox);
            heroCrit.Size = new Size(critPictureBox.Width, critPictureBox.Height);
            heroCrit.BackColor = Color.Transparent;
            panelBottom.Controls.Add(heroCrit);

            heroCoins = new Label();
            PictureBox coinPictureBox = new PictureBox();

            coinPictureBox.Image = Image.FromFile(Environment.CurrentDirectory + "\\Icons\\coinIcon.png");
            coinPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            heroCoins.Controls.Add(coinPictureBox);
            heroCoins.Size = new Size(coinPictureBox.Width, coinPictureBox.Height);
            heroCoins.BackColor = Color.Transparent;
            heroCoins.Margin = new Padding(0, 10, 0, 10);
            panelBottom.Controls.Add(heroCoins);


            Leveltext = new Label();
            Leveltext.Text = level.ToString();
            Leveltext.Font = new Font("Old English Text MT", 24, FontStyle.Bold);
            Leveltext.Top = 30;
            Leveltext.Size = new Size(Leveltext.Width, Leveltext.Height + 10);
            Leveltext.Left = 10;
            Leveltext.Location = new Point(10, 10);
            
            Leveltext.BackColor = Color.Transparent;
            Leveltext.TextAlign = ContentAlignment.BottomCenter;
            
            
            panelBottom.Controls.Add(Leveltext);

            Healthtext = new Label();
            Healthtext.Text = health.ToString();
            Healthtext.Font = new Font("Old English Text MT",24, FontStyle.Bold);
            Healthtext.Top = heroLevel.Bottom + 15;
            Healthtext.Left = 10;
            Healthtext.Size = new Size(Leveltext.Width, Leveltext.Height + 10);
            Healthtext.BackColor = Color.Transparent;
            Healthtext.TextAlign = ContentAlignment.MiddleCenter;
            panelBottom.Controls.Add(Healthtext);



            AttackSpeedtext = new Label();
            AttackSpeedtext.Text = attackSpeed.ToString();
            AttackSpeedtext.Font = new Font("Old English Text MT", 24, FontStyle.Bold);
            AttackSpeedtext.Top = heroHealth.Bottom + 15;
            AttackSpeedtext.Left = 10;
            AttackSpeedtext.Size = new Size(Leveltext.Width, Leveltext.Height + 10);
            AttackSpeedtext.TextAlign = ContentAlignment.MiddleCenter;
            AttackSpeedtext.BackColor = Color.Transparent;
            panelBottom.Controls.Add(AttackSpeedtext);

            crittext = new Label();
            crittext.Text = critHitChance.ToString();
            crittext.Font = new Font("Old English Text MT", 24, FontStyle.Bold);
            crittext.Size = new Size(Leveltext.Width, Leveltext.Height + 10);
            crittext.Top = heroAttackSpeed.Bottom + 15;
            crittext.Left = 10;
            crittext.TextAlign = ContentAlignment.MiddleCenter;
            crittext.BackColor = Color.Transparent;
            panelBottom.Controls.Add(crittext);

            Coinstext = new Label();
            Coinstext.Text = coins.ToString();
            Coinstext.Font = new Font("Old English Text MT", 24, FontStyle.Bold);
            Coinstext.Top = heroCrit.Bottom + 15;
            Coinstext.Left = 10;
            Coinstext.Size = new Size(Leveltext.Width, Leveltext.Height + 10);

            Coinstext.TextAlign = ContentAlignment.MiddleCenter;
            Coinstext.BackColor = Color.Transparent;
            panelBottom.Controls.Add(Coinstext);
            
            panelBottom.Refresh();
            AddCoins(10);
        }
        
        public void AddCoins(int amount)
        {
            coins += amount;
            Coinstext.Text = coins.ToString();
        }
        public void AddExp(int amount)
        {
            exp += amount;
            CheckHeroLvl(exp);
        }
        private void CheckHeroLvl(int expGained)
        {
            exp += expGained;
            if(exp >= level * 10)
            {
                level++;
                Leveltext.Text = level.ToString();
                exp = 0;
            }
            
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