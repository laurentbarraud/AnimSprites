/// <file>VisualEffects.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.4.1</version>
/// <date>July 4th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public static class VisualEffects
    {
        /// <summary>
        /// Creates a temporary diagonal slash effect over the given control.
        /// </summary>
        /// <param name="target">The control to overlay the slash on</param>
        public static void PlaySlashEffect(Control target)
        {
            if (target.Parent == null) return;

            var slash = new SlashEffect(target);
            target.Parent.Controls.Add(slash);
            slash.BringToFront();
        }

        /// <summary>
        /// Gradually fades out the control's background and disposes it.
        /// </summary>
        /// <param name="target">The control to fade and remove</param>
        public static void FadeAndDisappear(Control target)
        {
            var timer = new Timer { Interval = 40 };
            timer.Tick += (s, e) =>
            {
                int newAlpha = target.BackColor.A - 25; // the value of transparency that we will apply
                if (newAlpha <= 0)
                {
                    timer.Stop();
                    timer.Dispose();
                    target.Dispose(); 
                }
                else
                {
                    // Updates the background color with the new alpha value
                    target.BackColor = Color.FromArgb(newAlpha, target.BackColor);
                }
            };

            timer.Start();
        }
    }

}
