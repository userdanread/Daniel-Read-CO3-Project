using System.Windows.Forms;

namespace Daniel_Read_CO3_Project
{
    partial class formOptions
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox22 = new System.Windows.Forms.PictureBox();
            this.curBack = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.curVideo = new System.Windows.Forms.PictureBox();
            this.curControls = new System.Windows.Forms.PictureBox();
            this.formOptionsTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curControls)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox22
            // 
            this.pictureBox22.Image = global::Daniel_Read_CO3_Project.Properties.Resources.BACK_text;
            this.pictureBox22.Location = new System.Drawing.Point(120, 388);
            this.pictureBox22.Name = "pictureBox22";
            this.pictureBox22.Size = new System.Drawing.Size(135, 50);
            this.pictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox22.TabIndex = 30;
            this.pictureBox22.TabStop = false;
            // 
            // curBack
            // 
            this.curBack.Image = global::Daniel_Read_CO3_Project.Properties.Resources.CURSOR_icon;
            this.curBack.Location = new System.Drawing.Point(71, 388);
            this.curBack.Name = "curBack";
            this.curBack.Size = new System.Drawing.Size(43, 50);
            this.curBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.curBack.TabIndex = 31;
            this.curBack.TabStop = false;
            this.curBack.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Daniel_Read_CO3_Project.Properties.Resources.VIDEO_text;
            this.pictureBox1.Location = new System.Drawing.Point(120, 158);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Daniel_Read_CO3_Project.Properties.Resources.CONTROLS_text;
            this.pictureBox2.Location = new System.Drawing.Point(120, 99);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(254, 53);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // curVideo
            // 
            this.curVideo.Image = global::Daniel_Read_CO3_Project.Properties.Resources.CURSOR_icon;
            this.curVideo.Location = new System.Drawing.Point(71, 170);
            this.curVideo.Name = "curVideo";
            this.curVideo.Size = new System.Drawing.Size(43, 50);
            this.curVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.curVideo.TabIndex = 34;
            this.curVideo.TabStop = false;
            this.curVideo.Visible = false;
            // 
            // curControls
            // 
            this.curControls.Image = global::Daniel_Read_CO3_Project.Properties.Resources.CURSOR_icon;
            this.curControls.Location = new System.Drawing.Point(71, 102);
            this.curControls.Name = "curControls";
            this.curControls.Size = new System.Drawing.Size(43, 50);
            this.curControls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.curControls.TabIndex = 35;
            this.curControls.TabStop = false;
            // 
            // formOptionsTimer
            // 
            this.formOptionsTimer.Enabled = true;
            this.formOptionsTimer.Tick += new System.EventHandler(this.formOptionsTimer_Tick);
            // 
            // formOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.curControls);
            this.Controls.Add(this.curVideo);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.curBack);
            this.Controls.Add(this.pictureBox22);
            this.Name = "formOptions";
            this.Text = "OPTIONS";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formOptions_keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formOptions_keyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox pictureBox22;
        private PictureBox curBack;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox curVideo;
        private PictureBox curControls;
        private Timer formOptionsTimer;
    }
}