/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5</version>
/// <date>July 6th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AnimSprites.PlayerPictureBox;
using Image = System.Drawing.Image;

namespace AnimSprites
{
    public partial class frmMain : Form
    {
        // Global viewport offset which indicates how far the "camera" has scrolled
        private int viewportHorizontalOffset = 0;

        // Define the total width of the level
        private int levelWidth = 1000;

        private Panel levelEditorPanel;
        private SolidPictureBox selectedObject = null;
        private TrackBar trkBlockCount; // Stores the slider instance globally
        private Label lblBlockCount; // Displays the current block count selected

        // -----------------------------
        // Rain effect global variables
        // -----------------------------

        // Timer that controls the refresh rate of the rain animation
        private Timer rainTimer;

        // Stores the current positions of all visible raindrops on the screen
        private List<Point> rainDrops = new List<Point>();

        // Random number generator used to initialize and reposition raindrops
        private Random rng = new Random();

        private bool isRaining = false;


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
                Height = 200, // Laissons un peu de marge
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Enabled = false
            };

            int paddingLeft = 10;
            int spacing = 5;
            int currentTop = 10;

            // -----------------------------
            // "Add Platform" Button
            // -----------------------------
            Button addPlatformButton = new Button
            {
                Text = "Add a platform",
                Width = 220,
                Height = 30,
                Left = paddingLeft,
                Top = currentTop
            };
            addPlatformButton.Click += (s, e) =>
            {
                AddObject("Solid", viewportHorizontalOffset + 200, 350, trkBlockCount.Value);
            };
            levelEditorPanel.Controls.Add(addPlatformButton);
            currentTop = addPlatformButton.Bottom + spacing;

            // -----------------------------
            // "Add a bush" Button
            // -----------------------------
            Button addBushButton = new Button
            {
                Text = "Add a bush",
                Width = 220,
                Height = 30,
                Left = paddingLeft,
                Top = currentTop
            };
            addBushButton.Click += (s, e) =>
            {
                AddObject("Breakable", viewportHorizontalOffset + 300, 410);
            };
            levelEditorPanel.Controls.Add(addBushButton);
            currentTop = addBushButton.Bottom + spacing;


            // -----------------------------
            // Label "Number of blocs"
            // -----------------------------
            lblBlockCount = new Label
            {
                Text = $"Number of blocs to add : {trkBlockCount?.Value ?? 6}",
                Left = paddingLeft,
                Top = currentTop,
                Width = 220,
                TextAlign = ContentAlignment.MiddleCenter
            };
            levelEditorPanel.Controls.Add(lblBlockCount);
            currentTop = lblBlockCount.Bottom + spacing;

            // -----------------------------
            // TrackBar for block count
            // -----------------------------
            trkBlockCount = new TrackBar
            {
                Minimum = 1,
                Maximum = 18,
                Value = 6,
                Left = paddingLeft,
                Top = currentTop,
                Width = 220,
                TickFrequency = 1,
                SmallChange = 1,
                LargeChange = 2
            };
            levelEditorPanel.Controls.Add(trkBlockCount);
            currentTop = trkBlockCount.Bottom + spacing;

            // Update label when slider moves
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
                Left = paddingLeft,
                Top = currentTop
            };
            deletePlatformButton.Click += DeleteSelectedObject;
            levelEditorPanel.Controls.Add(deletePlatformButton);
            currentTop = deletePlatformButton.Bottom + spacing;

            // -----------------------------
            // "Weather Toggle" Button
            // -----------------------------
            CheckBox weatherToggle = new CheckBox
            {
                Appearance = Appearance.Button,
                Text = "  Rain Effect",
                Width = 220,
                Height = 30,
                Left = paddingLeft,
                Top = currentTop,
                TextAlign = ContentAlignment.MiddleCenter,
                ImageAlign = ContentAlignment.MiddleRight,
                FlatStyle = FlatStyle.Standard,
                Checked = false,
                Image = Properties.Resources.rain // Default icon
            };

            weatherToggle.CheckedChanged += (s, e) =>
            {
                if (weatherToggle.Checked)
                {
                    weatherToggle.Image = Properties.Resources.sunny;
                    StartRainEffect();
                }
                else
                {
                    weatherToggle.Image = Properties.Resources.rain;
                    StopRainEffect();
                }
            };

            levelEditorPanel.Controls.Add(weatherToggle);
            currentTop = weatherToggle.Bottom + spacing;

            // Resize the panel to fit all controls
            levelEditorPanel.Height = currentTop + spacing;

            // -----------------------------
            // Add the Menu Panel to the Form
            // -----------------------------
            this.Controls.Add(levelEditorPanel);
          
            // ----------------------------------------------------------------
            // Converts designer PictureBoxes to SolidPictureBox for collisions
            // ----------------------------------------------------------------
            ConvertToSolidPictureBox(ref picGround);
                      
            // Set initial motionless images
            picGround.Width = levelWidth;

            // Loads level solid and breakable objects
            LoadLevel();
        }

        private void AddObject(string type, int positionX, int positionY, int middleBlockCount = 3)
        {
            PictureBox pb;

            if (type == "Breakable")
            {
                pb = new BreakableSolidPictureBox
                {
                    Width = 40,
                    Height = 40,
                    BackgroundImage = Properties.Resources.bush,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackColor = Color.Transparent
                };
            }
            else // Solid platform
            {
                // Load the tileset image and define source rectangles
                Bitmap tileSet = AnimSprites.Properties.Resources.nature_tileset;
                Rectangle srcRectLeft = new Rectangle(4, 2, 28, 31);
                Rectangle srcRectMiddle = new Rectangle(36, 2, 30, 31);
                Rectangle srcRectRight = new Rectangle(68, 2, 26, 31);

                Bitmap platformBitmap = FillPlatformWithTextures(middleBlockCount, srcRectLeft, srcRectMiddle, srcRectRight, tileSet);

                pb = new SolidPictureBox
                {
                    Width = platformBitmap.Width,
                    Height = platformBitmap.Height,
                    BackgroundImage = platformBitmap,
                    BackgroundImageLayout = ImageLayout.None,
                    BackColor = Color.Transparent
                };
            }

            pb.Left = positionX;
            pb.Top = positionY;
            pb.Name = $"{type}_{positionX}_{positionY}";

            if (pb is SolidPictureBox solid)
            {
                solid.EnableEditorBehavior(levelEditorPanel);
            }

            pb.Click += SelectObject;

            this.Controls.Add(pb);
            pb.SendToBack();

            SaveLevel();
        }



        private void AddPlatform(object sender, EventArgs e)
        {
            if (!levelEditorPanel.Visible)
            {
                return;
            }

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

            newPlatform.EnableEditorBehavior(levelEditorPanel);
            newPlatform.Click += SelectObject;


            // Add the new platform to the form
            this.Controls.Add(newPlatform);

            SaveLevel();
        }


        // Game loop tick: update the game logic.
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            UpdateSpriteWalkingAnimation(); // Handles only sprite animation
            UpdateGame(); // Handles movement and scrolling
        }

        /// Checks for nearby breakable objects within the player's attack hitbox,
        /// and applies damage to the closest one — only once per attack.
        private void CheckBreakableCollisionsWithPlayer()
        {
            // If the player already dealt damage during this attack, skip collision check
            if (picKnight.HasDealtDamageThisAttack)
                return;

            // Define the player's weapon hitbox based on facing direction
            Rectangle playerWeaponHitbox = picKnight.FacingLeft
                ? new Rectangle(
                    picKnight.Left - 20,           // area to the left of the player
                    picKnight.Top + 10,            // slight vertical offset
                    20,                            // narrow horizontal range
                    picKnight.Height - 20          // avoids touching feet/head
                )
                : new Rectangle(
                    picKnight.Right,              // area to the right of the player
                    picKnight.Top + 10,
                    20,
                    picKnight.Height - 20
                );

            // Create a larger area around the weapon hitbox to limit the search space
            Rectangle collisionSearchZone = Rectangle.Inflate(playerWeaponHitbox, 40, 20);

            BreakableSolidPictureBox closestBreakableObject = null;
            int closestObjectDistanceSquared = int.MaxValue;

            // Loop through all scene controls to find nearby breakable objects
            foreach (Control control in this.Controls)
            {
                if (control is BreakableSolidPictureBox breakableObject &&
                    breakableObject.Visible &&
                    collisionSearchZone.IntersectsWith(breakableObject.Bounds))
                {
                    // Make sure the object actually intersects the precise weapon hitbox
                    if (!playerWeaponHitbox.IntersectsWith(breakableObject.Bounds))
                        continue;

                    // Calculate squared distance from player center to object center
                    int playerCenterX = picKnight.Left + picKnight.Width / 2;
                    int playerCenterY = picKnight.Top + picKnight.Height / 2;

                    int objectCenterX = breakableObject.Left + breakableObject.Width / 2;
                    int objectCenterY = breakableObject.Top + breakableObject.Height / 2;

                    int deltaX = objectCenterX - playerCenterX;
                    int deltaY = objectCenterY - playerCenterY;
                    int distanceSquared = deltaX * deltaX + deltaY * deltaY;

                    // Update if this object is the closest so far
                    if (distanceSquared < closestObjectDistanceSquared)
                    {
                        closestObjectDistanceSquared = distanceSquared;
                        closestBreakableObject = breakableObject;
                    }
                }
            }

            // If a valid target was found, apply damage and lock attack state
            if (closestBreakableObject != null)
            {
                closestBreakableObject.Hit();
                picKnight.HasDealtDamageThisAttack = true;
            }
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

        private void DeleteSelectedObject(object sender, EventArgs e)
        {
            if (selectedObject != null)
            {
                this.Controls.Remove(selectedObject); // Remove platform from the form
                selectedObject = null; // Reset selection after deletion

                SaveLevel();
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


        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Prevent system beep on handled keys
            e.Handled = true;
            e.SuppressKeyPress = true;

            bool shouldAnimate = false;

            // Player movement (left/right) — only when editor is hidden
            if (!levelEditorPanel.Visible)
            {
                if (e.KeyCode == Keys.Left)
                {
                    picKnight.IsMovingLeft = true;
                    picKnight.FacingLeft = true;
                    shouldAnimate = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    picKnight.IsMovingRight = true;
                    picKnight.FacingLeft = false;
                    shouldAnimate = true;
                }
                else if (e.KeyCode == Keys.Space && picKnight.Status == PlayerStatus.IsGrounded)
                {
                    picKnight.Status = PlayerStatus.IsJumping;
                    picKnight.JumpSpeed = picKnight.InitialJumpSpeed;
                    shouldAnimate = true;
                }
                else if (e.KeyCode == Keys.ControlKey && !picKnight.IsAttacking)
                {
                    picKnight.StartAttack();
                    shouldAnimate = true;
                }
            }

            // Scroll viewport left
            if (e.KeyCode == Keys.A && viewportHorizontalOffset > 0)
            {
                viewportHorizontalOffset -= 20;
                ScrollLevel(20);
            }
            // Scroll viewport right
            else if (e.KeyCode == Keys.D && viewportHorizontalOffset + this.ClientSize.Width < levelWidth)
            {
                viewportHorizontalOffset += 20;
                ScrollLevel(-20);
            }

            // Toggle level editor panel
            else if (e.KeyCode == Keys.B)
            {
                levelEditorPanel.Visible = !levelEditorPanel.Visible;
                levelEditorPanel.Enabled = levelEditorPanel.Visible;
            }

            // Close editor with Escape
            else if (e.KeyCode == Keys.Escape && levelEditorPanel.Visible)
            {
                levelEditorPanel.Visible = false;
                levelEditorPanel.Enabled = false;
            }

            // Delete selected object in editor
            else if (e.KeyCode == Keys.Delete && levelEditorPanel.Visible)
            {
                DeleteSelectedObject(sender, e);
            }

            // Start animation if needed
            if (shouldAnimate)
            {
                animTimer.Start();
            }
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

            // Stops animation only if the player is at full stop
            if (!picKnight.IsMovingLeft && !picKnight.IsMovingRight && !picKnight.IsAttacking && picKnight.Status != PlayerStatus.IsJumping && picKnight.Status != PlayerStatus.IsFalling)
            {
                animTimer.Stop();
            }
        }

        /// <summary>
        /// Load platforms and bushes with default textures based on type
        /// </summary>
        private void LoadLevel()
        {
            string json = Properties.Settings.Default.LevelData;
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            try
            {
                List<SimpleLevelObject> savedObjects = JsonSerializer.Deserialize<List<SimpleLevelObject>>(json);

                foreach (SimpleLevelObject data in savedObjects)
                {
                    PictureBox pb;

                    // Create object based on type
                    if (data.ObjectType == "Breakable")
                        pb = new BreakableSolidPictureBox();
                    else
                        pb = new SolidPictureBox();

                    // Restore position and size
                    pb.Location = new Point(data.PositionX, data.PositionY);
                    pb.Size = new Size(data.Width, data.Height);
                    pb.BackgroundImageLayout = ImageLayout.Stretch;

                    // Apply default visuals based on object type
                    if (data.ObjectType == "Breakable")
                    {
                        pb.BackgroundImage = Properties.Resources.bush;
                    }
                    else
                    {
                        // Reconstruct platform texture dynamically from tileset
                        Bitmap tileSet = Properties.Resources.nature_tileset;

                        // Define the rectangles inside the tileset image
                        Rectangle srcRectLeft = new Rectangle(4, 2, 28, 31);
                        Rectangle srcRectMiddle = new Rectangle(36, 2, 30, 31);
                        Rectangle srcRectRight = new Rectangle(68, 2, 26, 31);

                        // Estimate the number of middle blocks based on total width
                        int totalEdgeWidth = srcRectLeft.Width + srcRectRight.Width;
                        int availableMiddleWidth = Math.Max(0, data.Width - totalEdgeWidth);
                        int middleBlockCount = availableMiddleWidth / srcRectMiddle.Width;

                        // Build the final composite image
                        pb.BackgroundImage = FillPlatformWithTextures(middleBlockCount, srcRectLeft, srcRectMiddle, srcRectRight, tileSet);
                        pb.BackgroundImageLayout = ImageLayout.None;
                    }

                    // Assign name and enable interactivity
                    pb.Name = $"{data.ObjectType}_{data.PositionX}_{data.PositionY}";

                    if (pb is SolidPictureBox solid)
                    {
                        solid.EnableEditorBehavior(levelEditorPanel);
                    }

                    pb.Click += SelectObject;

                    this.Controls.Add(pb);
                    pb.SendToBack();
                }
            }
            catch
            {
                MessageBox.Show("Could not load saved level.");
                Properties.Settings.Default.LevelData = "";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Custom paint routine that renders a scrolling background and optional rain effect.
        /// </summary>
        /// <param name="e">Provides data for the Paint event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // --- Scrolling background rendering ---
            if (this.BackgroundImage != null)
            {
                // Retrieve background image dimensions
                int backgroundImageWidth = this.BackgroundImage.Width;

                // Calculate the horizontal offset for parallax scrolling
                int backgroundScrollOffset = (int)(viewportHorizontalOffset * 0.5) % backgroundImageWidth;

                // Draw the background image repeatedly to create a seamless scrolling effect
                for (int positionX = -backgroundScrollOffset; positionX < this.ClientSize.Width; positionX += backgroundImageWidth)
                {
                    e.Graphics.DrawImage(this.BackgroundImage, positionX, 0, backgroundImageWidth, this.ClientSize.Height);
                }
            }

            // --- Rain effect rendering ---
            if (isRaining)
            {
                // Create a semi-transparent light blue pen for raindrops
                using Pen rainPen = new Pen(Color.FromArgb(180, Color.LightBlue), 2);

                // Draw each raindrop as a short diagonal line
                foreach (Point drop in rainDrops)
                {
                    e.Graphics.DrawLine(rainPen, drop.X, drop.Y, drop.X + 2, drop.Y + 12);
                }
            }

            // Call base method to ensure standard control rendering
            base.OnPaint(e);
        }


        /// <summary>
        /// Saves all Solid and Breakable objects into a JSON string in app settings.
        /// </summary>
        public void SaveLevel()
        {
            List<SimpleLevelObject> allObjects = new List<SimpleLevelObject>();

            foreach (Control control in this.Controls)
            {
                // Only save SolidPictureBox or BreakableSolidPictureBox
                if (control is SolidPictureBox)
                {
                    string type = control is BreakableSolidPictureBox ? "Breakable" : "Solid";

                    SimpleLevelObject obj = new SimpleLevelObject
                    {
                        ObjectType = type,
                        PositionX = control.Left,
                        PositionY = control.Top,
                        Width = control.Width,
                        Height = control.Height
                    };

                    allObjects.Add(obj);
                }
            }

            // Convert list to JSON and save it
            string json = JsonSerializer.Serialize(allObjects);
            Properties.Settings.Default.LevelData = json;
            Properties.Settings.Default.Save();
        }


        private void SelectObject(object sender, EventArgs e)
        {
            if (!levelEditorPanel.Visible) return;

            if (sender is SolidPictureBox obj)
            {
                selectedObject = obj;
                obj.BlinkIfVisible(levelEditorPanel);
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

        /// <summary>
        /// Initializes and starts the rain animation by generating raindrop positions
        /// and launching a timer to update their movement.
        /// </summary>
        private void StartRainEffect()
        {
            isRaining = true;

            // Clear any existing raindrops
            rainDrops.Clear();

            // Generate initial raindrop positions randomly across the screen
            for (int i = 0; i < 100; i++)
            {
                int x = rng.Next(0, this.ClientSize.Width);
                int y = rng.Next(0, this.ClientSize.Height);
                rainDrops.Add(new Point(x, y));
            }

            // Create and configure the timer to animate the rain
            rainTimer = new Timer();
            rainTimer.Interval = 30; // Controls the speed of the rain animation
            rainTimer.Tick += (s, e) =>
            {
                // Move each raindrop downward
                for (int i = 0; i < rainDrops.Count; i++)
                {
                    Point p = rainDrops[i];
                    p.Y += 10; // Fall speed in pixels

                    // Reset raindrop to top if it falls below the screen
                    if (p.Y > this.ClientSize.Height)
                        p = new Point(rng.Next(0, this.ClientSize.Width), 0);

                    rainDrops[i] = p;
                }

                // Request a repaint to show updated raindrop positions
                this.Invalidate();
            };

            rainTimer.Start();
        }

        /// <summary>
        /// Stops the rain animation and clears all raindrop data from memory.
        /// </summary>
        private void StopRainEffect()
        {
            isRaining = false;

            // Stop and dispose the timer if it exists
            rainTimer?.Stop();
            rainTimer = null;

            // Clear all raindrops from the screen
            rainDrops.Clear();

            // Force a repaint to remove any remaining visuals
            this.Invalidate();
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

                // Select appropriate animation frame
                List<Bitmap> jumpFrames = picKnight.FacingLeft ? picKnight.jumpLeft : picKnight.jumpRight;

                // Prevent index overflow
                int totalFrames = jumpFrames.Count;
                int frameIndex = Math.Min(picKnight.CurrentFrame, totalFrames - 1);

                // Set the current sprite image
                picKnight.BackgroundImage = jumpFrames[frameIndex];

                // Advance the frame counter
                if (picKnight.CurrentFrame < totalFrames - 1)
                    picKnight.CurrentFrame++;

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

                // Deal damage on frame 1 if it hasn't been done yet
                if (picKnight.CurrentFrame == 1 && !picKnight.HasDealtDamageThisAttack)
                {
                    CheckBreakableCollisionsWithPlayer();          // Only one hit possible per attack
                    picKnight.HasDealtDamageThisAttack = true;        
                }

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



