﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        bool Entity_editor;
        public Form2()
        {
            InitializeComponent();
        }

        private void changeMap(object sender, EventArgs e)
        {
            try
            {
                pictureBox_display.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level 1\\Map " + numericUpDown_Map_Y.Value + "-" + numericUpDown_Map_X.Value + ".png");
                pictureBox_display.Image = Display.Scale(pictureBox_display.Image, pictureBox_display.Size.Width, pictureBox_display.Size.Height);
            }
            catch
            {
                if((sender as NumericUpDown) == numericUpDown_Map_Y)
                {
                    numericUpDown_Map_Y.Value--;
                }
                else
                {
                    numericUpDown_Map_X.Value--;
                }
                MessageBox.Show("Map not found");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox_display.Size = new Size(this.Height, this.Height);
            pictureBox_display.Location = new Point((this.Width - pictureBox_display.Size.Width) / 2, 0);
            pictureBox_display.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level 1\\Map " + numericUpDown_Map_Y.Value + "-" + numericUpDown_Map_X.Value + ".png");
            pictureBox_display.Image = Display.Scale(pictureBox_display.Image, pictureBox_display.Size.Width, pictureBox_display.Size.Height);
            pictureBox_display.BackColor = Color.Red;

            add_button.Location = new Point(this.Width - add_button.Width - 15, 15);

            Entity_editor = true;

            //dla monet i broni
            Label CW_label = new Label();
            CW_label.Location = new Point(label_range.Location.X, label_range.Location.Y);
            CW_label.Text = "Wartość Monety";
            groupBox_Inside.Controls.Add(CW_label);
            CW_label.Visible = false;

            NumericUpDown CW_value_nup = new NumericUpDown();
            CW_value_nup.Value = 10;
            CW_value_nup.Minimum = 0;
            CW_value_nup.Maximum = 999;
            CW_value_nup.Location = new Point(numericUpDown_range.Location.X, numericUpDown_range.Location.Y);
            groupBox_Inside.Controls.Add(CW_value_nup);
            CW_value_nup.Visible = false;

            //dla przeciwnników
            Label name_label = new Label();
            name_label.Location = new Point(label_range.Location.X, label_range.Location.Y);
            name_label.Text = "Nazwa";
            groupBox_Inside.Controls.Add(name_label);

            TextBox name_tb = new TextBox();
            name_tb.Location = new Point(numericUpDown_range.Location.X, numericUpDown_range.Location.Y);
            name_tb.Size = new Size(numericUpDown_size.Width, name_tb.Size.Height);
            groupBox_Inside.Controls.Add(name_tb);

            Label health_label = new Label();
            health_label.Location = new Point(name_label.Location.X, name_label.Location.Y + 27);
            health_label.Text = "Max życie";
            groupBox_Inside.Controls.Add(health_label);

            NumericUpDown health_nup = new NumericUpDown();
            health_nup.Value = 10;
            health_nup.Minimum = 0;
            health_nup.Maximum = 999;
            health_nup.Name = "Health";
            health_nup.Location = new Point(name_tb.Location.X, name_tb.Location.Y + 27);
            groupBox_Inside.Controls.Add(health_nup);

            Label damage_label = new Label();
            damage_label.Location = new Point(health_label.Location.X, health_label.Location.Y + 27);
            damage_label.Text = "Obrażenia";
            groupBox_Inside.Controls.Add(damage_label);

            NumericUpDown damage_nup = new NumericUpDown();
            damage_nup.Value = 1;
            damage_nup.Minimum = 0;
            damage_nup.Maximum = 999;
            damage_nup.Name = "Damage";
            damage_nup.Location = new Point(health_nup.Location.X, health_nup.Location.Y + 27);
            groupBox_Inside.Controls.Add(damage_nup);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            numericUpDown_Y.Value = Cursor.Position.Y;
            numericUpDown_X.Value = Cursor.Position.X - pictureBox_display.Location.X;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            //dodawanie nowych kluczy do App.config
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            int enemyIDCounter = 0;
            int coinIDCounter = 0;
            int weaponIDCounter = 0;
            var Object_list = xmlDoc.SelectNodes("//Map_1_Data/Map_Objects");

            foreach(XmlElement Node_object in Object_list)
            {
                foreach(XmlNode Single_Node in Node_object)
                {
                    if(Single_Node.InnerText == "")
                    {
                        string[] Node_values = Single_Node.Attributes["value"].Value.Split(',');
                        if(Node_values[6] == "Coin")
                        {
                            coinIDCounter++;
                        }
                        else if (Node_values[6] == "Weapon")
                        {
                            weaponIDCounter++;
                        }
                        else
                        {
                            enemyIDCounter++;
                        }
                    }
                }
            }
            
            var nodeRegion = xmlDoc.CreateElement("add");
            string enemy_name = "Enemy" + enemyIDCounter.ToString();
            string coin_name = "Coin" + coinIDCounter.ToString();
            string weapon_name = "Weapon" + weaponIDCounter.ToString();
            string value = "";
            string key = "";
            //sprawdzanie poszczególnych opcji
            if (radioButtonCoin.Checked)
            {
                key = coin_name;
                int coin_value = 0;
                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is NumericUpDown)
                    {
                        NumericUpDown temp = obiekt as NumericUpDown;
                        if (temp.Visible)
                        {
                            coin_value = (int)temp.Value;
                        }
                    }
                }
                value = numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
                    + numericUpDown_range.Value + "," + numericUpDown_size.Value + ",Coin," + coin_value;
                coinIDCounter++;
            }
            else if (radioButtonEnemy.Checked)
            {
                key = enemy_name;
                string source = "";
                int health = 0;
                int damage = 0;
                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is NumericUpDown)
                    {
                        NumericUpDown temp = obiekt as NumericUpDown;
                        if (temp.Visible && temp.Name == "Health")
                        {
                            health = (int)temp.Value;
                        }
                        else if (temp.Visible && temp.Name == "Damage")
                        {
                            damage = (int)temp.Value;
                        }

                    }
                    else if(obiekt is TextBox)
                    {
                        TextBox temp = obiekt as TextBox;
                        source = temp.Text;
                    }
                }

                //Test czy istnieje plik z przeciwnikiem
                bool Enemy_exist = false;
                string[] file_list = Directory.GetFiles(Environment.CurrentDirectory + "\\Map parts\\Level 1\\");
                foreach(string file in file_list)
                {
                    if(file == Environment.CurrentDirectory + "\\Map parts\\Level 1\\" + source + ".png")
                    {
                        Enemy_exist = true;
                        break;
                    }
                }
                if(!Enemy_exist)
                {
                    MessageBox.Show("Brak pliku graficznego odpowiadającego nazwie");
                    return;
                }
                value = numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
                     + numericUpDown_size.Value + "," + numericUpDown_range.Value + "," + source + "," + damage + "," + health;
                enemyIDCounter++;
            }
            else
            {
                EquipmentDataContext Edc = new EquipmentDataContext();
                key = weapon_name;
                int weapon_id = 0;
                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is NumericUpDown)
                    {
                        NumericUpDown temp = obiekt as NumericUpDown;
                        if (temp.Visible)
                        {
                            weapon_id = (int)temp.Value;
                        }
                    }
                }

                //Test czy w bazie danych istnieje przedmiot o podanym id
                var Held = from Item in Edc.Items
                           where Item.Id == weapon_id
                           select Item;
                if(Held == null)
                {
                    MessageBox.Show("Brak przedmiotu o podanym id");
                    return;
                }
                value = numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
                    + numericUpDown_range.Value + "," + numericUpDown_size.Value + ",Weapon," + weapon_id;
                weaponIDCounter++;
            }

            //dodaj klucz i wartość
            nodeRegion.SetAttribute("key", key);
            nodeRegion.SetAttribute("value", value);

            xmlDoc.SelectSingleNode("//Map_1_Data/Map_Objects").AppendChild(nodeRegion);
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            ConfigurationManager.RefreshSection("Map_1_Data/Map_objects");

            File.Copy(Environment.CurrentDirectory + "\\WindowsFormsApp1.exe.config", Environment.CurrentDirectory.Replace("\\bin\\Debug", "") + "\\App.config", true);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEnemy.Checked)
            {
                if(!Entity_editor)
                {
                    Swap_Visible();
                    Entity_editor = true;
                }
            }
            else if (radioButtonCoin.Checked)
            {
                if (Entity_editor)
                {
                    Swap_Visible();
                    Entity_editor = false;
                }
                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is Label)
                    {
                        Label temp = obiekt as Label;
                        if (temp.Visible)
                        {
                            temp.Text = "Wartość Monety";
                        }
                    }
                }
            }
            else
            {
                if (Entity_editor)
                {
                    Swap_Visible();
                    Entity_editor = false;
                }
                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is Label)
                    {
                        Label temp = obiekt as Label;
                        if(temp.Visible)
                        {
                            temp.Text = "Id broni";
                        }
                    }
                }
            }
        }

        private void Swap_Visible()
        {
            foreach (object obiekt in groupBox_Inside.Controls)
            {
                if (obiekt is Label)
                {
                    Label temp = obiekt as Label;
                    temp.Visible = !temp.Visible;
                }
                else if (obiekt is NumericUpDown)
                {
                    NumericUpDown temp = obiekt as NumericUpDown;
                    temp.Visible = !temp.Visible;
                }
                else if (obiekt is TextBox)
                {
                    TextBox temp = obiekt as TextBox;
                    temp.Visible = !temp.Visible;
                }
            }
        }
    }
    
}