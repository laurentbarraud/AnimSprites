/// <file>IBreakable.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.5.1</version>
/// <date>February 26th, 2026</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimSprites
{
    public interface IBreakable
    {
        void Hit();                // Called when the object is hit
        bool IsDestroyed { get; } // Returns true if already destroyed
    }
}

