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
        int enemyIDCounter = 3;
        int coinIDCounter = 1;
        int weaponIDCounter = 1;
        public Form2()
        {
            InitializeComponent();
        }

        private void changeMap(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level 1\\Map " + numericUpDown_Map_Y.Value + "-" + numericUpDown_Map_X.Value + ".png");
                pictureBox1.Image = Display.Scale(pictureBox1.Image, pictureBox1.Size.Width, pictureBox1.Size.Height);
            }
            catch
            {
                MessageBox.Show("Map not found");


            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(this.Height, this.Height);
            pictureBox1.Location = new Point((this.Width - pictureBox1.Size.Width) / 2, 0);
            pictureBox1.Image = Image.FromFile(Environment.CurrentDirectory + "\\Map parts\\Level 1\\Map " + numericUpDown_Map_Y.Value + "-" + numericUpDown_Map_X.Value + ".png");
            pictureBox1.Image = Display.Scale(pictureBox1.Image, pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            numericUpDown_Y.Value = Cursor.Position.Y;
            numericUpDown_X.Value = Cursor.Position.X - (this.Width - pictureBox1.Size.Width) / 2;

            //dodawanie nowych kluczy do App.config
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            var nodeRegion = xmlDoc.CreateElement("add");
            string enemyID = "Enemy" + enemyIDCounter.ToString();
            string coinID = "Coin" + coinIDCounter.ToString();
            string weaponID = "Sword" + weaponIDCounter.ToString();
            string value = "";
            string key = "";
            //sprawdzanie poszczególnych opcji
            if (radioButtonCoin.Checked)
            {
                key = coinID;
                value = "1," + numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ",15,50,Coin,15";
                coinIDCounter++;
            }
            else if (radioButtonEnemy.Checked)
            {
                key = enemyID;
                value = "1," + numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ",50,50,Enemy,6,20";
                enemyIDCounter++;
            }
            else
            {
                key = weaponID;
                value = "1," + numericUpDown_Map_Y.Value + "," + numericUpDown_Map_X.Value + "," + numericUpDown_X.Value + "," + numericUpDown_Y.Value + ",40,50,Weapon,4";
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
    }
    
}