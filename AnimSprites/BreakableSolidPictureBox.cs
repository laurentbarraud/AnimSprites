/// <file>BreakableSolidPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.4</version>
/// <date>July 3rd, 2025</date>

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

        /// <summary>
        /// Creates a breakable solid object from an already prepared image.
        /// </summary>
        /// <param name="backgroundImage">The pre-extracted image to display</param>
        public BreakableSolidPictureBox(Image backgroundImage)
        {
            this.BackgroundImage = backgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
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
