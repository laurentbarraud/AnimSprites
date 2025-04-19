/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.1</version>
/// <date>April 19th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnimSprites
{
    public partial class frmMain : Form
    {
        private int currentFrame = 0;
        private Bitmap spriteSheetLeft, spriteSheetRight;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private List<Bitmap> knightWalkLeft = new List<Bitmap>();
        private List<Bitmap> knightWalkRight = new List<Bitmap>();

        private bool isGrounded = false;  // True means the sprite is on a solid object
        private int gravity = 5;          // Gravity strength (pixels per tick)
        private int moveSpeed = 5;        // Horizontal movement speed (pixels per tick)
        private bool movingLeft = false;  // True when left arrow is held down
        private bool movingRight = false; // True when right arrow is held down

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                Bitmap bmpLeft = (Bitmap)Properties.Resources.ResourceManager.GetObject($"walk{(i < 10 ? "0" : "")}{i}_left");
                knightWalkLeft.Add(bmpLeft);

                Bitmap bmpRight = (Bitmap)Properties.Resources.ResourceManager.GetObject($"walk{(i < 10 ? "0" : "")}{i}_right");
                knightWalkRight.Add(bmpRight);
            }

            // Set initial motionless image (first frame facing right)
            picKnight.BackgroundImage = knightWalkRight[0];

            // Load the tileset image
            Bitmap bmpTileSet = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Define the actual dimensions of each tile and the spacing between tiles
            int tileWidth = 30;  // Standard width of the tiles (middle tiles)
            int tileHeight = 31; // Height of all tiles
            int tileSpacing = 2; // Space between tiles in the tileset

            // Define source rectangles with adjusted cropping
            Rectangle srcRectLeftPlatform = new Rectangle(4, 2, tileWidth - 2, tileHeight);  // Left tile: 2 pixels less on the left
            Rectangle srcRectMiddlePlatform = new Rectangle(34 + tileSpacing, 2, tileWidth, tileHeight); // Middle tile remains the same
            Rectangle srcRectRightPlatform = new Rectangle(66 + 2 * tileSpacing, 2, tileWidth - 4, tileHeight); // Right tile: 2 pixels less on the right

            // Create a new bitmap to represent the full platform (8 tiles wide)
            int totalWidth = (tileWidth - 2) + (6 * tileWidth) + (tileWidth - 4); // Calculate total width precisely
            Bitmap platformBitmap = new Bitmap(totalWidth, tileHeight);

            // Create a Graphics object to draw on the new bitmap
            using (Graphics g = Graphics.FromImage(platformBitmap))
            {
                GraphicsUnit units = GraphicsUnit.Pixel;

                // Draw the left end of the platform
                g.DrawImage(bmpTileSet, new Rectangle(0, 0, tileWidth - 2, tileHeight), srcRectLeftPlatform, units);

                // Draw the 6 middle sections of the platform
                for (int i = 0; i < 6; i++)
                {
                    int xPosition = (tileWidth - 2) + (i * tileWidth);
                    g.DrawImage(bmpTileSet, new Rectangle(xPosition, 0, tileWidth, tileHeight), srcRectMiddlePlatform, units);
                }

                // Draw the right end of the platform
                int rightXPosition = (tileWidth - 2) + (6 * tileWidth);
                g.DrawImage(bmpTileSet, new Rectangle(rightXPosition, 0, tileWidth - 4, tileHeight), srcRectRightPlatform, units);
            }

            // Set the background image of the PictureBox
            picPlateforme.BackgroundImage = platformBitmap;

            // Make the background image stretch to fill the PictureBox
            picPlateforme.BackgroundImageLayout = ImageLayout.Stretch;
       
            // Convert designer PictureBoxes to SolidPictureBox for collisions.
            ConvertToSolidPictureBox(ref picPlateforme);
            ConvertToSolidPictureBox(ref picGround);
            ConvertToPlayerPictureBox(ref picKnight);
            PlayerPictureBox playerKnight = (PlayerPictureBox)picKnight;
        }

        // Utility method: convert a PictureBox to a SolidPictureBox.
        private void ConvertToSolidPictureBox(ref PictureBox pb)
        {
            SolidPictureBox spb = new SolidPictureBox();
            spb.Location = pb.Location;
            spb.Size = pb.Size;
            spb.BackgroundImage = pb.BackgroundImage;
            spb.BackgroundImageLayout = pb.BackgroundImageLayout;
            spb.BackColor = pb.BackColor;
            spb.Name = pb.Name;
            this.Controls.Remove(pb);
            pb.Dispose();
            pb = spb;
            this.Controls.Add(pb);
            pb.SendToBack(); // Ensure solid objects remain in the background.
        }

        private void ConvertToPlayerPictureBox(ref PictureBox picKnight)
        {
            // Create a new instance of PlayerPictureBox
            PlayerPictureBox player = new PlayerPictureBox();

            // Copy properties from old PictureBox
            player.Location = picKnight.Location;
            player.Size = picKnight.Size;
            player.BackgroundImage = picKnight.BackgroundImage;
            player.BackgroundImageLayout = picKnight.BackgroundImageLayout;
            player.BackColor = picKnight.BackColor;
            player.Name = picKnight.Name;

            // Remove old PictureBox and replace it with PlayerPictureBox
            this.Controls.Remove(picKnight);
            picKnight.Dispose();
            picKnight = player;
            this.Controls.Add(picKnight);
        }


        // Game loop tick: update the game logic.
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
        }

        // UpdateGame method:
        // - Applies gravity for vertical movement (the knight falls if not grounded).
        // - Checks collisions with all SolidPictureBox objects.
        // - Updates horizontal movement only if no collision is detected (animation may still play).
        private void UpdateGame()
        {
            // ---------------------------
            // Vertical Movement (Gravity)
            // ---------------------------
            Rectangle nextFallRect = new Rectangle(picKnight.Left, picKnight.Bottom + gravity, picKnight.Width, gravity);
            bool collisionBelow = false;

            // Check collision with all solid objects (platform and ground).
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SolidPictureBox spb)
                {
                    if (spb.Bounds.IntersectsWith(nextFallRect))
                    {
                        collisionBelow = true;
                        // Snap the sprite to the top of the object.
                        picKnight.Top = spb.Top - picKnight.Height;
                        break;
                    }
                }
            }

            // Apply gravity if no solid object is below
            if (!collisionBelow)
            {
                if (!((PlayerPictureBox)picKnight).isGrounded)
                {
                    ((PlayerPictureBox)picKnight).Top += gravity;
                }

                ((PlayerPictureBox)picKnight).isGrounded = false;
            }
            else
            {
                ((PlayerPictureBox)picKnight).isGrounded = true;
            }

            // ---------------------------
            // Horizontal Movement with Animation & Window Borders Collision
            // ---------------------------
            if (movingLeft || movingRight)
            {
                int nextPos = picKnight.Left + (movingRight ? moveSpeed : -moveSpeed);
                Rectangle nextRect = new Rectangle(nextPos, picKnight.Top, picKnight.Width, picKnight.Height);
                bool canMove = true;

                // Collision detection with solid objects
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is SolidPictureBox spb && spb.Bounds.IntersectsWith(nextRect))
                    {
                        canMove = false;
                        break;
                    }
                }

                // Limit sprite position at screen borders without blocking movement
                if (nextPos < 0)
                {
                    nextPos = 0; // Stop at left edge but allow movement
                    canMove = false;
                }

                if (nextPos > this.ClientSize.Width - picKnight.Width)
                {
                    nextPos = this.ClientSize.Width - picKnight.Width; // Stop at right edge but allow movement
                    canMove = false;
                }

                // Animation setup
                List<Bitmap> animationFrames = movingLeft ? knightWalkLeft : knightWalkRight;

                // Move sprite if allowed
                if (canMove)
                {
                    picKnight.Left = nextPos;
                }

                // Keep animation playing even if blocked
                picKnight.BackgroundImage = animationFrames[currentFrame];
                currentFrame = (currentFrame + 1) % animationFrames.Count;
            }
        }



        // KeyDown event: start the appropriate horizontal movement.
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                movingLeft = true;
            else if (e.KeyCode == Keys.Right)
                movingRight = true;
            
            animTimer.Start();
        }

        // KeyUp event: stop the horizontal movement when key is released.
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                movingLeft = false;
            else if (e.KeyCode == Keys.Right)
                movingRight = false;
        }
    }
}



