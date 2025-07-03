/// <file>SolidPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.4</version>
/// <date>July 3rd, 2025</date>

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public class SolidPictureBox : PictureBox
    {
        private bool isBlinking = false;
        private bool isDragging = false;
        private Point dragOffset;
        private Control levelEditorPanelRef; // Allows to check if the Build menu is visible


        /// <summary>
        /// Starts a flickering animation of the image by briefly inverting its colors.
        /// </summary>
        public async void BlinkIfVisible(Control editorPanel)
        {
            if (!Visible || editorPanel == null || !editorPanel.Visible || isBlinking)
                return;

            if (this.BackgroundImage is Bitmap original)
            {
                isBlinking = true;

                Bitmap inverted = InvertBitmapColors(original);
                this.BackgroundImage = inverted;

                await Task.Delay(200);

                this.BackgroundImage = original;
                isBlinking = false;
            }
        }


        public void EnableEditorBehavior(Control editorPanel)
        {
            levelEditorPanelRef = editorPanel;
            this.MouseDown += Editor_MouseDown;
            this.MouseMove += Editor_MouseMove;
            this.MouseUp += Editor_MouseUp;
            this.Click += Editor_Click;
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (levelEditorPanelRef == null || !levelEditorPanelRef.Visible) return;
            isDragging = true;
            dragOffset = e.Location;
            this.BringToFront();
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || levelEditorPanelRef == null || !levelEditorPanelRef.Visible) return;
            this.Left += e.X - dragOffset.X;
            this.Top += e.Y - dragOffset.Y;
        }

        private void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            if (levelEditorPanelRef != null && levelEditorPanelRef.Visible)
            {
                BlinkIfVisible(levelEditorPanelRef);
            }
        }

        /// <summary>
        /// Inverts the colors one bitmap pixel by pixel (RGB inversion)
        /// </summary>
        private Bitmap InvertBitmapColors(Bitmap source)
        {
            Bitmap inverted = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    Color flipped = Color.FromArgb(pixel.A, 255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    inverted.SetPixel(x, y, flipped);
                }
            }
            return inverted;
        }
    }

}
