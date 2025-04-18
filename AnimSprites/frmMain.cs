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
        private int groundLevel = 360;
       
        private int PlatformLeftEdge => picPlateforme.Left;
        private int PlatformRightEdge => picPlateforme.Right;



        public frmMain()
        {
            InitializeComponent();
        }

        private void frmAnimSprites_Load(object sender, EventArgs e)
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

            // Place the sprite on the platform at startup
            picKnight.Top = picPlateforme.Top - picKnight.Height; 


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
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            // Vérifier si le sprite doit commencer à tomber
            if (!isFalling && (picKnight.Right <= PlatformLeftEdge || picKnight.Left >= PlatformRightEdge))
            {
                isFalling = true;
            }

            // Appliquer la gravité si le sprite est en chute libre
            if (isFalling)
            {
                picKnight.Top += gravity;

                // Arrêter la chute lorsqu'il touche le sol
                if (picKnight.Top >= groundLevel)
                {
                    picKnight.Top = groundLevel;
                    isFalling = false;
                }
            }

            // Permettre le déplacement horizontal à tout moment, même après la chute
            if (isMovingLeft || isMovingRight)
            {
                List<Bitmap> animationFrames = isMovingLeft ? knightWalkLeft : knightWalkRight;
                picKnight.BackgroundImage = animationFrames[currentFrame];
                picKnight.Left += isMovingRight ? 5 : -5;
                currentFrame = (currentFrame + 1) % animationFrames.Count;
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                isMovingLeft = true;
                isMovingRight = false;
                animTimer.Start();
            }
            else if (e.KeyCode == Keys.Right)
            {
                isMovingRight = true;
                isMovingLeft = false;
                animTimer.Start();
            }

            // Si le sprite est au sol, relancer la possibilité de bouger
            if (picKnight.Top == groundLevel)
            {
                isFalling = false;  // Assurer que la chute est bien arrêtée
            }
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                isMovingLeft = false;
                isMovingRight = false;
                // Ne stopper le timer que si le sprite n'est PAS en chute.
                if (!isFalling)
                {
                    animTimer.Stop();
                }
            }
        }
    }
}


