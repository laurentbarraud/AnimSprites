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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            this.picPlatform = new System.Windows.Forms.PictureBox();
            this.picGround = new System.Windows.Forms.PictureBox();
            this.picKnight = new AnimSprites.PlayerPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).BeginInit();
            this.SuspendLayout();
            // 
            // animTimer
            // 
            this.animTimer.Enabled = true;
            this.animTimer.Interval = 50;
            this.animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // 
            // picPlatform
            // 
            this.picPlatform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlatform.Location = new System.Drawing.Point(260, 368);
            this.picPlatform.Name = "picPlatform";
            this.picPlatform.Size = new System.Drawing.Size(320, 43);
            this.picPlatform.TabIndex = 2;
            this.picPlatform.TabStop = false;
            // 
            // picGround
            // 
            this.picGround.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picGround.BackgroundImage")));
            this.picGround.Location = new System.Drawing.Point(0, 475);
            this.picGround.Name = "picGround";
            this.picGround.Size = new System.Drawing.Size(860, 25);
            this.picGround.TabIndex = 3;
            this.picGround.TabStop = false;
            // 
            // picKnight
            // 
            this.picKnight.attackLeft = ((System.Collections.Generic.List<System.Drawing.Bitmap>)(resources.GetObject("picKnight.attackLeft")));
            this.picKnight.attackRight = ((System.Collections.Generic.List<System.Drawing.Bitmap>)(resources.GetObject("picKnight.attackRight")));
            this.picKnight.BackColor = System.Drawing.Color.Transparent;
            this.picKnight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKnight.CurrentFrame = 0;
            this.picKnight.FacingLeft = true;
            this.picKnight.Gravity = 5;
            this.picKnight.InitialJumpSpeed = 10;
            this.picKnight.IsAttacking = false;
            this.picKnight.IsMovingLeft = false;
            this.picKnight.IsMovingRight = false;
            this.picKnight.jumpAttackLeft = ((System.Collections.Generic.List<System.Drawing.Bitmap>)(resources.GetObject("picKnight.jumpAttackLeft")));
            this.picKnight.jumpAttackRight = ((System.Collections.Generic.List<System.Drawing.Bitmap>)(resources.GetObject("picKnight.jumpAttackRight")));
            this.picKnight.JumpMultiplier = 2D;
            this.picKnight.JumpSpeed = 0;
            this.picKnight.Location = new System.Drawing.Point(260, 308);
            this.picKnight.Name = "picKnight";
            this.picKnight.Size = new System.Drawing.Size(52, 60);
            this.picKnight.Status = AnimSprites.PlayerPictureBox.PlayerStatus.IsFalling;
            this.picKnight.TabIndex = 1;
            this.picKnight.TabStop = false;
            this.picKnight.WalkingSpeed = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(860, 501);
            this.Controls.Add(this.picGround);
            this.Controls.Add(this.picPlatform);
            this.Controls.Add(this.picKnight);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprites animation";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picPlatform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKnight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PlayerPictureBox picKnight;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.PictureBox picPlatform;
        private System.Windows.Forms.PictureBox picGround;
    }
}

