/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.1</version>
/// <date>April 19th, 2025</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
        private bool isFalling = false;
        private int gravity = 5;
        private int groundLevel = 340;

        private int PlatformLeftEdge => picPlateforme?.Left ?? 0;
        private int PlatformRightEdge => picPlateforme?.Right ?? 0;

        // Define two solid objects: a platform and a left-side wall
        private SolidPictureBox picPlateforme;
        private SolidPictureBox picWall;

        private bool isGrounded = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmAnimSprites_Load(object sender, EventArgs e)
        {
            // Load sprite animations
            for (int i = 1; i <= 10; i++)
            {
                Bitmap bmpLeft = (Bitmap)Properties.Resources.ResourceManager.GetObject($"walk{(i < 10 ? "0" : "")}{i}_left");
                knightWalkLeft.Add(bmpLeft);

                Bitmap bmpRight = (Bitmap)Properties.Resources.ResourceManager.GetObject($"walk{(i < 10 ? "0" : "")}{i}_right");
                knightWalkRight.Add(bmpRight);
            }

            // Set initial idle image (first frame facing right)
            picKnight.BackgroundImage = knightWalkRight[0];

            // Load the tileset for platform rendering
            Bitmap bmpTileSet = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Platform tile properties
            int tileWidth = 30;
            int tileHeight = 31;
            int tileSpacing = 2;

            // Define cropped tile sections
            Rectangle srcRectLeftPlatform = new Rectangle(4, 2, tileWidth - 2, tileHeight);
            Rectangle srcRectMiddlePlatform = new Rectangle(34 + tileSpacing, 2, tileWidth, tileHeight);
            Rectangle srcRectRightPlatform = new Rectangle(66 + 2 * tileSpacing, 2, tileWidth - 4, tileHeight);

            // Create the full platform texture (8 tiles wide)
            int totalWidth = (tileWidth - 2) + (6 * tileWidth) + (tileWidth - 4);
            Bitmap platformBitmap = new Bitmap(totalWidth, tileHeight);

            using (Graphics g = Graphics.FromImage(platformBitmap))
            {
                GraphicsUnit units = GraphicsUnit.Pixel;

                // Draw left end of the platform
                g.DrawImage(bmpTileSet, new Rectangle(0, 0, tileWidth - 2, tileHeight), srcRectLeftPlatform, units);

                // Draw 6 middle sections
                for (int i = 0; i < 6; i++)
                {
                    int xPosition = (tileWidth - 2) + (i * tileWidth);
                    g.DrawImage(bmpTileSet, new Rectangle(xPosition, 0, tileWidth, tileHeight), srcRectMiddlePlatform, units);
                }

                // Draw right end of the platform
                int rightXPosition = (tileWidth - 2) + (6 * tileWidth);
                g.DrawImage(bmpTileSet, new Rectangle(rightXPosition, 0, tileWidth - 4, tileHeight), srcRectRightPlatform, units);
            }

            // Create the solid platform
            picPlateforme = new SolidPictureBox
            {
                Left = 200,
                Top = 300, 
                Width = totalWidth,
                Height = tileHeight,
                BackgroundImage = platformBitmap,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            this.Controls.Add(picPlateforme);

            // Place the sprite correctly at the start
            picKnight.Top = picPlateforme.Top - picKnight.Height;
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            isGrounded = false; // Assume the sprite is in the air unless proven otherwise

            // Check if the sprite is standing on a solid object (prevent falling through, allow passing below)
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SolidPictureBox solidObj)
                {
                    // Block only if the sprite is falling onto the platform
                    if (picKnight.Bottom >= solidObj.Top && picKnight.Bottom - gravity < solidObj.Top &&
                        picKnight.Right > solidObj.Left && picKnight.Left < solidObj.Right &&
                        picKnight.Top < solidObj.Top) // Block only if coming from above
                    {
                        picKnight.Top = solidObj.Top - picKnight.Height; // Place the sprite correctly on top
                        isGrounded = true;
                        break;
                    }
                }
            }

            // Ensure the sprite does not fall below groundLevel
            if (picKnight.Top >= groundLevel)
            {
                picKnight.Top = groundLevel;
                isGrounded = true;
            }

            // Apply gravity only if the sprite is not grounded
            isFalling = !isGrounded;
            if (isFalling)
            {
                picKnight.Top += gravity;
            }

            // Allow horizontal movement while preventing unwanted blocking
            if (isMovingLeft || isMovingRight)
            {
                int nextPosition = isMovingRight ? picKnight.Left + 5 : picKnight.Left - 5;
                bool canMove = true;

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is SolidPictureBox solidObj)
                    {
                        // Prevent horizontal collision only if sprite is not fully below the platform
                        if (picKnight.Top < solidObj.Top && nextPosition + picKnight.Width > solidObj.Left && nextPosition < solidObj.Right &&
                            picKnight.Bottom > solidObj.Top) // Allow passing below the platform!
                        {
                            canMove = false;
                            break;
                        }
                    }
                }

                if (canMove)
                {
                    List<Bitmap> animationFrames = isMovingLeft ? knightWalkLeft : knightWalkRight;
                    picKnight.BackgroundImage = animationFrames[currentFrame];
                    picKnight.Left += isMovingRight ? 5 : -5;
                    currentFrame = (currentFrame + 1) % animationFrames.Count;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            int nextPositionRight = picKnight.Left + 5;
            int nextPositionLeft = picKnight.Left - 5;
            bool canMoveRight = true;
            bool canMoveLeft = true;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SolidPictureBox solidObj)
                {
                    // Block right movement if an obstacle is to the right of the sprite or if edge is reached
                    if (nextPositionRight + picKnight.Width > solidObj.Left &&
                        picKnight.Top < solidObj.Bottom && picKnight.Bottom > solidObj.Top)
                    {
                        canMoveRight = false;
                    }

                    // Block the movement to left if an obstacle is to the left of the sprite or if edge reached
                    if (nextPositionLeft < solidObj.Right &&
                        picKnight.Top < solidObj.Bottom && picKnight.Bottom > solidObj.Top)
                    {
                        canMoveLeft = false;
                    }

                    // Detect if the sprite is on a platform
                    if (picKnight.Bottom >= solidObj.Top && picKnight.Bottom <= solidObj.Top + 5 &&
                        picKnight.Left + picKnight.Width > solidObj.Left && picKnight.Left < solidObj.Right)
                    {
                        isGrounded = true; // Placed on a solid element
                    }
                    else if (picKnight.Bottom < solidObj.Top)
                    {
                        isGrounded = false; // In free fall
                    }
                }
            }

            // Check the edges before running the move
            if (picKnight.Left <= 0)
            {
                canMoveLeft = false;
            }
            if (picKnight.Right >= this.ClientSize.Width)
            {
                canMoveRight = false;
            }

            // If the sprite is in the air, it should fall even if no key is pressed
            if (!isGrounded)
            {
                picKnight.Top += gravity; // Adds a downward motion to simulate gravity
            }

            // Execute move only if allowed
            if (e.KeyCode == Keys.Left)
            {
                if (canMoveLeft)
                {
                    isMovingLeft = true;
                    isMovingRight = false;
                    animTimer.Start();
                }
                else
                {
                    animTimer.Stop();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (canMoveRight)
                {
                    isMovingRight = true;
                    isMovingLeft = false;
                    animTimer.Start();
                }
                else
                {
                    animTimer.Stop();
                }
            }
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                isMovingLeft = false;
                isMovingRight = false;
                // Only stop the timer if the sprite is not falling.
                if (!isFalling)
                {
                    animTimer.Stop();
                }
            }
        }
    }
}


