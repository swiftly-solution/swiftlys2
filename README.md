# SwiftlyS2

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
[![Build Status](https://img.shields.io/github/actions/workflow/status/swiftly-solution/swiftlys2/build.yml?branch=master)](https://github.com/swiftly-solution/swiftlys2/actions)
[![Discord](https://img.shields.io/discord/1178027657594687608?color=7289da&logo=discord&logoColor=white)](https://swiftlys2.net/discord)
[![NuGet](https://img.shields.io/nuget/v/SwiftlyS2.CS2.svg)](https://www.nuget.org/packages/SwiftlyS2.CS2/)

SwiftlyS2 is a powerful scripting framework for Source 2 games, built in C++ with C# plugin support. It provides developers with a comprehensive API to create plugins for Source 2-based games like Counter-Strike 2.

# Why should you choose SwifltyS2?

SwifltyS2 is built with developers in mind, providing an extensive suite of scripting features that make plugin development faster and more efficient.

We're preventing memory leaks when working with the game's SDK and functions, having active maintenance and faster execution speeds through natives which are calling directly Native (core) code.

A list of features:
- **Commands**: Handles custom console commands or chat-based commands in the game.
- **Convars**: Manages console variables (cvars) to control game behavior and configuration.
- **Database**: Provides access to a centralized space to store your database credentials.
- **Entity System**: Handles creation, management, and interaction of game entities.
- **Events**: Manages event hooks, allowing scripts to react to in-game occurrences.
- **GameEvents**: Handles triggering and listening for in-game events. All fields are typed.
- **Memory**: Provides low-level memory manipulation tools for advanced scripting.
- **Menus**: Provides a easy Menu API with tons of customization, from colors to options and much more.
- **Hooks**: A hooking system to hook functions, net messages, entity outputs and more.
- **NetMessages**: Facilitates sending and receiving network messages via protobuf to clients.
- **Profiler**: Tools for performance analysis and debugging of scripts.
- **ProtobufDefinitions**: Types for NetMessages received or sent by the server.
- **Scheduler**: Provides timers and scheduling functionality for deferred or repeated tasks.
- **SchemaDefinitions**: Defines the SDK Schema classes and enums.
- **Schemas**: Schema class helpers for working with Game's SDK.
- **Sounds**: Provides tools for playing and managing audio within the game.

## Supported Games

- **Counter-Strike 2**

## Quick Start

### Prerequisites

- **Windows**: Visual Studio 2022 with C++ and .NET workloads
- **Linux**: GCC 11+ and .NET 10.0 SDK
- **XMake**

### Building SwiftlyS2

1. **Clone the repository**
   ```bash
   git clone --recurse-submodules https://github.com/swiftly-solution/swiftlys2.git
   cd swiftlys2
   ```

2. **Build the framework**
   ```bash
   # Windows
   xmake -j 4
   
   # Linux
   xmake -j $(nproc)
   ```

3. **Install to game directory**
   ```bash
   # Copy built files to your CS2 server
   cp -r build/package/* /path/to/cs2/game/csgo/
   ```

4. **Start your server**

### Your First Plugin

Create your first guide using our comprehensive guide:
https://swiftlys2.net/docs/development/getting-started/

## Architecture

```
SwiftlyS2/
├── src/                         # C++ core framework
│   ├── api/                     # C++ API interfaces
│   ├── core/                    # Core framework logic
│   ├── engine/                  # Source 2 engine integration
│   ├── memory/                  # Memory management
│   ├── monitor/                 # Loggers
│   ├── network/                 # Network handling
│   ├── scripting/               # Native Exports
│   ├── sdk/                     # SDK Dumper
│   └── server/                  # Server management
├── managed/                     # C# managed layer
│   └── src/                     # C# managed source code
│       ├── SwiftlyS2.Core/      # Core C# APIs
│       ├── SwiftlyS2.Generated/ # Generated bindings
│       └── SwiftlyS2.Shared/    # Shared utilities
├── natives/                     # Native definitions
├── generator/                   # Code generation tools
└── vendor/                      # Third-party dependencies
```

## License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details.

## Community

- **Discord**: [Join our community](https://swiftlys2.net/discord)
- **Issues**: [Report bugs and request features](https://github.com/swiftly-solution/swiftlys2/issues)

## Acknowledgments

All of the acknowledgments can be seen in [ACKNOWLEDGEMENTS.md](ACKNOWLEDGEMENTS.md)

---

<div align="center">
  <strong>Made with ❤️ by the Swiftly Development team</strong>
</div>
