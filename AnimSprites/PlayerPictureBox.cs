/// <file>PlayerPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.1</version>
/// <date>April 22th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnimSprites
{
    public class PlayerPictureBox : PictureBox
    {
        // Enum defining the player's states (grounded, jumping, falling)
        public enum PlayerStatus
        {
            IsGrounded, // The player is standing on a solid object
            IsJumping,  // The player is actively jumping
            IsFalling   // The player is in the air, falling due to gravity
        }

        public PlayerStatus Status { get; set; } = PlayerStatus.IsFalling;

        // Lists storing animations for walking and jumping
        public List<Bitmap> walkLeft { get; private set; }
        public List<Bitmap> walkRight { get; private set; }
        public List<Bitmap> jumpLeft { get; private set; }
        public List<Bitmap> jumpRight { get; private set; }

        // Current animation frame for the player
        public int CurrentFrame { get; set; } = 0;

        public int Gravity { get; set; } = 5; // Default gravity value

        // Walking speed of the player (pixels per tick)
        public int WalkingSpeed { get; set; } = 5; // Default walking speed

        // Initial jump force applied when the player starts jumping
        public int InitialJumpSpeed { get; set; } = 10;

        // Dynamic jump speed updated during the jump
        public int JumpSpeed { get; set; } = 0;


        public bool IsMovingLeft { get; set; } = false;
        public bool IsMovingRight { get; set; } = false;


        public PlayerPictureBox()
        {
            // Load all animations automatically when the player object is created
            LoadAnimations();
        }

        // Load all player animations for walking and jumping
        public void LoadAnimations()
        {
            // Walking animations (Left & Right)
            walkLeft = new List<Bitmap>
            {
                Properties.Resources.walk01_left,
                Properties.Resources.walk02_left,
                Properties.Resources.walk03_left,
                Properties.Resources.walk04_left,
                Properties.Resources.walk05_left,
                Properties.Resources.walk06_left,
                Properties.Resources.walk07_left,
                Properties.Resources.walk08_left,
                Properties.Resources.walk09_left,
                Properties.Resources.walk10_left
            };

            walkRight = new List<Bitmap>
            {
                Properties.Resources.walk01_right,
                Properties.Resources.walk02_right,
                Properties.Resources.walk03_right,
                Properties.Resources.walk04_right,
                Properties.Resources.walk05_right,
                Properties.Resources.walk06_right,
                Properties.Resources.walk07_right,
                Properties.Resources.walk08_right,
                Properties.Resources.walk09_right,
                Properties.Resources.walk10_right
            };

            // Jumping animations (Left & Right)
            jumpLeft = new List<Bitmap>
            {
                Properties.Resources.jump01_left,
                Properties.Resources.jump02_left,
                Properties.Resources.jump03_left,
                Properties.Resources.jump04_left,
                Properties.Resources.jump05_left,
                Properties.Resources.jump06_left,
                Properties.Resources.jump07_left,
                Properties.Resources.jump08_left,
                Properties.Resources.jump09_left,
                Properties.Resources.jump10_left
            };

            jumpRight = new List<Bitmap>
            {
                Properties.Resources.jump01_right,
                Properties.Resources.jump02_right,
                Properties.Resources.jump03_right,
                Properties.Resources.jump04_right,
                Properties.Resources.jump05_right,
                Properties.Resources.jump06_right,
                Properties.Resources.jump07_right,
                Properties.Resources.jump08_right,
                Properties.Resources.jump09_right,
                Properties.Resources.jump10_right
            };
        }

    }
}
