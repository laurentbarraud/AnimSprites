/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.3.1</version>
/// <date>May 14th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AnimSprites.PlayerPictureBox;
using static System.Net.Mime.MediaTypeNames;

namespace AnimSprites
{
    public partial class frmMain : Form
    {
        // Global viewport offset which indicates how far the "camera" has scrolled
        private int viewportHorizontalOffset = 0;

        // Define the total width of the level
        private int levelWidth = 1000;

        private Panel levelEditorPanel;
        private bool isMenuVisible = false;
        private SolidPictureBox selectedPlatform = null;
        private TrackBar trkBlockCount; // Stores the slider instance globally
        private Label lblBlockCount; // Displays the current block count selected

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // -----------------------------------------
            // Create the Menu Panel (initially hidden)
            // -----------------------------------------
            levelEditorPanel = new Panel
            {
                Left = 10,
                Top = 10,
                Width = 250,
                Height = 160, // Optimized size to fit everything
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            // -----------------------------
            // "Add Platform" Button (Top)
            // -----------------------------
            Button addPlatformButton = new Button
            {
                Text = "Add a platform",
                Width = 220,
                Height = 30,
                Top = 10,
                Left = 10
            };
            addPlatformButton.Click += AddPlatform;
            levelEditorPanel.Controls.Add(addPlatformButton);

            // -----------------------------
            // Label "Number of blocs" (Dynamic)
            // -----------------------------
            lblBlockCount = new Label
            {
                Text = $"Number of blocs to add : {trkBlockCount?.Value ?? 6}", // Shows initial value if TrackBar is not yet set
                Left = 10,
                Top = 50, // Positioned below the button
                Width = 220,
                TextAlign = ContentAlignment.MiddleCenter
            };
            levelEditorPanel.Controls.Add(lblBlockCount);

            // -----------------------------
            // TrackBar for block count
            // -----------------------------
            trkBlockCount = new TrackBar
            {
                Minimum = 1,
                Maximum = 18,
                Value = 6,
                Left = 10,
                Top = 80, // Positioned below the label
                Width = 220,
                TickFrequency = 1,
                SmallChange = 1,
                LargeChange = 2
            };
            levelEditorPanel.Controls.Add(trkBlockCount);

            // Updates the label dynamically based on slider movement
            trkBlockCount.Scroll += (aSender, aEvent) =>
            {
                lblBlockCount.Text = $"Number of blocs to add : {trkBlockCount.Value}";
            };

            // -----------------------------
            // "Delete Object" Button
            // -----------------------------
            Button deletePlatformButton = new Button
            {
                Text = "Delete selected object",
                Width = 220,
                Height = 30,
                Top = 125, // Positioned below the slider
                Left = 10
            };
            deletePlatformButton.Click += DeleteSelectedObject;
            levelEditorPanel.Controls.Add(deletePlatformButton);

            // -----------------------------
            // Add the Menu Panel to the Form
            // -----------------------------
            this.Controls.Add(levelEditorPanel);

            // -----------------------------
            // Loads the initial platform
            // -----------------------------

            // Load the tileset image
            Bitmap bmpTileSet = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Define source rectangles for left, middle, and right tiles
            Rectangle srcRectLeftPlatform = new Rectangle(4, 2, 28, 31);  // Left tile
            Rectangle srcRectMiddlePlatform = new Rectangle(36, 2, 30, 31); // Middle tile
            Rectangle srcRectRightPlatform = new Rectangle(68, 2, 26, 31); // Right tile

            // Define number of middle blocks
            int middleBlockCount = 6; // Adjust platform size dynamically

            // Generate platform texture using the method
            Bitmap platformBitmap = FillPlatformWithTextures(middleBlockCount, srcRectLeftPlatform, srcRectMiddlePlatform, srcRectRightPlatform, bmpTileSet);

            // Assign the generated image to picPlateforme
            picPlatform.BackgroundImage = platformBitmap;
            picPlatform.Width = platformBitmap.Width;
            picPlatform.Height = platformBitmap.Height;
            picPlatform.BackColor = Color.Transparent; // Ensure transparent background

            // ----------------------------------------------------------------
            // Converts designer PictureBoxes to SolidPictureBox for collisions
            // ----------------------------------------------------------------

            ConvertToSolidPictureBox(ref picPlatform);
            ConvertToSolidPictureBox(ref picGround);

            // ----------------------------------
            // Makes initial platform selectable
            // ----------------------------------

            picPlatform.Click += SelectPlatform;

            // ---------------------------------------------------
            // Makes initial platform movable if the menu is open
            // ---------------------------------------------------

            picPlatform.MouseDown += (aSender, aEvent) =>
            {
                if (levelEditorPanel.Visible)
                {
                    picPlatform.Tag = aEvent.Location;
                }
            };

            picPlatform.MouseMove += (aSender, aEvent) =>
            {
                if (levelEditorPanel.Visible && aEvent.Button == MouseButtons.Left && picPlatform.Tag is Point initialPos)
                {
                    picPlatform.Left += aEvent.X - initialPos.X;
                    picPlatform.Top += aEvent.Y - initialPos.Y;
                }
            };

            // -----------------------------
            // Set initial motionless images
            // -----------------------------
            picKnight.BackgroundImage = picKnight.walkRight[0];
            picGround.Width = levelWidth;
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

        private void AddPlatform(object sender, EventArgs e)
        {
            // Load the tileset image
            Bitmap bmpTileSet = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Define source rectangles for left, middle, and right tiles
            Rectangle srcRectLeft = new Rectangle(4, 2, 28, 31);
            Rectangle srcRectMiddle = new Rectangle(36, 2, 30, 31);
            Rectangle srcRectRight = new Rectangle(68, 2, 26, 31);

            // Retrieve the selected number of middle blocks from the slider
            int middleBlockCount = trkBlockCount.Value; // Dynamically adjust platform size

            // Generate the platform texture using FillPlatformWithTextures
            Bitmap platformBitmap = FillPlatformWithTextures(middleBlockCount, srcRectLeft, srcRectMiddle, srcRectRight, bmpTileSet);

            // Create the new platform with the generated texture
            SolidPictureBox newPlatform = new SolidPictureBox
            {
                Left = viewportHorizontalOffset + 200, // Position X relative to camera
                Top = 350, // Position Y
                Width = platformBitmap.Width,
                Height = platformBitmap.Height,
                BackgroundImage = platformBitmap,
                BackColor = Color.Transparent // Ensure transparency
            };

            // Allow the platform to be moved with the mouse
            newPlatform.MouseDown += (aSender, aEvent) => { newPlatform.Tag = aEvent.Location; };
            newPlatform.MouseMove += (aSender, aEvent) =>
            {
                if (aEvent.Button == MouseButtons.Left && newPlatform.Tag is Point initialPos)
                {
                    newPlatform.Left += aEvent.X - initialPos.X;
                    newPlatform.Top += aEvent.Y - initialPos.Y;
                }
            };

            newPlatform.Click += SelectPlatform; // Ensures that each new platform created can be
                                                 // selected when the user clicks on it.

            // Add the new platform to the form
            this.Controls.Add(newPlatform);
        }


        // Game loop tick: update the game logic.
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            UpdateSpriteWalkingAnimation(); // Handles only sprite animation
            UpdateGame(); // Handles movement and scrolling
        }

        private async void BlinkSelectedPlatform()
        {
            if (selectedPlatform != null && levelEditorPanel.Visible)
            {
                Bitmap originalImage = (Bitmap)selectedPlatform.BackgroundImage;
                Bitmap invertedImage = InvertBitmapColors(originalImage);

                // Swap to inverted image
                selectedPlatform.BackgroundImage = invertedImage;

                // Wait for half second
                await Task.Delay(500);

                // Restore original image
                selectedPlatform.BackgroundImage = originalImage;
            }
        }

        private void DeleteSelectedObject(object sender, EventArgs e)
        {
            if (selectedPlatform != null)
            {
                this.Controls.Remove(selectedPlatform); // Remove platform from the form
                selectedPlatform = null; // Reset selection after deletion
            }
        }


        /// <summary>
        /// Creates a composite platform image using left, middle, and right textures.
        /// </summary>
        /// <param name="middleBlockCount">Number of middle blocks to repeat.</param>
        /// <param name="srcRectLeft">Source rectangle for the left end.</param>
        /// <param name="srcRectMiddle">Source rectangle for the middle block.</param>
        /// <param name="srcRectRight">Source rectangle for the right end.</param>
        /// <param name="tileSet">The tileset bitmap to use for textures.</param>
        /// <returns>A Bitmap representing the full platform image.</returns>
        private static Bitmap FillPlatformWithTextures(int middleBlockCount, Rectangle srcRectLeft, Rectangle srcRectMiddle, Rectangle srcRectRight, Bitmap tileSet)
        {
            int tileHeight = srcRectMiddle.Height;
            int totalWidth = middleBlockCount * srcRectMiddle.Width; // Default: only middle blocks

            // Check if platform includes left and right blocks
            bool includeSides = middleBlockCount >= 3;

            if (includeSides)
            {
                totalWidth += srcRectLeft.Width + srcRectRight.Width; // Add left and right widths
            }

            // Create the platform image
            Bitmap platformBitmap = new Bitmap(totalWidth, tileHeight);

            using (Graphics g = Graphics.FromImage(platformBitmap))
            {
                GraphicsUnit units = GraphicsUnit.Pixel;
                int xPosition = 0;

                // Draw left block if included
                if (includeSides)
                {
                    g.DrawImage(tileSet, new Rectangle(xPosition, 0, srcRectLeft.Width, tileHeight), srcRectLeft, units);
                    xPosition += srcRectLeft.Width;
                }

                // Draw middle blocks
                for (int i = 0; i < middleBlockCount; i++)
                {
                    g.DrawImage(tileSet, new Rectangle(xPosition, 0, srcRectMiddle.Width, tileHeight), srcRectMiddle, units);
                    xPosition += srcRectMiddle.Width;
                }

                // Draw right block if included
                if (includeSides)
                {
                    g.DrawImage(tileSet, new Rectangle(xPosition, 0, srcRectRight.Width, tileHeight), srcRectRight, units);
                }
            }

            return platformBitmap;
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

            else if (e.KeyCode == Keys.A && viewportHorizontalOffset > 0)
            {
                viewportHorizontalOffset -= 20; // Déplace la caméra à gauche
                ScrollLevel(20);
            }

            else if (e.KeyCode == Keys.D && viewportHorizontalOffset + this.ClientSize.Width < levelWidth)
            {
                viewportHorizontalOffset += 20; // Déplace la caméra à droite
                ScrollLevel(-20);
            }

            else if (e.KeyCode == Keys.B)
            {
                levelEditorPanel.Visible = !levelEditorPanel.Visible;
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

        /// <summary>
        /// Creates a clone of the image and applies an inverted color filter. 
        /// Each channel (Red, Green, Blue) is reversed, giving a negative effect.
        /// The final image is returned to be used as a temporary image.
        /// </summary>
        /// <param name="original">The original bitmap</param>
        /// <returns>The inverted bitmap</returns>
        private static Bitmap InvertBitmapColors(Bitmap original)
        {
            Bitmap invertedBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(invertedBitmap))
            {
                // Use color matrix to invert colors
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
            new float[] {-1,  0,  0, 0, 0},
            new float[] { 0, -1,  0, 0, 0},
            new float[] { 0,  0, -1, 0, 0},
            new float[] { 0,  0,  0, 1, 0},
            new float[] { 1,  1,  1, 0, 1}
                });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                            0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }

            return invertedBitmap;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                // Retrieve background image dimensions
                int backgroundImageWidth = this.BackgroundImage.Width;

                // Calculate the scrolling offset for the background image
                int backgroundScrollOffset = (int)(viewportHorizontalOffset * 0.5) % backgroundImageWidth;

                // Draw the background image multiple times to ensure continuous scrolling
                for (int positionX = -backgroundScrollOffset; positionX < this.ClientSize.Width; positionX += backgroundImageWidth)
                {
                    e.Graphics.DrawImage(this.BackgroundImage, positionX, 0, backgroundImageWidth, this.ClientSize.Height);
                }
            }

            base.OnPaint(e);
        }

        private void SelectPlatform(object sender, EventArgs e)
        {
            if (sender is SolidPictureBox platform)
            {
                selectedPlatform = platform; // Store the selected platform
                BlinkSelectedPlatform(); // Start blinking effect
            }
        }


        /// <summary>
        /// Applies a horizontal scrolling offset to all game elements.
        /// </summary>
        /// <param name="scrollAmount">The horizontal amount to move the level.</param>
        private void ScrollLevel(int scrollAmount)
        {
            foreach (Control gameObject in this.Controls)
            {
                if (gameObject is SolidPictureBox)
                {
                    gameObject.Left += scrollAmount;
                }
            }

            // Refresh only if necessary to reduce rendering lag
            if (scrollAmount != 0)
            {
                this.Invalidate();
            }
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

                // -----------------------------
                // Scrolling Logic
                // -----------------------------

                // Define scrolling boundaries based on the client width:
                int rightBoundary = (int)(this.ClientSize.Width * 0.8); // 4/5 of the width
                int leftBoundary = (int)(this.ClientSize.Width * 0.2);  // 1/5 of the width

                // Scroll right when the sprite reaches 4/5 of the screen, but stop at levelWidth
                if (picKnight.Left > rightBoundary && viewportHorizontalOffset + this.ClientSize.Width < levelWidth)
                {
                    int scrollAmount = picKnight.Left - rightBoundary;
                    viewportHorizontalOffset += scrollAmount;
                    ScrollLevel(-scrollAmount);
                    picKnight.Left = rightBoundary; // Keep the character at the right boundary
                }

                // Prevent scrolling beyond the rightmost boundary (keep the "wall" at levelWidth)
                if (viewportHorizontalOffset + this.ClientSize.Width > levelWidth)
                {
                    viewportHorizontalOffset = levelWidth - this.ClientSize.Width;
                }

                // Scroll left when the sprite moves past 1/5 of the screen, but stop at position 0
                if (picKnight.Left < leftBoundary && viewportHorizontalOffset > 0)
                {
                    int scrollAmount = leftBoundary - picKnight.Left;
                    viewportHorizontalOffset -= scrollAmount;
                    ScrollLevel(scrollAmount);
                    picKnight.Left = leftBoundary; // Keep the character at the left boundary
                }

                // Prevent scrolling beyond the leftmost boundary (keep the "wall" at x = 0)
                if (viewportHorizontalOffset < 0)
                {
                    viewportHorizontalOffset = 0;
                }

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

        private void UpdateSpriteWalkingAnimation()
        {
            if (picKnight.IsMovingLeft || picKnight.IsMovingRight)
            {
                List<Bitmap> walkingFrames = picKnight.IsMovingLeft ? picKnight.walkLeft : picKnight.walkRight;
                picKnight.BackgroundImage = walkingFrames[picKnight.CurrentFrame];
                picKnight.CurrentFrame = (picKnight.CurrentFrame + 1) % walkingFrames.Count;
            }
        }
    }
}



