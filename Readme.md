## AnimSprites 
This is the beginning of a lightweight 2D platformer developed in C#, using the built-in GDI+ graphics API within Winforms.

[![Release](https://img.shields.io/badge/release-stable-245e48?style=flat)](https://github.com/laurentbarraud/AnimSprites/releases)
[![GitHub release downloads](https://img.shields.io/github/downloads/laurentbarraud/AnimSprites/v0.5/total?color=88aacc&style=flat)](https://github.com/laurentbarraud/AnimSprites/releases/)

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="500" alt="screenshot of the platformer" >
</p>

The goal was to implement a robust sprite animation system, integrated with platforming mechanics such as movement, collision, and dynamic object handling.

### Features
- 🎮 Smooth sprite animations for walking, jumping, and attacking  
- 🎯 Precise collision detection with platforms and screen boundaries  
- 🛠️ In-game platform builder: create, move, and delete platforms or bushes at runtime  
- 🌿 Interactive objects (bushes can be placed, hit, and removed)  
- 🌧️ Toggleable rain effect via the build menu  
- 🧩 Modular architecture for easy extension and maintenance  

### Development setup
1. Clone the repository with Git
2. Open the '.sln' file in Visual Studio 2022
3. Build the solution with Ctrl+Shift+B
4. Run with debugging

### Download
Go to the [Releases](https://github.com/laurentbarraud/AnimSprites/releases) section to download a ZIP archive containing the compiled executable (for Windows 7 and above, x64-based).

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

If you find this project useful or inspiring, a star would be greatly appreciated !
