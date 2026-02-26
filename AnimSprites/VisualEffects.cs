/// <file>VisualEffects.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5.1</version>
/// <date>February 26th, 2026</date>

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
        public static void PlaySlashEffect(Control targetControl)
        {
            if (targetControl.Parent == null)
            {
                return;
            }
            
            var slashEffect = new SlashEffect(targetControl);
            targetControl.Parent.Controls.Add(slashEffect);
            slashEffect.BringToFront();
        }

        /// <summary>
        /// Gradually fades out the control's background and disposes it.
        /// </summary>
        /// <param name="target">The control to fade and remove</param>
        public static void FadeAndDisappear(Control targetControl)
        {
            var timer = new Timer { Interval = 40 };
            timer.Tick += (s, e) =>
            {
                int newAlphaColorValue = targetControl.BackColor.A - 25; // the value of transparency that we will apply
            
                if (newAlphaColorValue <= 0)
                {
                    timer.Stop();
                    timer.Dispose();
                    targetControl.Dispose(); 
                }
                else
                {
                    // Updates the background color with the new alpha value
                    targetControl.BackColor = Color.FromArgb(newAlphaColorValue, targetControl.BackColor);
                }
            };

            timer.Start();
        }
    }

}
