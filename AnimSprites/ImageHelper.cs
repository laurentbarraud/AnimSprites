/// <file>ImageHelper.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.4</version>
/// <date>July 3rd, 2025</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimSprites
{
    public static class ImageHelper
    {
        /// <summary>
        /// Extracts a rectangular region from a source image and optionally makes a specific color transparent.
        /// </summary>
        /// <param name="source">The full source image (e.g., a tileset or sprite sheet)</param>
        /// <param name="region">The rectangle to extract (in pixels, relative to the source)</param>
        /// <param name="transparentColor">Optional color to be made transparent. If null, no transparency is applied.</param>
        /// <returns>A Bitmap containing only the selected region, with optional transparency</returns>
        public static Bitmap ExtractSubImage(Image source, Rectangle region, Color? transparentColor = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (region.Width <= 0 || region.Height <= 0)
                throw new ArgumentException("Region size must be positive.");

            if (region.Right > source.Width || region.Bottom > source.Height)
                throw new ArgumentOutOfRangeException(nameof(region), "Region exceeds source image bounds.");

            Bitmap result = new Bitmap(region.Width, region.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                Rectangle destRect = new Rectangle(0, 0, region.Width, region.Height);
                g.DrawImage(source, destRect, region, GraphicsUnit.Pixel);
            }

            // If a transparency color was specified, make it transparent
            if (transparentColor.HasValue)
                result.MakeTransparent(transparentColor.Value);

            return result;
        }

    }
}
