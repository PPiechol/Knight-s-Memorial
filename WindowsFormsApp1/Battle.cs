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
        Bitmap Warrior_Image;
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

                if(Columns * Rows < Amount)
                {
                    Rows++;
                }

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
                Bitmap Item_Image = (Bitmap)Display.Scale(Selected_Item.Image, Selected_Item.Width, Selected_Item.Height);
                Bitmap New_Background = Redraw_Held_Item_Under(Warrior_Image, Item_Image);

                if (Main_Hand == 2)
                {
                    New_Background = Redraw_Held_Item_Above(Warrior_Image, Item_Image);
                }

                Our_team[0].Get_Creature.Image = New_Background;
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
                PictureBox Held_Item = new PictureBox();
                Held_Item.Size = Selected_Item.Size;
                Held_Item.Image = Selected_Item.Image;

                creature.Name = Our_team.Count.ToString() + team;
                Our_team.Add(temp);
                PictureBox Warrior = Our_team[0].Get_Creature;

                Warrior_Image = (Bitmap)Display.Scale(Warrior.Image, 7 * Warrior.Width / 10, Warrior.Height);
                Bitmap Item_Image = (Bitmap)Display.Scale(Held_Item.Image, Held_Item.Width, Held_Item.Height);
                Bitmap New_Background = Redraw_Held_Item_Under(Warrior_Image, Item_Image);

                Warrior.Image = New_Background;
                Warrior.Size = New_Background.Size;
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

        private Bitmap Redraw_Held_Item_Under(Bitmap Warrior_Image, Bitmap Item_Image)
        {
            Bitmap New_Background = new Bitmap((4 * Warrior_Image.Width / 5) + Item_Image.Width, Warrior_Image.Height);
            for (int y = 0; y < New_Background.Height; y++)
            {
                for (int x = 0; x < New_Background.Width; x++)
                {
                    New_Background.SetPixel(x, y, Color.Transparent);
                }
            }
            for (int y = 0; y < Item_Image.Height; y++)
            {
                for (int x = 0; x < Item_Image.Width; x++)
                {
                    New_Background.SetPixel(x + (4 * Warrior_Image.Width / 5), y + (17 * Warrior_Image.Height / 20) - Item_Image.Height, Item_Image.GetPixel(x, y));
                }
            }
            for (int y = 0; y < Warrior_Image.Height; y++)
            {
                for (int x = 0; x < Warrior_Image.Width; x++)
                {
                    Color Pixel_Color = Warrior_Image.GetPixel(x, y);
                    if (Pixel_Color != Color.FromArgb(0, 0, 0, 0))
                    {
                        New_Background.SetPixel(x, y, Pixel_Color);
                    }
                }
            }

            return New_Background;
        }

        private Bitmap Redraw_Held_Item_Above(Bitmap Warrior_Image, Bitmap Item_Image)
        {
            Bitmap New_Background = new Bitmap((4 * Warrior_Image.Width / 5) + Item_Image.Width, Warrior_Image.Height);
            for (int y = 0; y < New_Background.Height; y++)
            {
                for (int x = 0; x < New_Background.Width; x++)
                {
                    New_Background.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0));
                }
            }
            for (int y = 0; y < Item_Image.Height; y++)
            {
                for (int x = 0; x < Item_Image.Width; x++)
                {
                    New_Background.SetPixel(x + (3 * Warrior_Image.Width / 5), y + (9 * Warrior_Image.Height / 10) - Item_Image.Height, Item_Image.GetPixel(x, y));
                }
            }
            for (int y = 0; y < Warrior_Image.Height; y++)
            {
                for (int x = 0; x < Warrior_Image.Width; x++)
                {;
                    if (New_Background.GetPixel(x,y) == Color.FromArgb(0, 0, 0, 0))
                    {
                        New_Background.SetPixel(x, y, Warrior_Image.GetPixel(x, y));
                    }
                }
            }

            return New_Background;
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
            if(CheckBattleResult())
            {
                Turn.Text = "TURA PRZECIWNIKÓW";
                Turn.Location = new Point((Background.Width - Turn.Width) / 2, Turn.Location.Y);
                AttackWithDelay(OpponentAttack,1000);
            }
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
                    foreach (Weapon_Effects Effect_Solo in Effects_Found_Solo)
                    {
                        Weapon_Effects Effect_clone = new Weapon_Effects();
                        Effect_clone.Id_Ability = Effect_Solo.Id_Ability;
                        Effect_clone.Strength = Effect_Solo.Strength;
                        Effect_clone.Duration = Effect_Solo.Duration;
                        Effect_clone.Effect_Id = Effect_Solo.Effect_Id;

                        var Effect_Chance = from WE in Edc.Weapon_Effects
                                            let EN = WE.Effects
                                            where EN.Id == Effect_clone.Effect_Id
                                            select EN;
                        foreach(Effects Type in Effect_Chance)
                        {
                            bool effect_on_list = false;
                            foreach(Weapon_Effects check in Opponents[Target_Id].Effects)
                            {
                                if(check.Effect_Id == Effect_clone.Effect_Id)
                                {
                                    check.Duration = Effect_clone.Duration;
                                    effect_on_list = true;
                                    break;
                                }
                            }
                            if(!effect_on_list)
                            {
                                if(Type.Name == "Stun")
                                {
                                    if (Chance.Next() % 100 < Effect_clone.Strength)
                                    {
                                        Effects_To_Apply_Solo.Add(Effect_clone);
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
                        foreach (Weapon_Effects Effect_Multi in Effects_Found_Multi)
                        {
                            Weapon_Effects Effect_clone = new Weapon_Effects();
                            Effect_clone.Id_Ability = Effect_Multi.Id_Ability;
                            Effect_clone.Strength = Effect_Multi.Strength;
                            Effect_clone.Duration = Effect_Multi.Duration;
                            Effect_clone.Effect_Id = Effect_Multi.Effect_Id;

                            Effects_To_Apply_Multi.Add(Effect_clone);
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
                            Weapon_Effects Effect_clone = new Weapon_Effects();
                            Effect_clone.Id_Ability = Effect.Id_Ability;
                            Effect_clone.Strength = Effect.Strength;
                            Effect_clone.Duration = Effect.Duration;
                            Effect_clone.Effect_Id = Effect.Effect_Id;
                            Effects_To_Apply.Add(Effect_clone);
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
            if(Opponents[0].AbleToAttack && Opponents[0].Get_health > 0)
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
            if(CheckBattleResult())
            {
                UpdateEffects();
                Performing_Attack = false;
                Turn.Text = "TWOJA TURA";
                Turn.Location = new Point((Background.Width - Turn.Width) / 2, Turn.Location.Y);
            }
        }

        private void UpdateEffects()
        {
            Entity Selected_entity;
            for(int i = 0; i < Our_team.Count; i++)
            {
                foreach (Weapon_Effects Effect in Our_team[i].Effects.ToList())
                {
                    Effect.Duration--;
                    if(Effect.Duration == 0)
                    {
                        Selected_entity = Our_team[i];
                        Selected_entity.Effects.Remove(Effect);
                        Our_team[i].Effects = Selected_entity.Effects;
                    }
                }
            }
            for (int i = 0; i < Opponents.Count; i++)
            {
                foreach (Weapon_Effects Effect in Opponents[i].Effects.ToList())
                {
                    Effect.Duration--;
                    if (Effect.Duration == 0)
                    {
                        Selected_entity = Opponents[i];
                        Selected_entity.Effects.Remove(Effect);
                        Opponents[i].Effects = Selected_entity.Effects;
                    }
                }
            }
        }

        public bool CheckBattleResult()
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

                foreach (Entity Heros in Our_team)
                {
                    Heros.Get_Creature.Enabled = false;
                }
                Background.Controls.Add(napisik);
                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (sender, args) =>
                {
                    napisik.Hide();
                    if(playerLost)
                    {
                        Application.Exit();
                    }
                    temp.stopBattle();
                    timer.Stop();
                };
                timer.Start();
                return false;
            }
            return true;
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
                    Label To_delete = (obj as Label);
                    To_delete.Visible = false;
                    Background.Controls.Remove(To_delete);
                }
                else if (obj is PictureBox)
                {
                    PictureBox To_delete = (obj as PictureBox);
                    To_delete.Visible = false;
                    Background.Controls.Remove(To_delete);
                }
            }
            Background.Refresh();
        }
    }
}
