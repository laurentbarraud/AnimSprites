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
            this.picPlateforme = new System.Windows.Forms.PictureBox();
            this.picKnight = new System.Windows.Forms.PictureBox();
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).BeginInit();
            this.SuspendLayout();
            // 
            // picPlateforme
            // 
            this.picPlateforme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlateforme.Location = new System.Drawing.Point(221, 376);
            this.picPlateforme.Margin = new System.Windows.Forms.Padding(4);
            this.picPlateforme.Name = "picPlateforme";
            this.picPlateforme.Size = new System.Drawing.Size(387, 62);
            this.picPlateforme.TabIndex = 0;
            this.picPlateforme.TabStop = false;
            // 
            // picKnight
            // 
            this.picKnight.BackColor = System.Drawing.Color.Transparent;
            this.picKnight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKnight.Location = new System.Drawing.Point(250, 318);
            this.picKnight.Name = "picKnight";
            this.picKnight.Size = new System.Drawing.Size(52, 60);
            this.picKnight.TabIndex = 1;
            this.picKnight.TabStop = false;
            // 
            // animTimer
            // 
            this.animTimer.Tick += new System.EventHandler(this.animTimer_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AnimSprites.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(861, 501);
            this.Controls.Add(this.picKnight);
            this.Controls.Add(this.picPlateforme);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Animation de sprites";
            this.Load += new System.EventHandler(this.frmAnimSprites_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPlateforme;
        private System.Windows.Forms.PictureBox picKnight;
        private System.Windows.Forms.Timer animTimer;
    }
}

