### School-based programming research project
Problem: animation of sprites in a window through a platform game

- [x] Display part of multiple images in picturebox.

- [ ] Move the sprite by 10 pixels in the direction of the arrow pressed on the keyboard.

- [ ] Make the walking animation

- [ ] Implement a gravity so that the sprite falls from the platform

- [ ] Implement the jump animation when the user presses space bar

- [ ] Set edge limits to prevent the sprite from leaving the screen.

- [ ] Implement attack animation

- [ ] Implement the enemy and its animations using a loop and a random-

- [ ] Implement the push effect when hitting the enemy and when being hit

- [ ] Implement the end of the game when one of the two players fell off the platform

- [ ] Test the smooth operation and fluidity of animations. 


### Unit tests:

- [ ] Control that sprites scroll in the correct order

- [ ] Control that all keys make the correct move

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

The DrawImage method is derived from GDI+, which is a library already integrated in all projects.
This avoids having to use an external framework, such as XNA or DirectX. 

### The three basic steps, discussed:
source: https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

### Royalty-free resources:

- [x] «tileset», for platforming: 
https://www.gameart2d.com/free-platformer-game-tileset.html 

- [x] Knight “sprite”:
https://www.gameart2d.com/the-knight-free-sprites.html
(«spritesheets» made by myself). 
