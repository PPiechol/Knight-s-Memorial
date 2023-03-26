using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    class Battle : Panel
    {
        PictureBox Background;
        List<Entity> Our_team;
        List<Entity> Opponents;
        Button AttackButton;

        public Battle(PictureBox Background)
        {
            this.Background = Background;
            Our_team = new List<Entity>();
            Opponents = new List<Entity>();
            AttackButton = new Button();
            AttackButton.Text = "Atak";
            AttackButton.Location = new Point(Background.Location.X + Background.Width / 8, Background.Width / 5 * 4); // ustawienie pozycji przycisku
            AttackButton.Size = new Size(100, 100);
            AttackButton.BackColor = Color.LightGray;
            AttackButton.Click += new EventHandler(AttackButton_Click);
            Background.Controls.Add(AttackButton);
        }


        

        public void Add_entity(char team, int damage, int health, PictureBox source)
        {
            PictureBox creature = new PictureBox();
            creature.Image = source.Image;
            creature.Location = source.Location;
            creature.Size = source.Size;
            creature.SizeMode = source.SizeMode;
            creature.BackColor = source.BackColor;

            Entity temp = new Entity(creature, damage, health);
            if(team == 'e')
            {
                Opponents.Add(temp);
            }
            else if(team == 'p')
            {
                Our_team.Add(temp);
            }
            
            Label healthLabel = new Label();
            healthLabel.Text = health.ToString();
            healthLabel.Location = new Point(creature.Location.X + (creature.Width/2), creature.Location.Y - 20);
            healthLabel.AutoSize = true;
            healthLabel.BackColor = Color.Transparent;
            healthLabel.Font = new Font("System", 20);
            healthLabel.ForeColor = Color.WhiteSmoke;
            healthLabel.BackColor = Color.DarkRed;
            healthLabel.Tag = temp;
            Background.Controls.Add(healthLabel);
        }

        public List<Entity> Get_Our_team
        {
            get 
            {
                return Our_team;
            }
        }

        public List<Entity> Get_Opponents
        {
            get
            {
                return Opponents;
            }
        }

        public PictureBox Get_Background
        {
            get
            {
                return Background;
            }
        }
        
        private void AttackWithDelay(Action action, int delay)
        {
            var timer = new Timer { Interval = delay };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                action.Invoke();
            };
            timer.Start();
        }
        private void AttackButton_Click(object sender, EventArgs e)
        {
            Opponents[0].Get_health -= Our_team[0].Get_Damage;
            // aktualizacja etykiety healthLabel
            foreach (Control control in Background.Controls)
            {
                if (control is Label label && label.Tag is Entity entity && entity == Opponents[0])
                {
                    label.Text = Opponents[0].Get_health.ToString();
                    Background.Refresh();
                    break;
                }
            }
            CheckBattleResult();
            AttackButton.Enabled = false;
            AttackWithDelay(OpponentAttack,1000);
        }
        
        
        
        private void OpponentAttack()
        {
            var random = new Random();
            var target = random.Next(Our_team.Count);

            Our_team[target].Get_health -= Opponents[0].Get_Damage;
            foreach (Control control in Background.Controls)
            {
                if (control is Label label && label.Tag is Entity entity && entity == Our_team[target])
                {
                    label.Text = Our_team[target].Get_health.ToString();
                    Background.Refresh();
                    break;
                }
                
            }
            CheckBattleResult();
            AttackButton.Enabled = true;
        }
        
        public void CheckBattleResult()
        {
            bool playerLost = true;
            bool opponentLost = true;
            foreach (Entity entity in Our_team)
            {
                if (entity.Get_health > 0)
                {
                    playerLost = false;
                    break;
                }
            }
            foreach (Entity entity in Opponents)
            {
                if (entity.Get_health > 0)
                {
                    opponentLost = false;
                    break;
                }
            }

            if (playerLost || opponentLost)
            {

                Label prompt = new Label();
                prompt.Location = new Point(Background.Location.X + (Background.Width / 3),
                                            Background.Height / 4);
                prompt.ForeColor = Color.Azure;
                prompt.BackColor = Color.Transparent;
                prompt.Font = new Font("System", 50);
                if (playerLost)
                {
                    prompt.Text = "Przegrałeś!";
                }
                else
                {
                    prompt.Text = "Wygrałeś!";
                }

                prompt.AutoSize = true;
                prompt.BringToFront();
                Background.Controls.Add(prompt);
                Background.Dispose();
            }
            
        }

        

        //Our_team = new List<Entity>();
    }
}
