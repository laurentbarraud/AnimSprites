## AnimSprites
A 2D platformer foundation written in C#, using WinForms and the built-in GDI+ graphics API. 

<a href="https://github.com/laurentbarraud/AnimSprites/releases">
  <img src="https://img.shields.io/badge/release-stable-64B07B" alt="Release"></a>
<a href="https://github.com/laurentbarraud/AnimSprites/releases">
  <img src="https://img.shields.io/github/downloads/laurentbarraud/LifeProManager/latest/total?color=88aacc&style=flat" alt="Downloads"></a>

<p align="center">
<img src="https://raw.githubusercontent.com/laurentbarraud/AnimSprites/refs/heads/master/AnimSprites-screenshot.jpg" width="500" alt="screenshot of the platformer" >
</p>

Started as an autonomous school project, this demonstrates how a basic platformer can be built from scratch without any external libraries.

## Features
- 🎮 Smooth sprite animations for walking, jumping and attacking, with refined airborne state management
- 🧩 Modular architecture for easy extension and maintenance
- 🎯 Precise collision detection with platforms and screen boundaries
- 🛠️ In-game platform builder: create, move, and delete platforms or bushes at runtime. 
- 💾 All platforms and bushes you create are saved and restored at their exact positions on next launch, using basic level serialization
- 🌿 Interactive objects: bushes can be struck with a lightning effect
- 🌀 Side‑scrolling effect when reaching the left or right edge of the screen
- 🌧️ Toggleable rain effect via the build menu.

## Unit Tests
- Sprite movement and animation consistency
- Platform collision from above and below
- Screen boundary enforcement
- Runtime object management (platforms and interactive elements)
- Level seralization and deserialization integrity — inserted objects persist across application restarts.
  
## How to run
1. Clone the repository with Git
2. Open the '.sln' file in Visual Studio 2022
3. Build the project with Ctrl+B and run it. 

## Controls
- left and right arrows keys : moving the sprite
- spacebar : jump
- Ctrl key : attack
- A and D keys : move the camera to the left/right
- B key : enable/disable build editor mode with menu.

While the build menu is open :

- left clic : select a solid object, then hold left to drag it anywhere on the form.
- delete key : delete selected object
- escape key or B key : close build menu and disable build editor mode.

## Download
Go to the [Releases](https://github.com/laurentbarraud/AnimSprites/releases) page to download a ZIP archive that includes a compiled x64 executable, for Windows 7 and later (x64-based).

### References:
- Three basic steps for using DrawImage:
https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

Only royalty-free resources were used in this project.
