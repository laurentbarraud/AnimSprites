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
            this.picPlateforme = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).BeginInit();
            this.SuspendLayout();
            // 
            // picPlateforme
            // 
            this.picPlateforme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlateforme.Location = new System.Drawing.Point(221, 376);
            this.picPlateforme.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picPlateforme.Name = "picPlateforme";
            this.picPlateforme.Size = new System.Drawing.Size(387, 62);
            this.picPlateforme.TabIndex = 0;
            this.picPlateforme.TabStop = false;
            // 
            // frmAnimSprites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AnimSprites.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(861, 501);
            this.Controls.Add(this.picPlateforme);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmAnimSprites";
            this.Text = "Animation de sprites";
            this.Load += new System.EventHandler(this.frmAnimSprites_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPlateforme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPlateforme;
    }
}

