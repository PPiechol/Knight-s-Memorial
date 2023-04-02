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
        Label Turn;
        List<int> Inventory;

        PictureBox Selected_Item;
        Panel Show_Inventory;

        int Main_Hand;
        bool Performing_Attack;
        EquipmentDataContext Edc = new EquipmentDataContext();


        Display temp;
        public Battle(PictureBox Background, List<int> Inventory, Display temp)
        {
            this.temp = temp;
            this.Background = Background;
            this.Inventory = Inventory;
            Our_team = new List<Entity>();
            Opponents = new List<Entity>();
            Performing_Attack = false;

            int Icon_size = 100;
            Turn = new Label();
            Turn.AutoSize = true;
            Turn.Location = new Point((Background.Width - Turn.Width)/2, Background.Height / 4 - Turn.Height);
            Turn.Font = new Font("Fixedsys Regular", 20);
            Turn.Text = "TWOJA TURA";
            Turn.ForeColor = Color.Black;
            Turn.BackColor = Color.FromArgb(100, 255, 255, 255);
            Background.Controls.Add(Turn);
            Turn.Location = new Point((Background.Width - Turn.Width) / 2, Background.Height / 4 - Turn.Height);
            Selected_Item = new PictureBox();
            Selected_Item.Location = new Point(Background.Location.X + Background.Width / 8, Background.Width / 5 * 4); // ustawienie pozycji wyświetlania broni
            Selected_Item.Size = new Size(Icon_size, Icon_size);
            Selected_Item.BackColor = Color.FromArgb(100,0,0,0);
            Selected_Item.SizeMode = PictureBoxSizeMode.Zoom;

            int Amount = Inventory.Count;
            if(Amount == 0)
            {
                throw new InvalidOperationException("Inventory can't be empty!");
            }
            var First_Weapon = from Item in Edc.Items
                               where Item.Id == Inventory[0]
                               select Item;
            foreach (Items Querry in First_Weapon)
            {
                Selected_Item.Image = Image.FromFile(Environment.CurrentDirectory + "\\Items\\" + Querry.Name.ToString() + ".png");
            }
            Main_Hand = Inventory[0];

            if (Amount > 1)
            {
                int Offset = (int)Math.Round((double)Icon_size / 20);
                Show_Inventory = new Panel();
                
                int Columns = (int)Math.Ceiling(Math.Sqrt(Amount));
                int Rows = (int)Math.Round((double)Amount / Columns);
                
                Show_Inventory.Width = Offset + (Offset + Icon_size) * Columns;
                Show_Inventory.Height = Offset + (Offset + Icon_size) * Rows;
                Show_Inventory.BackColor = Color.FromArgb(100, 0, 0, 0);
                Show_Inventory.Location = new Point(Selected_Item.Location.X - Show_Inventory.Width + Icon_size, Selected_Item.Location.Y - Show_Inventory.Height + Icon_size);

                int Row_pos = 0;
                int Column_pos = 0;

                for(int i = 0; i < Amount; i++)
                {
                    var Weapon = from Item in Edc.Items
                                 where Item.Id == Inventory[i]
                                 select Item;
                    foreach (Items Querry in Weapon)
                    {
                        if (Column_pos == Columns)
                        {
                            Column_pos = 0;
                            Row_pos++;
                        }
                        Button Display_Item = new Button();
                        Display_Item.BackColor = Color.FromArgb(100, 0, 0, 0);
                        Display_Item.Image = Image.FromFile(Environment.CurrentDirectory + "\\Items\\" + Querry.Name.ToString() + ".png");
                        Display_Item.Size = new Size(Icon_size, Icon_size);
                        Display_Item.Location = new Point(Offset + (Offset + Icon_size) * Column_pos, Offset + (Offset + Icon_size) * Row_pos);
                        Display_Item.Name = Querry.Id.ToString();
                        Display_Item.Click += Display_Item_Click;
                        Show_Inventory.Controls.Add(Display_Item);
                        Column_pos++;
                    }
                }

                Show_Inventory.Visible = false;
                Background.Controls.Add(Show_Inventory);
                Selected_Item.Click += Selected_Item_Click;
            }
            Background.Controls.Add(Selected_Item);
        }

        private void Display_Item_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button Temp = (sender as Button);
                Show_Inventory.Visible = false;
                Selected_Item.Visible = true;
                Selected_Item.Image = Temp.Image;
                Main_Hand = Convert.ToInt32(Temp.Name);
            }
        }

        private void Selected_Item_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                Show_Inventory.Visible = true;
                Selected_Item.Visible = false;
            }
        }

        public void Add_entity(char team, int damage, int health, PictureBox source)
        {
            PictureBox creature = new PictureBox();
            creature.Image = source.Image;
            creature.Location = source.Location;
            creature.Size = source.Size;
            creature.SizeMode = source.SizeMode;
            creature.BackColor = source.BackColor;
            creature.Click += AttackButton_Click;
            
            Entity temp = new Entity(creature, damage, health);
            if(team == 'e')
            {
                creature.Name = Opponents.Count.ToString() + team;
                Opponents.Add(temp);
            }
            else if(team == 'p')
            {
                creature.Name = Our_team.Count.ToString() + team;
                Our_team.Add(temp);
            }
            
            Label healthLabel = new Label();
            healthLabel.Text = health.ToString();
            healthLabel.Location = new Point(creature.Location.X + (creature.Width/2), creature.Location.Y - 20);
            healthLabel.AutoSize = true;
            healthLabel.BackColor = Color.DarkRed;
            healthLabel.Font = new Font("System", 20);
            healthLabel.ForeColor = Color.WhiteSmoke;
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
            if(Performing_Attack)
            {
                return;
            }
            Performing_Attack = true;
            PictureBox pb = (sender as PictureBox);
            Perform_Action(pb);
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
            Turn.Text = "TURA PRZECIWNIKÓW";
            Turn.Location = new Point((Background.Width - Turn.Width) / 2, Turn.Location.Y);
            AttackWithDelay(OpponentAttack,1000);
        }
        
        private void Perform_Action(PictureBox Target)
        {
            var Held = from Item in Edc.Items
                               where Item.Id == Main_Hand
                               select Item;

            Random Chance = new Random();
            int Target_Id = Convert.ToInt32(Target.Name.Substring(0, 1));
            char Target_Team = Convert.ToChar(Target.Name.Substring(1, 1));

            foreach (Items Querry in Held)
            {
                if(Target_Team == 'e')
                {
                    var Effects_Found_Solo = from Item in Edc.Items
                                             from TOA in Item.Type_Of_Attack
                                             let WE = TOA.Weapon_Effects
                                             where (Item.Id == Main_Hand) && (TOA.Item_Type == 's')
                                             select WE;

                    List<Weapon_Effects> Effects_To_Apply_Solo = new List<Weapon_Effects>();
                    foreach (Weapon_Effects Effect in Effects_Found_Solo)
                    {
                        var Effect_Chance = from WE in Edc.Weapon_Effects
                                            let EN = WE.Effects
                                            where EN.Id == Effect.Effect_Id
                                            select EN;
                        foreach(Effects Type in Effect_Chance)
                        {
                            bool effect_on_list = false;
                            foreach(Weapon_Effects check in Opponents[Target_Id].Effects)
                            {
                                if(check.Effect_Id == Effect.Effect_Id)
                                {
                                    check.Duration = Effect.Duration;
                                    effect_on_list = true;
                                    break;
                                }
                            }
                            if(!effect_on_list)
                            {
                                if(Type.Name == "Stun")
                                {
                                    if (Chance.Next() % 100 < Effect.Strength)
                                    {
                                        Effects_To_Apply_Solo.Add(Effect);
                                    }
                                }
                            }
                        }
                    }

                    if (Querry.Type_Of_Action == 's')
                    {
                        if (Chance.Next() % 100 < (int)Querry.Main_Target_Proc_Chance)
                        {
                            int Damage = (int)Querry.Damage;
                            Opponents[Target_Id].Get_health -= Damage;
                            Opponents[Target_Id].Effects = Effects_To_Apply_Solo;
                        }
                    }
                    else if (Querry.Type_Of_Action == 'c')
                    {
                        var Effects_Found_Multi = from Item in Edc.Items
                                                 from TOA in Item.Type_Of_Attack
                                                  let WE = TOA.Weapon_Effects
                                                  where Item.Id == Main_Hand && TOA.Item_Type == 'c'
                                                 select WE;

                        List<Weapon_Effects> Effects_To_Apply_Multi = new List<Weapon_Effects>();
                        foreach (Weapon_Effects Effect in Effects_Found_Multi)
                        {
                            Effects_To_Apply_Multi.Add(Effect);
                        }
                        if (Chance.Next() % 100 < (int)Querry.Main_Target_Proc_Chance)
                        {
                            int Damage = (int)Querry.Damage;
                            Opponents[Target_Id].Get_health -= Damage;
                            Opponents[Target_Id].Effects = Effects_To_Apply_Solo;
                            if (Chance.Next() % 100 < (int)Querry.Other_Targets_Proc_Chance)
                            {
                                int Targets_Count = Opponents.Count;
                                int Next_Target = Target_Id + 1;
                                int Jumps = (int)Querry.Amount_Of_Jumps;

                                while (Jumps > 1)
                                {
                                    if (Next_Target >= Targets_Count)
                                    {
                                        Next_Target = 0;
                                    }

                                    if (Next_Target == Target_Id)
                                    {
                                        break;
                                    }
                                    Damage = Damage * (int)Querry.Damage_After_Jump / 100;
                                    Opponents[Next_Target].Get_health -= Damage;
                                    Opponents[Target_Id].Effects = Effects_To_Apply_Multi;
                                    Jumps--;
                                }
                            }
                        }
                    }
                }
                else if(Target_Team == 'p')
                {
                    if (Querry.Type_Of_Action == 'f')
                    {
                        var Support_Effects = from Item in Edc.Items
                                              from TOA in Item.Type_Of_Attack
                                              let WE = TOA.Weapon_Effects
                                              where Item.Id == Main_Hand && TOA.Item_Type == 'f'
                                              select WE;

                        List<Weapon_Effects> Effects_To_Apply = new List<Weapon_Effects>();
                        foreach (Weapon_Effects Effect in Support_Effects)
                        {
                            Effects_To_Apply.Add(Effect);
                        }
                        Our_team[Target_Id].Effects = Effects_To_Apply;
                    }
                }
            }
        }

        private void OpponentAttack()
        {
            var random = new Random();
            var target = random.Next(Our_team.Count);

            //tymczasowo
            int Damage = Opponents[0].Get_Damage;
            if(Opponents[0].Effects.Count == 0)
            {
                if (Our_team[0].Effects.Count > 0)
                {
                    Damage = Damage * (100 - (int)Our_team[0].Effects[0].Strength) / 100;
                }
                Our_team[target].Get_health -= Damage;
                foreach (Control control in Background.Controls)
                {
                    if (control is Label label && label.Tag is Entity entity && entity == Our_team[target])
                    {
                        label.Text = Our_team[target].Get_health.ToString();
                        Background.Refresh();
                        break;
                    }
                }
            }
            CheckBattleResult();
            UpdateEffects();
            Performing_Attack = false;
            Turn.Text = "TWOJA TURA";
            Turn.Location = new Point((Background.Width - Turn.Width) / 2, Turn.Location.Y);
        }

        private void UpdateEffects()
        {
            for(int i = 0; i < Our_team.Count; i++)
            {
                foreach (Weapon_Effects Effect in Our_team[i].Effects.ToList())
                {
                    if(Effect.Duration == 0)
                    {
                        Our_team[i].Effects.Remove(Effect);
                    }
                    Effect.Duration--;
                }
            }
            for (int i = 0; i < Opponents.Count; i++)
            {
                foreach (Weapon_Effects Effect in Opponents[i].Effects.ToList())
                {
                    if (Effect.Duration == 0)
                    {
                        Opponents[i].Effects.Remove(Effect);
                    }
                    Effect.Duration--;
                }
            }
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
                Selected_Item.Visible = false;
                Label napisik = new Label();
                napisik.Font = new Font("System", 40);

                napisik.AutoSize = true;
                napisik.BringToFront();
                if (playerLost)
                {
                    napisik.Location = new Point(Background.Width / 2, Background.Height / 2);
                    napisik.Size = new Size(Background.Width, Background.Height);
                    napisik.Text = "Przegrałeś!";
                    napisik.ForeColor = Color.Red;
                    Background.BackColor = Color.Gray;
                    temp.BackColor = Color.Gray;
                    Background.Image = null;
                    disappear();

                }
                else
                {
                    napisik.Location = new Point((Background.Width / 2) - napisik.Size.Width, 100);
                    napisik.Text = "Wygrałeś!";
                    napisik.ForeColor = Color.LightGoldenrodYellow;
                    Background.BackColor = Color.Green;
                    temp.BackColor = Color.Green;
                    disappear();
                }

                Background.Controls.Add(napisik);
                Timer timer = new Timer();
                timer.Interval = 5000;
                timer.Tick += (sender, args) =>
                {
                    napisik.Hide();
                    timer.Stop();
                    if(playerLost)
                    {
                        Application.Exit();
                    }
                    temp.stopBattle();
                };
                timer.Start();
            }
            
        }

        private void disappear()
        {
            foreach (Control control in Background.Controls)
            {
                if (control is Label label && label.Tag is Entity entity && entity == Our_team[0])
                {
                    label.Visible = false;
                }
            }
            foreach (Control obj in Background.Controls)
            {
                if (obj is Label)
                {
                    Background.Controls.Remove(obj as Label);
                }
                else if (obj is PictureBox)
                {
                    Background.Controls.Remove(obj as PictureBox);
                }
            }
        }

        //Our_team = new List<Entity>();
    }
}
