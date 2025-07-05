/// <file>SimpleLevelObject.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5</version>
/// <date>July 6th, 2025</date>

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
        public string ObjectType { get; set; } // Takes "Solid" or "Breakable" values
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
