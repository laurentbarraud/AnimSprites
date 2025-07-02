/// <file>SlashEffect.cs</file>
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

namespace AnimSprites
{
    public class SlashEffect : Control
    {
        // Opacity level of the slash stroke (0 = transparent, 255 = fully visible)
        private int slashOpacity = 255;

        private readonly Timer timer;

        /// <summary>
        /// Creates a fading diagonal slash effect over the target control.
        /// </summary>
        /// <param name="target">The control to overlay this effect on</param>
        public SlashEffect(Control target)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |   // Allows to use Color.Transparent as a background color.
                     ControlStyles.OptimizedDoubleBuffer |          // Active the double buffering, avoiding flickers during redrawings.
                     ControlStyles.AllPaintingInWmPaint |           // Prevents OnPaintBackground from being called separately.
                                                                    // Everything is drawn in OnPaint.
                     ControlStyles.UserPaint, true);                // Indicates that I manage the drawing via OnPaint (not Windows Forms) 

            Width = target.Width;
            Height = target.Height;
            Left = target.Left;
            Top = target.Top;
            BackColor = Color.Transparent;

            timer = new Timer { Interval = 30 };
            timer.Tick += (s, e) =>
            {
                slashOpacity -= 50;

                if (slashOpacity <= 0)
                {
                    timer.Stop();
                    Dispose(); // Removes this effect from the form
                }
                else
                {
                    Invalidate(); // Triggers repaint with the updated opacity
                }
            };

            timer.Start();
        }

        /// <summary>
        /// Renders a diagonal slash line with current opacity level.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(slashOpacity, Color.WhiteSmoke), 5)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(pen, 0, 0, Width, Height); // Top-left to bottom-right
        }
    }
}
