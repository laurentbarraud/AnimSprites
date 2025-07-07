/// <file>SolidPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5</version>
/// <date>July 6th, 2025</date>

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public class SolidPictureBox : PictureBox
    {
        // Indicates whether the object is currently blinking (e.g., for selection feedback)
        private bool isBlinking = false;

        // Tracks whether the object is being dragged with the mouse
        private bool isDragging = false;

        // Stores the offset between the mouse position and the top-left corner of the object during dragging
        private Point dragOffset;

        // Reference to the level editor panel, used to determine if editor interactions should be active
        private Control levelEditorPanelRef;

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

        /// <summary>
        /// Enables drag-and-drop and selection behavior for this object within the level editor.
        /// Attaches mouse event handlers to allow repositioning and interaction when the editor is active.
        /// </summary>
        /// <param name="editorPanel">The panel representing the level editor interface.</param>
        public void EnableEditorBehavior(Control editorPanel)
        {
            levelEditorPanelRef = editorPanel;
            this.MouseDown += Editor_MouseDown;
            this.MouseMove += Editor_MouseMove;
            this.MouseUp += Editor_MouseUp;
            this.Click += Editor_Click;
        }
        /// <summary>
        /// Handles the start of a drag operation when the user clicks on the object.
        /// Stores the mouse offset and brings the object to the front.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (levelEditorPanelRef == null || !levelEditorPanelRef.Visible) return;
            isDragging = true;
            dragOffset = e.Location;
            this.BringToFront();
        }


        /// <summary>
        /// Updates the object's position as the user drags it with the mouse.
        /// Movement is only allowed when the editor panel is visible.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || levelEditorPanelRef == null || !levelEditorPanelRef.Visible) return;
            this.Left += e.X - dragOffset.X;
            this.Top += e.Y - dragOffset.Y;
        }

        /// <summary>
        /// Ends the drag operation when the mouse button is released.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Mouse event data.</param>
        private void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;

            // Calling SaveLevel() on the parent form (frmMain)
            (this.FindForm() as frmMain)?.SaveLevel();
        }


        /// <summary>
        /// Handles click events on the object while the editor is active.
        /// Triggers a visual blink effect to indicate selection.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data.</param>
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
