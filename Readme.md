### 2D Platformer Prototype using WinForms and GDI+

This is the beginning of a lightweight 2D platformer developed in C#, using the built-in GDI+ graphics API within Winforms.

The goal was to animate sprites and handle basic platforming mechanics (movement, collision, dynamic objects). 

![GitHub release downloads](https://img.shields.io/github/downloads/laurentbarraud/AnimSprites/total?color=88aacc&style=flat)

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

### Features
- Smooth sprite animations for walking, jumping, and attacking
- Precise collision detection with platforms and screen boundaries
- - In-game platform builder: create, move, and delete platforms or bushes at runtime
- Interactive objects (bushes can be placed, hit, and removed)
- Toggleable rain effect via the build menu
- Modular architecture for easy extension and maintenance

### How to Run
1. Clone the repository
2. Open the solution in Visual Studio 2022
3. Build and run the project

### Download
Go to the [Releases](../../releases) section to download a ZIP archive containing the compiled executable (for Windows 7 and above, x64-based).

### Unit Tests
- Sprite movement and animation consistency
- Platform collision from above and below
- Screen boundary enforcement
- Runtime object management (platforms and interactive elements)
- Level serialization and deserialization integrity

### References:
- (in French): https://sources-codes.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 
- Three basic steps for using DrawImage:
https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

Only royalty-free resources were used in this project.
