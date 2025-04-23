/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.2</version>
/// <date>April 24th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AnimSprites.PlayerPictureBox;

namespace AnimSprites
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Set initial motionless image (first frame facing right)
            picKnight.BackgroundImage = picKnight.walkRight[0];

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
            picPlatform.BackgroundImage = platformBitmap;

            // Convert designer PictureBoxes to SolidPictureBox for collisions.
            ConvertToSolidPictureBox(ref picPlatform);
            ConvertToSolidPictureBox(ref picGround);
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

     
        // Game loop tick: update the game logic.
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
        }

        private void UpdateGame()
        {
            // -----------------------------
            // Horizontal Movement with Animation & Window Borders Collision
            // -----------------------------
            if (picKnight.IsMovingLeft || picKnight.IsMovingRight)
            {
                // Calculate the next horizontal position based on the walking speed
                int nextHorizontalPosition = picKnight.Left + (picKnight.IsMovingRight ? picKnight.WalkingSpeed : -picKnight.WalkingSpeed);

                // Define the area to check for horizontal collisions
                Rectangle horizontalCollisionArea = new Rectangle(nextHorizontalPosition, picKnight.Top, picKnight.Width, picKnight.Height);
                bool isHorizontalMovementAllowed = true;

                // Check for collisions with solid objects
                foreach (Control gameObject in this.Controls)
                {
                    if (gameObject is SolidPictureBox solidObject && solidObject.Bounds.IntersectsWith(horizontalCollisionArea))
                    {
                        isHorizontalMovementAllowed = false;
                        break;
                    }
                }

                // Handle window borders to keep the sprite within bounds
                if (nextHorizontalPosition < 0)
                {
                    nextHorizontalPosition = 0; // Stop at the left edge
                    isHorizontalMovementAllowed = false;
                }
                else if (nextHorizontalPosition > this.ClientSize.Width - picKnight.Width)
                {
                    nextHorizontalPosition = this.ClientSize.Width - picKnight.Width; // Stop at the right edge
                    isHorizontalMovementAllowed = false;
                }

                // Update sprite position if no obstacle is detected
                if (isHorizontalMovementAllowed)
                {
                    picKnight.Left = nextHorizontalPosition;
                }

                // Handle walking animation
                List<Bitmap> walkingFrames = picKnight.IsMovingLeft ? picKnight.walkLeft : picKnight.walkRight;
                picKnight.BackgroundImage = walkingFrames[picKnight.CurrentFrame];
                picKnight.CurrentFrame = (picKnight.CurrentFrame + 1) % walkingFrames.Count;
            }

            // -----------------------------
            // Jumping Logic
            // -----------------------------
            if (picKnight.Status == PlayerStatus.IsJumping)
            {
                // Amplify the jump speed using the multiplier for higher jump
                picKnight.Top -= (int)(picKnight.JumpSpeed * picKnight.JumpMultiplier);

                // Gradually decrease the jump speed
                picKnight.JumpSpeed--;

                // Transition to falling when jump speed reaches zero
                if (picKnight.JumpSpeed <= 0)
                {
                    picKnight.Status = PlayerStatus.IsFalling;
                }
            }
            else if (picKnight.Status == PlayerStatus.IsFalling)
            {
                // Apply gravity to move the sprite downward
                picKnight.Top += picKnight.Gravity;

                // Check if the sprite collides with a platform or the ground
                bool isGroundCollisionDetected = false;
                foreach (Control gameObject in this.Controls)
                {
                    if (gameObject is SolidPictureBox platformBelow)
                    {
                        // Define the area to check for collisions below the sprite
                        Rectangle verticalCollisionArea = new Rectangle(picKnight.Left, picKnight.Bottom, picKnight.Width, picKnight.Gravity);
                        if (platformBelow.Bounds.IntersectsWith(verticalCollisionArea))
                        {
                            isGroundCollisionDetected = true;
                            picKnight.Top = platformBelow.Top - picKnight.Height; // Align with the top of the platform
                            break;
                        }
                    }
                }

                // Handle landing or continue falling
                if (isGroundCollisionDetected)
                {
                    picKnight.Status = PlayerStatus.IsGrounded;
                    picKnight.JumpSpeed = 0; // Reset jump speed
                }
            }

            // -----------------------------
            // Collision Detection Above
            // -----------------------------
            bool isCeilingCollisionDetected = false;

            foreach (Control gameObject in this.Controls)
            {
                if (gameObject is SolidPictureBox ceilingPlatform)
                {
                    // Define the area to check for collisions above the sprite
                    Rectangle upwardCollisionArea = new Rectangle(picKnight.Left, picKnight.Top - picKnight.JumpSpeed, picKnight.Width, picKnight.JumpSpeed);

                    // Check for collisions with solid objects above
                    if (ceilingPlatform.Bounds.IntersectsWith(upwardCollisionArea))
                    {
                        isCeilingCollisionDetected = true;
                        break;
                    }
                }
            }

            // Interrupt the jump if a ceiling collision is detected
            if (isCeilingCollisionDetected && picKnight.Status == PlayerStatus.IsJumping)
            {
                picKnight.Status = PlayerStatus.IsFalling;
                picKnight.JumpSpeed = 0; // Reset jump speed
            }

            // -----------------------------
            // Vertical Movement (Gravity)
            // -----------------------------
            Rectangle fallingCollisionArea = new Rectangle(picKnight.Left, picKnight.Bottom + picKnight.Gravity, picKnight.Width, picKnight.Gravity);
            bool isFallingCollisionDetected = false;

            // Check for collisions with solid objects during falling
            foreach (Control gameObject in this.Controls)
            {
                if (gameObject is SolidPictureBox platformOnFall)
                {
                    if (platformOnFall.Bounds.IntersectsWith(fallingCollisionArea))
                    {
                        isFallingCollisionDetected = true;
                        picKnight.Top = platformOnFall.Top - picKnight.Height; // Align the sprite with the top of the platform
                        break;
                    }
                }
            }

            // Apply gravity or stop falling upon collision with the ground
            if (isFallingCollisionDetected)
            {
                picKnight.Status = PlayerStatus.IsGrounded;
                picKnight.JumpSpeed = 0; // Reset jump speed
            }
            else if (picKnight.Status != PlayerStatus.IsJumping)
            {
                picKnight.Status = PlayerStatus.IsFalling;
                picKnight.Top += picKnight.Gravity; // Move the sprite downward
            }

            // -----------------------------
            // Attack Animation Logic
            // -----------------------------
            if (picKnight.IsAttacking)
            {
                List<Bitmap> attackFrames;

                // Determine the correct animation frames
                if (picKnight.Status == PlayerStatus.IsJumping)
                {
                    attackFrames = picKnight.FacingLeft ? picKnight.jumpAttackLeft : picKnight.jumpAttackRight;
                }
                else
                {
                    attackFrames = picKnight.FacingLeft ? picKnight.attackLeft : picKnight.attackRight;
                }

                // Update the sprite's image with the current frame
                picKnight.BackgroundImage = attackFrames[picKnight.CurrentFrame];
                picKnight.CurrentFrame = (picKnight.CurrentFrame + 1) % attackFrames.Count;

                // Stop the attack animation when all frames are played
                if (picKnight.CurrentFrame == 0)
                {
                    picKnight.IsAttacking = false; // Reset attacking state
                }
            }
        }

        // KeyDown event: start the appropriate horizontal movement.
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                picKnight.IsMovingLeft = true;
                picKnight.FacingLeft = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                picKnight.IsMovingRight = true;
                picKnight.FacingLeft = false;
            }

            else if (e.KeyCode == Keys.Space)
            {
                // Triggers jump if the player is grounded
                if (picKnight.Status == PlayerStatus.IsGrounded)
                {
                    picKnight.Status = PlayerStatus.IsJumping;
                    picKnight.JumpSpeed = picKnight.InitialJumpSpeed; // Resets the jump force
                }
            }

            else if (e.KeyCode == Keys.ControlKey)
            {
                if (picKnight.Status != PlayerStatus.IsJumping && !picKnight.IsAttacking)
                {
                    // Starts ground attack animation
                    picKnight.IsAttacking = true;
                    picKnight.CurrentFrame = 0; // Resets animation to the first frame
                }
                else if (picKnight.Status == PlayerStatus.IsJumping && !picKnight.IsAttacking)
                {
                    // Starts jump-attack animation
                    picKnight.IsAttacking = true;
                    picKnight.CurrentFrame = 0; // Reset animation to the first frame
                }
            }


            animTimer.Start();
        }

        // KeyUp event: stops the horizontal movement when key is released.
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                picKnight.IsMovingLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                picKnight.IsMovingRight = false;
            }
        }
    }
}



