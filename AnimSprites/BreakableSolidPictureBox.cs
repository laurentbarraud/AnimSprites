/// <file>BreakableSolidPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.4</version>
/// <date>July 2nd, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace AnimSprites
{
    public class BreakableSolidPictureBox : SolidPictureBox, IBreakable
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Health { get; protected set; } = 1;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDestroyed { get; protected set; } = false;

        public BreakableSolidPictureBox(Image tileset, int tileX, int tileY, int tileSize)
        {
            Width = tileSize;
            Height = tileSize;
            BackgroundImage = TileHelper.GetTileImage(tileset, tileX, tileY, tileSize, tileSize);
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Transparent;
        }

        public virtual void Hit()
        {
            if (IsDestroyed) return;

            Health--;

            VisualEffects.PlaySlashEffect(this);

            if (Health <= 0)
            {
                IsDestroyed = true;
                VisualEffects.FadeAndDisappear(this);
            }
        }
    }

}
