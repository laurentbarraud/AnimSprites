### School-based programming research project
Problem: animation of sprites in a window through a platform game

- [x] Display multiple parts of a spreadsheet in a picturebox.

- [x] Move the sprite by 5 pixels in the direction of the arrow pressed on the keyboard with a walking animation.

- [x] Implement a gravity so that the sprite falls from the platform

- [x] Set edge limits to prevent the sprite from leaving the screen.

- [x] Implement the jump animation when the user presses space bar

- [ ] Implement the attack animation when the user presses Ctrl key

- [ ] Implement the jump + attack animation when the user presses space bar and then Ctrl key

- [ ] Implement basic collision detection when the user hits an object

- [ ] Test the smooth operation and fluidity of animations. 


### Unit tests:

- [ ] Control that sprites scroll correct and that all keys make the correct move

- [ ] Test if the sprite cannot exit the displayed window

- [ ] Animation fluidity test

### References:

Source (in French) : https://sources-codes.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 

"You have to draw all the stages of your character’s animation but instead of having several images we load a single one in memory that contains all its steps in a precise order. 
(What is then called a «sprite-sheet»).
The color magenta or lime or any other single color (no jpeg compression for sprites), which is not used by sprites will be the background color, which allows to not display it at the time of rendering.

In a real game everything happens in a main loop, we test the user inputs ( keyboard, joystick .. ) but we can also code it with events or with a timer. 

At the time of rendering, it is enough to display the sprite with the DrawImage method which takes as parameter a source rectangle that corresponds to the location of the sprite on the image and a destination rectangle that corresponds to the place on the screen where we will draw the sprite. 
By playing with its 2 rectangles, you can vary the movements of the character and his position.”


### The three basic steps of using DrawImage discussed:
source: https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

The DrawImage method is derived from GDI+, which is a library already integrated in all projects.
This avoids having to use an external framework, such as XNA or DirectX. 

### Royalty-free resources:

- [x] «tileset», for platforming: 
https://www.gameart2d.com/free-platformer-game-tileset.html 

- [x] Knight “sprite”:
https://www.gameart2d.com/the-knight-free-sprites.html
