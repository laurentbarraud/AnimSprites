### School-based programming research project
Problem: animation of sprites in a window illustrated with a platform game

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

### Cumulative Features roadmap
v0.1 includes:
- [x] Displays multiple parts of a spreadsheet to shape a platform
- [x] The sprite moves in the direction of the arrow pressed on the keyboard with a walking animation.

v0.2 added features :
- [x] Implemented a SolidPictureBox, which inherits all PictureBox properties and is considered solid in code by type-checking.
- [x] Set gravity so that the sprite falls from the platform.
- [x] Set edge limits to prevent the sprite from leaving the screen on border left and right using a 1px width wall.
- [x] Implemented a PlayerPictureBox which also inherits all PictureBox properties and adds a Status which can take "isJumping" , "isGrounded" or "isFalling" enum values. 
- [x] Implemented a jump animation when the user presses space bar.

v0.3 added features:
- [x] Implemented an attack animation when the player presses Ctrl key.
- [x] Implemented a jump and attack animation when the player presses the space bar and then Ctrl key.
- [x] Implemented a scrolling to the right when sprite reaches 4/5 of screen width and to the left if it reaches 1/5.
      
v0.3.1 added features:
- [x] The player can freely explore the level by moving the camera left or right with A and D keys.
- [x] Implemented a hidden build menu (user can press B key to activate/desactivate it).
- [x] With the build menu opened, the player can add platforms, choose their size in blocks and delete a selected one.
- [x] Implemented a blink effect when the user select a platform by clicking on it, while the build menu is opened.

v0.4 added features:
- [x] Added a button in the build menu to add a bush.
- [x] Added a BreakableSolidPictureBox with a health counter that implements the IBreakable interface.
- [x] Added collision detection when the player attacks with CTRL key.
- [x] Added a sword effect (slash) that is painted on a target when hit
- [x] Added a fade-to-transparent effect called in methods of the VisualEffects class, for when the object is destroyed.
- [x] Added the persistence of platforms and bushes, as a string stored in app.settings.

### Unit tests:

- [x] Controled that sprites scroll correct and that all keys make the correct move
- [x] Tested if the sprite can't go through the platform, by walking over it and jumping below it
- [x] Tested if the sprite cannot exit the displayed window by the left or by the right
- [x] Animation fluidity test by jumping and attacking while in air
- [x] Activated the build menu, then created platforms of 1, 2 and 3 blocks, moved them on the form, jumped on them, then deleted all platforms, including the initial one.
- [x] Tested if a bush can be added, moved and deleted.
- [x] Tested if a bush can be hit by the knight and fade to disappear. 

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
