using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimSprites
{
    public static class TileHelper
    {
        /// <summary>
        /// Extracts a rectangular tile image from a tileset using tile coordinates.
        /// </summary>
        public static Image GetTileImage(Image tileset, int tileX, int tileY, int tileWidth, int tileHeight)
        {
            Bitmap bmp = new Bitmap(tileWidth, tileHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle source = new Rectangle(tileX * tileWidth, tileY * tileHeight, tileWidth, tileHeight);
                Rectangle target = new Rectangle(0, 0, tileWidth, tileHeight);
                g.DrawImage(tileset, target, source, GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}
