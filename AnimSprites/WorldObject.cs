using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    /// <summary>
    /// This class represents an object of the world
    /// with its logical coordinates (WorldX, WorldY) and visual control.
    /// </summary>
    public class WorldObject
    {
        public Control Ctrl { get; }
        public int WorldX { get; set; }
        public int WorldY { get; set; }

        public WorldObject(Control ctrl, int worldX, int worldY)
        {
            Ctrl = ctrl;
            WorldX = worldX;
            WorldY = worldY;
        }
    }

}
