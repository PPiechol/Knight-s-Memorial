
namespace WindowsFormsApp1
{
    partial class UserControlHighScores
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelNumber.Location = new System.Drawing.Point(3, 4);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(62, 46);
            this.labelNumber.TabIndex = 0;
            this.labelNumber.Text = "Nr";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.Location = new System.Drawing.Point(135, 4);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(142, 46);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Nazwa";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelScore.Location = new System.Drawing.Point(623, 4);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(129, 46);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text = "Wynik";
            // 
            // UserControlHighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelNumber);
            this.Name = "UserControlHighScores";
            this.Size = new System.Drawing.Size(749, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelScore;
    }
}
