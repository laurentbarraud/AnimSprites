/// <file>SlashEffect.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5.1</version>
/// <date>February 26th, 2026</date>

using System;
using System.Drawing;
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
        /// This method uses the GDI+ rendering system to simulate a sword stroke with
        /// current opacity level. 
        /// By overriding the default painting behavior, it draws a smooth,
        /// stylized line from the top-left to the bottom-right corner of the control.
        /// </summary>
        /// <param name="e">Provides data for the Paint event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a semi-transparent white pen with rounded caps
            using var pen = new Pen(Color.FromArgb(slashOpacity, Color.WhiteSmoke), 5)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            // Enable anti-aliasing for smoother rendering
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw a diagonal line from top-left to bottom-right
            e.Graphics.DrawLine(pen, 0, 0, Width, Height);
        }

    }
}
