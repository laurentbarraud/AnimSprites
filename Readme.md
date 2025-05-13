### School-based programming research project
Problem: animation of sprites in a window through a platform game

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

- [x] Display multiple parts of a spreadsheet in a picturebox.

- [x] Implement a SolidPictureBox, which inherits all PictureBox properties

- [x] Move the sprite in the direction of the arrow pressed on the keyboard with a walking animation.

- [x] Implement a gravity so that the sprite falls from the platform.
      
- [x] Implement a solid ground to avoid the sprite falling through the bottom of the window
      
- [x] Set edge limits to prevent the sprite from leaving the screen using a 1px width wall.
      
- [x] Implement a PlayerPictureBox which also inherits all PictureBox properties and adds a Status which can take the enum isJumping, isGrounded or isFalling. 

- [x] Implement the jump animation when the user presses space bar.

- [x] Implement the attack animation when the user presses Ctrl key.

- [x] Implement the jump and attack animation when the user presses the space bar and then Ctrl key.

- [x] Test the smooth operation and fluidity of animations.

- [x] Implement scrolling to the right when sprite reaches 4/5 of screen width and to the left if it reaches 1/5.

- [x] The player can freely explore the level by moving the camera left or right with the A and D keys.

- [x] Implement a hidden build menu (user can press B key to activate/desactivate it).

- [x] With the open menu, the player can add platforms, choose their size in blocks and delete a selected one.
       
- [ ] Implement objects or enemies to hit

- [ ] Implement basic collision detection when the user hits an object or enemy


### Unit tests:

- [x] Control that sprites scroll correct and that all keys make the correct move

- [x] Test if the sprite can't go through the platform, by walking over it and jumping below it

- [x] Test if the sprite cannot exit the displayed window by the left or by the right

- [x] Animation fluidity test

### References:

Source (in French) : https://sources-codes.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 

"You have to draw all the stages of your character’s animation but instead of having several images we load a single one in memory that contains all its steps in a precise order. 
(What is then called a «sprite-sheet»).

The color magenta or lime or any other single color (no jpeg compression for sprites), which is not used by sprites will be the background color, which allows to not display it at the time of rendering.

In a real game everything happens in a main loop, we test the user inputs ( keyboard, joystick .. ) but we can also code it with events or with a timer. 

At the time of rendering, it is enough to display the sprite with the DrawImage method which takes as parameter a source rectangle that corresponds to the location of the sprite on the image and a destination rectangle that corresponds to the place on the screen where we will draw the sprite. 
By playing with its 2 rectangles, you can vary the movements of the character and his position.”

### Three basic steps for using DrawImage:
https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

The DrawImage method is derived from GDI+, which is a library already integrated in all projects.
This avoids having to use an external framework, such as XNA or DirectX. 

In this project only royalty-free resources have been used.
