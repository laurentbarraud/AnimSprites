### School-based programming research project
Problem: animation of sprites in a window illustrated with a C# platform game.

This project uses the DrawImage method from GDI+, a library already integrated in all Winforms projects.
This avoids having to use an external framework, such as XNA or DirectX. 

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

### Unit tests:

- [x] Controled that the sprite animation is fluid and that all keys make the correct move. 
- [x] Tested if the sprite can't go through the platform, by walking over it and jumping below it
- [x] Tested if the sprite cannot exit the displayed window by the left or by the right. 
- [x] Animation fluidity test by jumping and attacking while in air
- [x] Activated the build menu, then created platforms of 1, 2 and 3 blocks, moved them on the form, jumped on them, then deleted all platforms, including the initial one.
- [x] Tested if a bush can be added, moved and deleted.
- [x] Tested if a bush can be hit by the sprite and fade to disappear. 

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

In this project only royalty-free resources have been used.
