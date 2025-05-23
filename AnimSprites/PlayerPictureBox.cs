﻿/// <file>PlayerPictureBox.cs</file>
/// <author>Laurent Barraud</author>
/// <version>0.3.1</version>
/// <date>May 14th, 2025</date>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PlayerStatus Status { get; set; } = PlayerStatus.IsFalling;

         // Lists storing animations for walking and jumping
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> walkLeft { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> walkRight { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> jumpLeft { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> jumpRight { get; private set; }

        // Lists storing animations for attack and jump-attack
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> attackLeft { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> attackRight { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> jumpAttackLeft { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Bitmap> jumpAttackRight { get; set; }

        // Property to track whether the character is currently attacking
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAttacking { get; set; } = false;

        // Current animation frame for the player
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentFrame { get; set; } = 0;

        // Walking speed of the player (pixels per tick)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int WalkingSpeed { get; set; } = 1; // Default walking speed

        // Initial jump force applied when the player starts jumping
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InitialJumpSpeed { get; set; } = 20;

        // Dynamic jump speed updated during the jump
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int JumpSpeed { get; set; } = 0;

        // Controls jump height amplification
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double JumpMultiplier { get; set; } = 2;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Gravity { get; set; } = 5; // Default gravity value

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMovingLeft { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMovingRight { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FacingLeft { get; set; } = true;



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

            // Attack animations (Left & Right)
            attackLeft = new List<Bitmap>
            {
                Properties.Resources.attack01_left,
                Properties.Resources.attack02_left,
                Properties.Resources.attack03_left,
                Properties.Resources.attack04_left,
                Properties.Resources.attack05_left,
                Properties.Resources.attack06_left,
                Properties.Resources.attack07_left,
                Properties.Resources.attack08_left,
                Properties.Resources.attack09_left,
                Properties.Resources.attack10_left
            };

            attackRight = new List<Bitmap>
            {
                Properties.Resources.attack01_right,
                Properties.Resources.attack02_right,
                Properties.Resources.attack03_right,
                Properties.Resources.attack04_right,
                Properties.Resources.attack05_right,
                Properties.Resources.attack06_right,
                Properties.Resources.attack07_right,
                Properties.Resources.attack08_right,
                Properties.Resources.attack09_right,
                Properties.Resources.attack10_right
            };

            // Jump-attack animations (Left & Right)
            jumpAttackLeft = new List<Bitmap>
            {
                Properties.Resources.jump_attack01_left,
                Properties.Resources.jump_attack02_left,
                Properties.Resources.jump_attack03_left,
                Properties.Resources.jump_attack04_left,
                Properties.Resources.jump_attack05_left,
                Properties.Resources.jump_attack06_left,
                Properties.Resources.jump_attack07_left,
                Properties.Resources.jump_attack08_left,
                Properties.Resources.jump_attack09_left,
                Properties.Resources.jump_attack10_left
            };

            jumpAttackRight = new List<Bitmap>
            {
                Properties.Resources.jump_attack01_right,
                Properties.Resources.jump_attack02_right,
                Properties.Resources.jump_attack03_right,
                Properties.Resources.jump_attack04_right,
                Properties.Resources.jump_attack05_right,
                Properties.Resources.jump_attack06_right,
                Properties.Resources.jump_attack07_right,
                Properties.Resources.jump_attack08_right,
                Properties.Resources.jump_attack09_right,
                Properties.Resources.jump_attack10_right
            };
        }
    }
}
