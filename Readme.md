### 2D Platformer Prototype using WinForms and GDI+

This is a lightweight 2D platformer engine developed in C#, using the built-in GDI+ graphics API within Winforms.
The goal was to animate sprites and handle basic platforming mechanics (movement, collision, dynamic objects). 

![Tested](https://img.shields.io/badge/tested-no%20bugs-brightgreen)

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

### Features
- Smooth sprite animations for movement, jumping, and attacking
- Collision detection with platforms and screen boundaries
- Object interaction (e.g. bushes can be added, hit and disappear)
- Platform builder mode: create, move, and delete platforms during runtime
  
### How to Run
1. Clone the repository
2. Open in Visual Studio 2022
3. Build and run the solution

Or go to the Release section to download a zip archive, containing the compiled executable (for x64 systems, starting from Windows 7 and upwards) 

### Unit Tests
- Sprite movement and animation fluidity
- Collision with platforms from above and below
- Screen boundary checks
- Runtime object management (platforms and bushes)

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

Only royalty-free resources were used in this project.
