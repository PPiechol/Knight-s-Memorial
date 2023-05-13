﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        Form source;
        bool Entity_editor;
        List<Map_Object> Objects;
        Map_Object Selected_Object;
        int Current_Level;

        public Form2(Form source)
        {
            InitializeComponent();
            this.source = source;
        }

        private void changeMap(object sender, EventArgs e)
        {
            try
            {
                pictureBox_display.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level 1\\Map " + numericUpDown_Map_X.Value + "-" + numericUpDown_Map_Y.Value + ".png");
                pictureBox_display.Image = Display.Scale(pictureBox_display.Image, pictureBox_display.Size.Width, pictureBox_display.Size.Height);
                Show_Objects();
            }
            catch
            {
                if((sender as NumericUpDown) == numericUpDown_Map_Y)
                {
                    if(numericUpDown_Map_Y.Value == 0)
                    {
                        numericUpDown_Map_Y.Value++;
                    }
                    else
                    {
                        numericUpDown_Map_Y.Value--;
                    }
                }
                else
                {
                    if (numericUpDown_Map_X.Value == 0)
                    {
                        numericUpDown_Map_X.Value++;
                    }
                    else
                    {
                        numericUpDown_Map_X.Value--;
                    }
                }
                MessageBox.Show("Mapa nie istnieje");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Current_Level = 1;
            Objects = new List<Map_Object>();

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


            string SectionName = "Map_" + Current_Level.ToString() + "_Data/Map_Objects";
            Retrive_Map_Objects(SectionName, Current_Level, Objects);
            foreach (Map_Object MO in Objects)
            {
                MO.Click += Map_Object_Click;
            }
            Show_Objects();
        }

        private void Retrive_Map_Objects(string SectionName, int Current_Level, List<Map_Object> Object_list)
        {
            var ApplicationConfig = ConfigurationManager.GetSection(SectionName) as NameValueCollection;
            foreach (var key in ApplicationConfig.AllKeys)
            {
                string[] Object_Data = ApplicationConfig[key].Split(',');
                int[] Int_data = new int[6];
                int i = 0;
                foreach (string Int_num in Object_Data)
                {
                    Int_data[i] = Convert.ToInt32(Int_num);
                    i++;
                    if (i == Int_data.Length)
                    {
                        break;
                    }
                }

                if (Object_Data[6] == "Coin")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], Convert.ToInt32(Object_Data[7]));
                    Object_list.Add(new Map_Object(New_Object,key));
                }
                else if (Object_Data[6] == "Weapon")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], Convert.ToInt32(Object_Data[7]));
                    Object_list.Add(new Map_Object(New_Object, key));
                }
                else if (Object_Data[6].Length >= 5 && Object_Data[6].Substring(0, 5) == "Enemy")
                {
                    Interactive_Object New_Object = new Interactive_Object(Current_Level, Int_data[0], Int_data[1], Int_data[2],
                                                        Int_data[3], Int_data[4], Int_data[5],
                                                        Object_Data[6], null);
                    Entity Monster = new Entity(New_Object.Get_Icon, Convert.ToInt32(Object_Data[7]), Convert.ToInt32(Object_Data[8]));
                    New_Object.Get_Type = Monster;
                    Object_list.Add(new Map_Object(New_Object, key));
                }
            }
        }

        private void Map_Object_Click(object sender, EventArgs e)
        {
            Map_Object temp = null;
            if (sender is Map_Object)
            {
                temp = (sender as Map_Object);
            }
            else
            {
                return;
            }

            if(Selected_Object == temp)
            {
                Selected_Object.Image = (Bitmap)Selected_Object.Get_Object.Get_Icon.Image.Clone();
                Selected_Object.Refresh();

                Selected_Object = null;
                add_button.Text = "Dodaj Obiekt";
            }
            else
            {
                if(Selected_Object != null)
                {
                    Selected_Object.Image = (Bitmap)Selected_Object.Get_Object.Get_Icon.Image.Clone();
                    Selected_Object.Refresh();
                }
                Selected_Object = temp;

                Graphics g = Graphics.FromImage(Selected_Object.Image);
                g.DrawRectangle(new Pen(Color.Green, 10), 0, 0, Selected_Object.Image.Width - 1, Selected_Object.Image.Height - 1);
                Selected_Object.Refresh();

                add_button.Text = "Zapisz Obiekt";
            }
            Load_Object_Data();
        }

        private void Load_Object_Data()
        {
            if(Selected_Object != null)
            {
                numericUpDown_X.Value = Selected_Object.Get_Object.Get_Pos_X;
                numericUpDown_Y.Value = Selected_Object.Get_Object.Get_Pos_Y;
                numericUpDown_size.Value = Selected_Object.Get_Object.Get_Icon.Size.Width;
                numericUpDown_range.Value = Selected_Object.Get_Object.Get_Range;
                if(Selected_Object.Get_Object.Get_Type is Entity)
                {
                    if(!Entity_editor)
                    {
                        radioButtonEnemy.Checked = true;
                    }

                    foreach (object obiekt in groupBox_Inside.Controls)
                    {
                        if (obiekt is NumericUpDown)
                        {
                            NumericUpDown temp = obiekt as NumericUpDown;
                            if (temp.Visible && temp.Name == "Health")
                            {
                                temp.Value = (Selected_Object.Get_Object.Get_Type as Entity).Get_health;
                            }
                            else if (temp.Visible && temp.Name == "Damage")
                            {
                                temp.Value = (Selected_Object.Get_Object.Get_Type as Entity).Get_Damage;
                            }

                        }
                        else if (obiekt is TextBox temp && temp.Visible)
                        {
                            temp.Text = (Selected_Object.Get_Object.Get_Type as Entity).Get_Creature.Name;
                        }
                    }
                    return;
                }
                else if (Selected_Object.Get_Object.Get_Name == "Coin")
                {
                    radioButtonCoin.Checked = true;
                }
                else if (Selected_Object.Get_Object.Get_Name == "Weapon")
                {
                    radioButtonWeapon.Checked = true;
                }
                else
                {
                    return;
                }

                foreach (object obiekt in groupBox_Inside.Controls)
                {
                    if (obiekt is NumericUpDown)
                    {
                        NumericUpDown temp = obiekt as NumericUpDown;
                        if (temp.Visible)
                        {
                            temp.Value = (int)Selected_Object.Get_Object.Get_Type;
                        }
                    }
                }
            }
        }

        private void Show_Objects()
        {
            foreach (Map_Object MO in Objects)
            {
                if (numericUpDown_Map_X.Value == MO.Get_Object.Get_Map_X && numericUpDown_Map_Y.Value == MO.Get_Object.Get_Map_Y)
                {
                    pictureBox_display.Controls.Add(MO);
                }
                else
                {
                    pictureBox_display.Controls.Remove(MO);
                }
            }
            pictureBox_display.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Load_Object_Data();
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

            XmlNode Existing_Node = null;

            foreach (XmlElement Node_object in Object_list)
            {
                foreach(XmlNode Single_Node in Node_object)
                {
                    if(Single_Node.InnerText == "")
                    {
                        if(Selected_Object != null && Single_Node.Attributes["key"].Value == Selected_Object.Get_Key)
                        {
                            Existing_Node = Single_Node;
                        }

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
                value = numericUpDown_Map_X.Value + "," + numericUpDown_Map_Y.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
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
                string[] file_list = Directory.GetFiles(Environment.CurrentDirectory + "\\Map parts\\Level " + Current_Level + "\\");
                foreach(string file in file_list)
                {
                    if(file == Environment.CurrentDirectory + "\\Map parts\\Level " + Current_Level + "\\" + source + ".png")
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
                value = numericUpDown_Map_X.Value + "," + numericUpDown_Map_Y.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
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
                value = numericUpDown_Map_X.Value + "," + numericUpDown_Map_Y.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ","
                    + numericUpDown_range.Value + "," + numericUpDown_size.Value + ",Weapon," + weapon_id;
                weaponIDCounter++;
            }

            //dodaj klucz i wartość
            nodeRegion.SetAttribute("key", key);
            nodeRegion.SetAttribute("value", value);
            if(Existing_Node != null)
            {
                Existing_Node.Attributes["value"].Value = value;
                Selected_Object = null;
                add_button.Text = "Dodaj Obiekt";
            }
            else
            {
                xmlDoc.SelectSingleNode("//Map_1_Data/Map_Objects").AppendChild(nodeRegion);
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            string SectionName = "Map_" + Current_Level.ToString() + "_Data/Map_Objects";
            ConfigurationManager.RefreshSection(SectionName);

            File.Copy(Environment.CurrentDirectory + "\\WindowsFormsApp1.exe.config", Environment.CurrentDirectory.Replace("\\bin\\Debug", "") + "\\App.config", true);

            Objects.Clear();
            pictureBox_display.Controls.Clear();

            Retrive_Map_Objects(SectionName, Current_Level, Objects);
            foreach (Map_Object MO in Objects)
            {
                MO.Click += Map_Object_Click;
            }
            Show_Objects();
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

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            source.Close();
        }
    }
    
}