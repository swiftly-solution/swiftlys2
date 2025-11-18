<div align="center">
  <img src="https://pan.samyyc.dev/s/VYmMXE" />
  <h2><strong>PluginName</strong></h2>
  <h3>PluginDescription</h3>
</div>

<p align="center">
  <img src="https://img.shields.io/badge/build-passing-brightgreen" alt="Build Status">
  <img src="https://img.shields.io/github/downloads/PluginAuthor/PluginId/total" alt="Downloads">
  <img src="https://img.shields.io/github/stars/PluginAuthor/PluginId?style=flat&logo=github" alt="Stars">
  <img src="https://img.shields.io/github/license/PluginAuthor/PluginId" alt="License">
</p>

## Getting Started (delete me)

1. **Edit `PluginMetadata` Attribute**  
   - Set your plugin's `Id`, `Name`, `Version`, `Author` and `Description`.
2. **Edit `PluginId.csproj`**  
   - Set the `<AssemblyName>` property to match your plugin's main class name.
   - Add any additional dependencies as needed.
3. **Implement your plugin logic** in C#.
   - Place your main plugin class in the root of the project.
   - Use the SwiftlyS2 managed API to interact with the game and core.
4. **Add resources**  
   - Place any required files in the `gamedata`, `templates`, or `translations` folders as needed.

## Building

- Open the project in your preferred .NET IDE (e.g., Visual Studio, Rider, VS Code).
- Build the project. The output DLL and resources will be placed in the `build/` directory.
- The publish process will also create a zip file for easy distribution.

## Publishing

- Use the `dotnet publish -c Release` command to build and package your plugin.
- Distribute the generated zip file or the contents of the `build/publish` directory.