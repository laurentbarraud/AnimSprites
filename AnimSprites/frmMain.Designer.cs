namespace AnimSprites
{
    partial class frmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            animTimer = new System.Windows.Forms.Timer(components);
            picKnight = new PlayerPictureBox();
            picGround = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)picKnight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGround).BeginInit();
            SuspendLayout();
            // 
            // animTimer
            // 
            animTimer.Enabled = true;
            animTimer.Interval = 50;
            animTimer.Tick += AnimTimer_Tick;
            // 
            // picKnight
            // 
            picKnight.BackColor = System.Drawing.Color.Transparent;
            picKnight.BackgroundImage = Properties.Resources.walk01_right;
            picKnight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            picKnight.Location = new System.Drawing.Point(260, 518);
            picKnight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            picKnight.Name = "picKnight";
            picKnight.Size = new System.Drawing.Size(52, 75);
            picKnight.TabIndex = 1;
            picKnight.TabStop = false;
            // 
            // picGround
            // 
            picGround.BackgroundImage = Properties.Resources.forest_ground;
            picGround.Location = new System.Drawing.Point(0, 594);
            picGround.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            picGround.Name = "picGround";
            picGround.Size = new System.Drawing.Size(860, 31);
            picGround.TabIndex = 3;
            picGround.TabStop = false;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(860, 626);
            Controls.Add(picGround);
            Controls.Add(picKnight);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sprites animation";
            Load += frmMain_Load;
            KeyDown += frmMain_KeyDown;
            KeyUp += frmMain_KeyUp;
            ((System.ComponentModel.ISupportInitialize)picKnight).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGround).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private PlayerPictureBox picKnight;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.PictureBox picGround;
    }
}

