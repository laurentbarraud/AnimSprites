### 2D Platformer Prototype using WinForms and GDI+

This is a lightweight 2D platformer engine developed in C#, using the built-in GDI+ graphics API within Winforms.

The goal was to animate sprites and handle basic platforming mechanics (movement, collision, dynamic objects). 

![Tested](https://img.shields.io/badge/tested-no%20bugs-1b4636)
![GitHub all releases](https://img.shields.io/github/downloads/laurentbarraud/AnimSprites/total?color=88aacc&style=flat)

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="300" alt="screenshot of the main form" >
</p>

### Features
- Smooth sprite animations for walking, jumping, and attacking
- Precise collision detection with platforms and screen boundaries
- Interactive objects (e.g. bushes can be placed, hit, and removed)
- In-game platform builder: create, move, and delete platforms at runtime
- Toggleable rain effect via the build menu
- Modular architecture for easy extension and maintenance

### How to Run
1. Clone the repository
2. Open the solution in Visual Studio 2022
3. Build and run the project

Alternatively, visit the [Releases](../../releases) section to download a ZIP archive containing the compiled executable (compatible with x64 systems running Windows 7 or later).

### Unit Tests
- Sprite movement and animation consistency
- Platform collision from above and below
- Screen boundary enforcement
- Runtime object management (platforms and interactive elements)
- Level serialization and deserialization integrity

### References:

### Sources
- (in French): https://sources-codes.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 
- Three basic steps for using DrawImage:
https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

Only royalty-free resources were used in this project.
