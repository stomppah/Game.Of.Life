namespace GOL
{
    partial class WorldDisplayForm
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
            this.worldCanvas = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.worldCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // worldCanvas
            // 
            this.worldCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldCanvas.Location = new System.Drawing.Point(0, 0);
            this.worldCanvas.Name = "worldCanvas";
            this.worldCanvas.Size = new System.Drawing.Size(960, 530);
            this.worldCanvas.TabIndex = 0;
            this.worldCanvas.TabStop = false;
            this.worldCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.worldCanvas_Paint);
            this.worldCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.worldCanvas_MouseDown);
            this.worldCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.worldCanvas_MouseMove);
            this.worldCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.worldCanvas_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WorldDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 530);
            this.Controls.Add(this.worldCanvas);
            this.MaximizeBox = false;
            this.Name = "WorldDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorldDisplayForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.worldCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox worldCanvas;
        private System.Windows.Forms.Timer timer1;
    }
}

