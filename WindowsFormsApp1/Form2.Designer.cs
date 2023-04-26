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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonEnemy
            // 
            this.radioButtonEnemy.AutoSize = true;
            this.radioButtonEnemy.Location = new System.Drawing.Point(119, 264);
            this.radioButtonEnemy.Name = "radioButtonEnemy";
            this.radioButtonEnemy.Size = new System.Drawing.Size(57, 17);
            this.radioButtonEnemy.TabIndex = 0;
            this.radioButtonEnemy.TabStop = true;
            this.radioButtonEnemy.Text = "Enemy";
            this.radioButtonEnemy.UseVisualStyleBackColor = true;
            // 
            // radioButtonCoin
            // 
            this.radioButtonCoin.AutoSize = true;
            this.radioButtonCoin.Location = new System.Drawing.Point(119, 305);
            this.radioButtonCoin.Name = "radioButtonCoin";
            this.radioButtonCoin.Size = new System.Drawing.Size(46, 17);
            this.radioButtonCoin.TabIndex = 1;
            this.radioButtonCoin.TabStop = true;
            this.radioButtonCoin.Text = "Coin";
            this.radioButtonCoin.UseVisualStyleBackColor = true;
            // 
            // radioButtonWeapon
            // 
            this.radioButtonWeapon.AutoSize = true;
            this.radioButtonWeapon.Location = new System.Drawing.Point(119, 342);
            this.radioButtonWeapon.Name = "radioButtonWeapon";
            this.radioButtonWeapon.Size = new System.Drawing.Size(66, 17);
            this.radioButtonWeapon.TabIndex = 2;
            this.radioButtonWeapon.TabStop = true;
            this.radioButtonWeapon.Text = "Weapon";
            this.radioButtonWeapon.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_Map_Y
            // 
            this.numericUpDown_Map_Y.Location = new System.Drawing.Point(66, 57);
            this.numericUpDown_Map_Y.Name = "numericUpDown_Map_Y";
            this.numericUpDown_Map_Y.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Map_Y.TabIndex = 3;
            this.numericUpDown_Map_Y.ValueChanged += new System.EventHandler(this.changeMap);
            // 
            // numericUpDown_Map_X
            // 
            this.numericUpDown_Map_X.Location = new System.Drawing.Point(66, 84);
            this.numericUpDown_Map_X.Name = "numericUpDown_Map_X";
            this.numericUpDown_Map_X.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Map_X.TabIndex = 4;
            this.numericUpDown_Map_X.ValueChanged += new System.EventHandler(this.changeMap);
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.Location = new System.Drawing.Point(66, 111);
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
            this.numericUpDown_X.Location = new System.Drawing.Point(66, 150);
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_X.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(319, 194);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numericUpDown_X);
            this.Controls.Add(this.numericUpDown_Y);
            this.Controls.Add(this.numericUpDown_Map_X);
            this.Controls.Add(this.numericUpDown_Map_Y);
            this.Controls.Add(this.radioButtonWeapon);
            this.Controls.Add(this.radioButtonCoin);
            this.Controls.Add(this.radioButtonEnemy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Map_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonEnemy;
        private System.Windows.Forms.RadioButton radioButtonCoin;
        private System.Windows.Forms.RadioButton radioButtonWeapon;
        private System.Windows.Forms.NumericUpDown numericUpDown_Map_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_Map_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}