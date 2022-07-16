﻿namespace GalacticConquestSim
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picturebox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pixelsize_numeric = new System.Windows.Forms.NumericUpDown();
            this.speed_numeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelsize_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed_numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox
            // 
            this.picturebox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picturebox.Location = new System.Drawing.Point(14, 13);
            this.picturebox.Name = "picturebox";
            this.picturebox.Size = new System.Drawing.Size(836, 836);
            this.picturebox.TabIndex = 0;
            this.picturebox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pixelsize_numeric
            // 
            this.pixelsize_numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pixelsize_numeric.Location = new System.Drawing.Point(100, 855);
            this.pixelsize_numeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pixelsize_numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelsize_numeric.Name = "pixelsize_numeric";
            this.pixelsize_numeric.Size = new System.Drawing.Size(42, 29);
            this.pixelsize_numeric.TabIndex = 1;
            this.pixelsize_numeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelsize_numeric.ValueChanged += new System.EventHandler(this.pixelsize_numeric_ValueChanged);
            // 
            // speed_numeric
            // 
            this.speed_numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.speed_numeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speed_numeric.Location = new System.Drawing.Point(208, 855);
            this.speed_numeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.speed_numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speed_numeric.Name = "speed_numeric";
            this.speed_numeric.Size = new System.Drawing.Size(92, 29);
            this.speed_numeric.TabIndex = 2;
            this.speed_numeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speed_numeric.ValueChanged += new System.EventHandler(this.speed_numeric_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 857);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pixel Size";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 857);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Delay";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 896);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.speed_numeric);
            this.Controls.Add(this.pixelsize_numeric);
            this.Controls.Add(this.picturebox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelsize_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speed_numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picturebox;
        private System.Windows.Forms.Timer timer;
        private NumericUpDown pixelsize_numeric;
        private NumericUpDown speed_numeric;
        private Label label1;
        private Label label2;
    }
}