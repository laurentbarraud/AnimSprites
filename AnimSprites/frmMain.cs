/// <file>frmMain.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.1</version>
/// <date>April 13th, 2025</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmAnimSprites_Load(object sender, EventArgs e)
        {
            // Load the tileset image
            Bitmap bmpTileSet = new Bitmap(AnimSprites.Properties.Resources.nature_tileset);

            // Define the actual dimensions of each tile and the spacing between tiles
            int tileWidth = 30;  // Standard width of the tiles (middle tiles)
            int tileHeight = 31; // Height of all tiles
            int tileSpacing = 2; // Space between tiles in the tileset

            // Define source rectangles with adjusted cropping
            Rectangle srcRectLeft = new Rectangle(4, 2, tileWidth - 2, tileHeight);  // Left tile: 2 pixels less on the left
            Rectangle srcRectMiddle = new Rectangle(34 + tileSpacing, 2, tileWidth, tileHeight); // Middle tile remains the same
            Rectangle srcRectRight = new Rectangle(66 + 2 * tileSpacing, 2, tileWidth - 4, tileHeight); // Right tile: 2 pixels less on the right

            // Create a new bitmap to represent the full platform (8 tiles wide)
            int totalWidth = (tileWidth - 2) + (6 * tileWidth) + (tileWidth - 4); // Calculate total width precisely
            Bitmap platformBitmap = new Bitmap(totalWidth, tileHeight);

            // Create a Graphics object to draw on the new bitmap
            using (Graphics g = Graphics.FromImage(platformBitmap))
            {
                GraphicsUnit units = GraphicsUnit.Pixel;

                // Draw the left end of the platform
                g.DrawImage(bmpTileSet, new Rectangle(0, 0, tileWidth - 2, tileHeight), srcRectLeft, units);

                // Draw the 6 middle sections of the platform
                for (int i = 0; i < 6; i++)
                {
                    int xPosition = (tileWidth - 2) + (i * tileWidth);
                    g.DrawImage(bmpTileSet, new Rectangle(xPosition, 0, tileWidth, tileHeight), srcRectMiddle, units);
                }

                // Draw the right end of the platform
                int rightXPosition = (tileWidth - 2) + (6 * tileWidth);
                g.DrawImage(bmpTileSet, new Rectangle(rightXPosition, 0, tileWidth - 4, tileHeight), srcRectRight, units);
            }

            // Set the background image of the PictureBox
            picPlateforme.BackgroundImage = platformBitmap;

            // Make the background image stretch to fill the PictureBox
            picPlateforme.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}


