namespace WindowsFormsApplication1
{
    partial class Frmwelcome
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
            this.btntwoplayer = new System.Windows.Forms.Button();
            this.btnconnectlan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btntwoplayer
            // 
            this.btntwoplayer.AutoSize = true;
            this.btntwoplayer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btntwoplayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.btntwoplayer.Location = new System.Drawing.Point(29, 29);
            this.btntwoplayer.Name = "btntwoplayer";
            this.btntwoplayer.Size = new System.Drawing.Size(217, 54);
            this.btntwoplayer.TabIndex = 2;
            this.btntwoplayer.Text = "Two Player";
            this.btntwoplayer.UseVisualStyleBackColor = false;
            this.btntwoplayer.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnconnectlan
            // 
            this.btnconnectlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnconnectlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.btnconnectlan.Location = new System.Drawing.Point(29, 114);
            this.btnconnectlan.Name = "btnconnectlan";
            this.btnconnectlan.Size = new System.Drawing.Size(217, 57);
            this.btnconnectlan.TabIndex = 4;
            this.btnconnectlan.Text = "Connect Lan";
            this.btnconnectlan.UseVisualStyleBackColor = true;
            this.btnconnectlan.Click += new System.EventHandler(this.btnconnectlan_Click);
            // 
            // Frmwelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(267, 198);
            this.Controls.Add(this.btnconnectlan);
            this.Controls.Add(this.btntwoplayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "Frmwelcome";
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btntwoplayer;
        private System.Windows.Forms.Button btnconnectlan;
    }
}

