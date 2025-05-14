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
            picPlatform = new System.Windows.Forms.PictureBox();
            picGround = new System.Windows.Forms.PictureBox();
            picKnight = new PlayerPictureBox();
            ((System.ComponentModel.ISupportInitialize)picPlatform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGround).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picKnight).BeginInit();
            SuspendLayout();
            // 
            // animTimer
            // 
            animTimer.Enabled = true;
            animTimer.Interval = 50;
            animTimer.Tick += AnimTimer_Tick;
            // 
            // picPlatform
            // 
            picPlatform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            picPlatform.Location = new System.Drawing.Point(260, 460);
            picPlatform.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            picPlatform.Name = "picPlatform";
            picPlatform.Size = new System.Drawing.Size(320, 54);
            picPlatform.TabIndex = 2;
            picPlatform.TabStop = false;
            // 
            // picGround
            // 
            picGround.BackgroundImage = (System.Drawing.Image)resources.GetObject("picGround.BackgroundImage");
            picGround.Location = new System.Drawing.Point(0, 594);
            picGround.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            picGround.Name = "picGround";
            picGround.Size = new System.Drawing.Size(860, 31);
            picGround.TabIndex = 3;
            picGround.TabStop = false;
            // 
            // picKnight
            // 
            picKnight.attackLeft = (System.Collections.Generic.List<System.Drawing.Bitmap>)resources.GetObject("picKnight.attackLeft");
            picKnight.attackRight = (System.Collections.Generic.List<System.Drawing.Bitmap>)resources.GetObject("picKnight.attackRight");
            picKnight.BackColor = System.Drawing.Color.Transparent;
            picKnight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            picKnight.CurrentFrame = 0;
            picKnight.FacingLeft = true;
            picKnight.Gravity = 5;
            picKnight.InitialJumpSpeed = 10;
            picKnight.IsAttacking = false;
            picKnight.IsMovingLeft = false;
            picKnight.IsMovingRight = false;
            picKnight.jumpAttackLeft = (System.Collections.Generic.List<System.Drawing.Bitmap>)resources.GetObject("picKnight.jumpAttackLeft");
            picKnight.jumpAttackRight = (System.Collections.Generic.List<System.Drawing.Bitmap>)resources.GetObject("picKnight.jumpAttackRight");
            picKnight.JumpMultiplier = 2D;
            picKnight.JumpSpeed = 0;
            picKnight.Location = new System.Drawing.Point(260, 385);
            picKnight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            picKnight.Name = "picKnight";
            picKnight.Size = new System.Drawing.Size(52, 75);
            picKnight.Status = PlayerPictureBox.PlayerStatus.IsFalling;
            picKnight.TabIndex = 1;
            picKnight.TabStop = false;
            picKnight.WalkingSpeed = 5;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(860, 626);
            Controls.Add(picGround);
            Controls.Add(picPlatform);
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
            ((System.ComponentModel.ISupportInitialize)picPlatform).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGround).EndInit();
            ((System.ComponentModel.ISupportInitialize)picKnight).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private PlayerPictureBox picKnight;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.PictureBox picPlatform;
        private System.Windows.Forms.PictureBox picGround;
    }
}

