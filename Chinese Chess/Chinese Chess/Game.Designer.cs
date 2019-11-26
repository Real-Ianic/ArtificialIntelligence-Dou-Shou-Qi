namespace Chinese_Chess
{
    partial class Game
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
            this.currentAnimal = new System.Windows.Forms.Label();
            this.labelTurn = new System.Windows.Forms.Label();
            this.currStrengthLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // currentAnimal
            // 
            this.currentAnimal.AutoSize = true;
            this.currentAnimal.Location = new System.Drawing.Point(566, 87);
            this.currentAnimal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentAnimal.Name = "currentAnimal";
            this.currentAnimal.Size = new System.Drawing.Size(110, 13);
            this.currentAnimal.TabIndex = 0;
            this.currentAnimal.Text = "Current Animal : None";
            // 
            // labelTurn
            // 
            this.labelTurn.AutoSize = true;
            this.labelTurn.Location = new System.Drawing.Point(566, 59);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.Size = new System.Drawing.Size(81, 13);
            this.labelTurn.TabIndex = 1;
            this.labelTurn.Text = "Current Turn : 1";
            // 
            // currStrengthLabel
            // 
            this.currStrengthLabel.AutoSize = true;
            this.currStrengthLabel.Location = new System.Drawing.Point(566, 112);
            this.currStrengthLabel.Name = "currStrengthLabel";
            this.currStrengthLabel.Size = new System.Drawing.Size(119, 13);
            this.currStrengthLabel.TabIndex = 2;
            this.currStrengthLabel.Text = "Current Strength : None";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.currStrengthLabel);
            this.Controls.Add(this.labelTurn);
            this.Controls.Add(this.currentAnimal);
            this.Name = "Game";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label currentAnimal;
        private System.Windows.Forms.Label labelTurn;
        private System.Windows.Forms.Label currStrengthLabel;
    }
}