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
            this.components = new System.ComponentModel.Container();
            this.picKnight = new PlayerPictureBox();
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            this.picPlateforme = new System.Windows.Forms.PictureBox();
            this.picGround = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGround)).BeginInit();
            this.SuspendLayout();
            // 
            // picKnight
            // 
            this.picKnight.BackColor = System.Drawing.Color.Transparent;
            this.picKnight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKnight.Location = new System.Drawing.Point(260, 281);
            this.picKnight.Name = "picKnight";
            this.picKnight.Size = new System.Drawing.Size(52, 60);
            this.picKnight.TabIndex = 1;
            this.picKnight.TabStop = false;
            // 
            // animTimer
            // 
            this.animTimer.Enabled = true;
            this.animTimer.Interval = 50;
            this.animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // 
            // picPlateforme
            // 
            this.picPlateforme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPlateforme.Location = new System.Drawing.Point(260, 341);
            this.picPlateforme.Name = "picPlateforme";
            this.picPlateforme.Size = new System.Drawing.Size(320, 43);
            this.picPlateforme.TabIndex = 2;
            this.picPlateforme.TabStop = false;
            // 
            // picGround
            // 
            this.picGround.BackgroundImage = global::AnimSprites.Properties.Resources.texture_sol_forestier;
            this.picGround.Location = new System.Drawing.Point(0, 475);
            this.picGround.Name = "picGround";
            this.picGround.Size = new System.Drawing.Size(860, 25);
            this.picGround.TabIndex = 3;
            this.picGround.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AnimSprites.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(861, 501);
            this.Controls.Add(this.picGround);
            this.Controls.Add(this.picPlateforme);
            this.Controls.Add(this.picKnight);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Animation de sprites";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGround)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PlayerPictureBox picKnight;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.PictureBox picPlateforme;
        private System.Windows.Forms.PictureBox picGround;
    }
}

