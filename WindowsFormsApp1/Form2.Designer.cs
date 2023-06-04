namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonEnemy = new System.Windows.Forms.RadioButton();
            this.radioButtonCoin = new System.Windows.Forms.RadioButton();
            this.radioButtonWeapon = new System.Windows.Forms.RadioButton();
            this.numericUpDown_Map_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Map_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.pictureBox_display = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox_type = new System.Windows.Forms.GroupBox();
            this.key_label_Txt = new System.Windows.Forms.TextBox();
            this.key_label = new System.Windows.Forms.Label();
            this.groupBox_Location = new System.Windows.Forms.GroupBox();
            this.groupBox_parameters = new System.Windows.Forms.GroupBox();
            this.groupBox_Inside = new System.Windows.Forms.GroupBox();
            this.numericUpDown_range = new System.Windows.Forms.NumericUpDown();
            this.label_size = new System.Windows.Forms.Label();
            this.numericUpDown_size = new System.Windows.Forms.NumericUpDown();
            this.label_range = new System.Windows.Forms.Label();
            this.add_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.radioButtonEOL = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_display)).BeginInit();
            this.groupBox_type.SuspendLayout();
            this.groupBox_Location.SuspendLayout();
            this.groupBox_parameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_range)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonEnemy
            // 
            this.radioButtonEnemy.AutoSize = true;
            this.radioButtonEnemy.Location = new System.Drawing.Point(6, 19);
            this.radioButtonEnemy.Name = "radioButtonEnemy";
            this.radioButtonEnemy.Size = new System.Drawing.Size(57, 17);
            this.radioButtonEnemy.TabIndex = 0;
            this.radioButtonEnemy.TabStop = true;
            this.radioButtonEnemy.Text = "Enemy";
            this.radioButtonEnemy.UseVisualStyleBackColor = true;
            this.radioButtonEnemy.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonCoin
            // 
            this.radioButtonCoin.AutoSize = true;
            this.radioButtonCoin.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCoin.Name = "radioButtonCoin";
            this.radioButtonCoin.Size = new System.Drawing.Size(46, 17);
            this.radioButtonCoin.TabIndex = 1;
            this.radioButtonCoin.TabStop = true;
            this.radioButtonCoin.Text = "Coin";
            this.radioButtonCoin.UseVisualStyleBackColor = true;
            this.radioButtonCoin.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonWeapon
            // 
            this.radioButtonWeapon.AutoSize = true;
            this.radioButtonWeapon.Location = new System.Drawing.Point(6, 65);
            this.radioButtonWeapon.Name = "radioButtonWeapon";
            this.radioButtonWeapon.Size = new System.Drawing.Size(66, 17);
            this.radioButtonWeapon.TabIndex = 2;
            this.radioButtonWeapon.TabStop = true;
            this.radioButtonWeapon.Text = "Weapon";
            this.radioButtonWeapon.UseVisualStyleBackColor = true;
            this.radioButtonWeapon.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // numericUpDown_Map_Y
            // 
            this.numericUpDown_Map_Y.Location = new System.Drawing.Point(128, 42);
            this.numericUpDown_Map_Y.Name = "numericUpDown_Map_Y";
            this.numericUpDown_Map_Y.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Map_Y.TabIndex = 3;
            this.numericUpDown_Map_Y.ValueChanged += new System.EventHandler(this.changeMap);
            // 
            // numericUpDown_Map_X
            // 
            this.numericUpDown_Map_X.Location = new System.Drawing.Point(128, 14);
            this.numericUpDown_Map_X.Name = "numericUpDown_Map_X";
            this.numericUpDown_Map_X.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Map_X.TabIndex = 4;
            this.numericUpDown_Map_X.ValueChanged += new System.EventHandler(this.changeMap);
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.Location = new System.Drawing.Point(129, 68);
            this.numericUpDown_Y.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Y.TabIndex = 5;
            // 
            // numericUpDown_X
            // 
            this.numericUpDown_X.Location = new System.Drawing.Point(129, 94);
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_X.TabIndex = 6;
            // 
            // pictureBox_display
            // 
            this.pictureBox_display.Location = new System.Drawing.Point(300, 26);
            this.pictureBox_display.Name = "pictureBox_display";
            this.pictureBox_display.Size = new System.Drawing.Size(100, 50);
            this.pictureBox_display.TabIndex = 7;
            this.pictureBox_display.TabStop = false;
            this.pictureBox_display.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Plansza x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Plansza y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Połorzenie na planszy x";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Połorzenie na planszy y";
            // 
            // groupBox_type
            // 
            this.groupBox_type.Controls.Add(this.radioButtonEOL);
            this.groupBox_type.Controls.Add(this.key_label_Txt);
            this.groupBox_type.Controls.Add(this.radioButtonCoin);
            this.groupBox_type.Controls.Add(this.key_label);
            this.groupBox_type.Controls.Add(this.radioButtonWeapon);
            this.groupBox_type.Controls.Add(this.radioButtonEnemy);
            this.groupBox_type.Location = new System.Drawing.Point(12, 144);
            this.groupBox_type.Name = "groupBox_type";
            this.groupBox_type.Size = new System.Drawing.Size(200, 156);
            this.groupBox_type.TabIndex = 14;
            this.groupBox_type.TabStop = false;
            this.groupBox_type.Text = "Typ obiektu";
            // 
            // key_label_Txt
            // 
            this.key_label_Txt.Location = new System.Drawing.Point(94, 124);
            this.key_label_Txt.Name = "key_label_Txt";
            this.key_label_Txt.Size = new System.Drawing.Size(100, 20);
            this.key_label_Txt.TabIndex = 19;
            // 
            // key_label
            // 
            this.key_label.AutoSize = true;
            this.key_label.Cursor = System.Windows.Forms.Cursors.No;
            this.key_label.Location = new System.Drawing.Point(6, 124);
            this.key_label.Name = "key_label";
            this.key_label.Size = new System.Drawing.Size(52, 13);
            this.key_label.TabIndex = 0;
            this.key_label.Text = "Object ID";
            // 
            // groupBox_Location
            // 
            this.groupBox_Location.Controls.Add(this.label1);
            this.groupBox_Location.Controls.Add(this.numericUpDown_Map_Y);
            this.groupBox_Location.Controls.Add(this.label5);
            this.groupBox_Location.Controls.Add(this.numericUpDown_Map_X);
            this.groupBox_Location.Controls.Add(this.label3);
            this.groupBox_Location.Controls.Add(this.numericUpDown_Y);
            this.groupBox_Location.Controls.Add(this.label2);
            this.groupBox_Location.Controls.Add(this.numericUpDown_X);
            this.groupBox_Location.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Location.Name = "groupBox_Location";
            this.groupBox_Location.Size = new System.Drawing.Size(254, 121);
            this.groupBox_Location.TabIndex = 15;
            this.groupBox_Location.TabStop = false;
            this.groupBox_Location.Text = "Położenie";
            // 
            // groupBox_parameters
            // 
            this.groupBox_parameters.Controls.Add(this.groupBox_Inside);
            this.groupBox_parameters.Controls.Add(this.numericUpDown_range);
            this.groupBox_parameters.Controls.Add(this.label_size);
            this.groupBox_parameters.Controls.Add(this.numericUpDown_size);
            this.groupBox_parameters.Controls.Add(this.label_range);
            this.groupBox_parameters.Location = new System.Drawing.Point(12, 306);
            this.groupBox_parameters.Name = "groupBox_parameters";
            this.groupBox_parameters.Size = new System.Drawing.Size(254, 166);
            this.groupBox_parameters.TabIndex = 16;
            this.groupBox_parameters.TabStop = false;
            this.groupBox_parameters.Text = "Właściwości obiektu";
            // 
            // groupBox_Inside
            // 
            this.groupBox_Inside.Location = new System.Drawing.Point(0, 68);
            this.groupBox_Inside.Name = "groupBox_Inside";
            this.groupBox_Inside.Size = new System.Drawing.Size(254, 99);
            this.groupBox_Inside.TabIndex = 18;
            this.groupBox_Inside.TabStop = false;
            // 
            // numericUpDown_range
            // 
            this.numericUpDown_range.Location = new System.Drawing.Point(128, 15);
            this.numericUpDown_range.Name = "numericUpDown_range";
            this.numericUpDown_range.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_range.TabIndex = 4;
            this.numericUpDown_range.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label_size
            // 
            this.label_size.AutoSize = true;
            this.label_size.Location = new System.Drawing.Point(5, 44);
            this.label_size.Name = "label_size";
            this.label_size.Size = new System.Drawing.Size(45, 13);
            this.label_size.TabIndex = 11;
            this.label_size.Text = "Rozmiar";
            // 
            // numericUpDown_size
            // 
            this.numericUpDown_size.Location = new System.Drawing.Point(128, 42);
            this.numericUpDown_size.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_size.Name = "numericUpDown_size";
            this.numericUpDown_size.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_size.TabIndex = 5;
            this.numericUpDown_size.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label_range
            // 
            this.label_range.AutoSize = true;
            this.label_range.Location = new System.Drawing.Point(5, 17);
            this.label_range.Name = "label_range";
            this.label_range.Size = new System.Drawing.Size(39, 13);
            this.label_range.TabIndex = 10;
            this.label_range.Text = "Zasięg";
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(1088, 12);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(86, 34);
            this.add_button.TabIndex = 17;
            this.add_button.Text = "Dodaj obiekt";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(1088, 52);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(86, 34);
            this.delete_button.TabIndex = 18;
            this.delete_button.Text = "Usuń obiekt";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // radioButtonEOL
            // 
            this.radioButtonEOL.AutoSize = true;
            this.radioButtonEOL.Location = new System.Drawing.Point(6, 88);
            this.radioButtonEOL.Name = "radioButtonEOL";
            this.radioButtonEOL.Size = new System.Drawing.Size(87, 17);
            this.radioButtonEOL.TabIndex = 20;
            this.radioButtonEOL.TabStop = true;
            this.radioButtonEOL.Text = "End Of Level";
            this.radioButtonEOL.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1186, 667);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.groupBox_parameters);
            this.Controls.Add(this.groupBox_Location);
            this.Controls.Add(this.groupBox_type);
            this.Controls.Add(this.pictureBox_display);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_display)).EndInit();
            this.groupBox_type.ResumeLayout(false);
            this.groupBox_type.PerformLayout();
            this.groupBox_Location.ResumeLayout(false);
            this.groupBox_Location.PerformLayout();
            this.groupBox_parameters.ResumeLayout(false);
            this.groupBox_parameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_range)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonEnemy;
        private System.Windows.Forms.RadioButton radioButtonCoin;
        private System.Windows.Forms.RadioButton radioButtonWeapon;
        private System.Windows.Forms.NumericUpDown numericUpDown_Map_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_Map_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private System.Windows.Forms.PictureBox pictureBox_display;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox_type;
        private System.Windows.Forms.GroupBox groupBox_Location;
        private System.Windows.Forms.GroupBox groupBox_parameters;
        private System.Windows.Forms.NumericUpDown numericUpDown_range;
        private System.Windows.Forms.Label label_size;
        private System.Windows.Forms.NumericUpDown numericUpDown_size;
        private System.Windows.Forms.Label label_range;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.GroupBox groupBox_Inside;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Label key_label;
        private System.Windows.Forms.TextBox key_label_Txt;
        private System.Windows.Forms.RadioButton radioButtonEOL;
    }
}