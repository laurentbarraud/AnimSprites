using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimSprites
{
    public class PlayerPictureBox : PictureBox
    {
        public bool isGrounded { get; set; } = false;  

        public PlayerPictureBox()
        {

        }
    }
}
