using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Battle
    {
        PictureBox Background;
        List<Entity> Our_team;
        List<Entity> Opponents;
        public Battle(PictureBox Background)
        {
            this.Background = Background;
            Our_team = new List<Entity>();
            Opponents = new List<Entity>();
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
        //Our_team = new List<Entity>();
    }
}
