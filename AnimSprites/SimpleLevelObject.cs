using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimSprites
{
    // Represents a platform or breakable object for saving/loading purposes.
    public class SimpleLevelObject
    {
        public string ObjectType { get; set; } // "Solid" or "Breakable"
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
