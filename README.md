# Snake Game - Unity Implementation

A professional Unity-based implementation of the classic Snake game, demonstrating clean software architecture principles, Domain-Driven Design (DDD), and Object-Oriented Programming (OOP) patterns.

## Features

### Core Gameplay
- **Classic Snake Mechanics**: Guide a snake to consume apples while avoiding collisions
- **Smooth Movement**: Responsive controls with arrow keys or WASD
- **Growth System**: Snake length increases after eating apples
- **Collision Detection**: Wall and self-collision detection with game over
- **Scoring System**: Points awarded for each apple consumed

### Advanced Features
- **AI Mode**: Toggle AI control with intelligent pathfinding
- **Undo Functionality**: Revert moves when enabled in configuration
- **Pause/Resume**: Pause game with P key
- **Multiple Difficulty Levels**: Easy, Normal, and Hard modes
- **Deterministic Behavior**: Configurable RNG seed for reproducible gameplay

### Technical Features
- **Clean Architecture**: Separation between game logic and Unity components
- **Design Patterns**: State, Command, Observer, and Strategy patterns
- **Event System**: Decoupled communication using Observer pattern
- **Configuration System**: Unity ScriptableObjects with JSON support
- **Comprehensive Testing**: Unity Test Framework with Edit Mode and Play Mode tests

## Requirements

- **Unity**: 2022.3 LTS or newer
- **Platform**: Windows, macOS, Linux
- **C#**: C# 9.0+ features supported

## Installation

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd snake-game-unity
   ```

2. **Open in Unity**:
   - Launch Unity Hub
   - Click "Add project from disk"
   - Select the project folder
   - Open the project

3. **Configure the project**:
   - The project should automatically detect Unity version
   - All required packages are included

## Quick Start

1. **Open the main scene**:
   - Navigate to `Assets/Scenes/MainScene.unity`
   - Double-click to open

2. **Run the game**:
   - Press the Play button in Unity Editor
   - Or build and run the executable

3. **Controls**:
   - **Movement**: Arrow keys or WASD
   - **Pause/Resume**: P key
   - **Restart**: R key
   - **Toggle AI**: T key
   - **Difficulty**: 1 (Easy), 2 (Normal), 3 (Hard)
   - **Quit**: Escape key

## Project Structure

```
Assets/
├── Scripts/
│   ├── GameLogic/           # Pure C# game logic
│   │   ├── GameSnake.cs     # Main game orchestrator
│   │   ├── Board.cs         # Grid management
│   │   ├── Snake.cs         # Snake entity
│   │   ├── GameState.cs     # Immutable state
│   │   └── Commands/        # Command pattern
│   ├── UnityComponents/     # Unity-specific components
│   │   ├── GameManager.cs   # Main Unity manager
│   │   ├── InputHandler.cs  # Input processing
│   │   ├── SnakeRenderer.cs # Rendering system
│   │   └── UIManager.cs     # UI management
│   ├── Events/              # Event system
│   │   ├── EventBus.cs      # Observer pattern
│   │   └── GameEvents.cs    # Event definitions
│   ├── AI/                  # AI strategies
│   │   ├── IAIStrategy.cs   # AI interface
│   │   └── GreedyAI.cs      # Greedy implementation
│   └── Configuration/       # Configuration system
│       ├── GameConfig.cs    # Configuration data
│       └── GameConfigSO.cs  # ScriptableObject
├── Prefabs/                 # Game object prefabs
├── Scenes/                  # Unity scenes
├── Sprites/                 # Game sprites
├── Audio/                   # Sound effects
└── Tests/                   # Test assemblies
    ├── EditMode/            # Pure C# tests
    └── PlayMode/            # Unity integration tests
```

## Architecture

### Design Patterns

1. **State Pattern**: `GameState` manages game state transitions
2. **Command Pattern**: `MoveCommand` enables undo functionality
3. **Observer Pattern**: `EventBus` provides decoupled communication
4. **Strategy Pattern**: `IAIStrategy` allows pluggable AI implementations

### Separation of Concerns

- **Game Logic**: Pure C# classes independent of Unity
- **Unity Components**: MonoBehaviour classes handling Unity-specific functionality
- **Event System**: Decoupled communication between components
- **Configuration**: Centralized settings management

## Configuration

### Unity ScriptableObjects
- Visual configuration in Unity Editor
- Runtime configuration via JSON files
- Environment variable overrides through PlayerPrefs

### Configuration Options
- Board dimensions (width × height)
- Difficulty levels (Easy/Normal/Hard)
- AI settings (enabled/disabled)
- Input settings (debouncing, timing)
- Rendering settings (cell size, colors)
- Audio settings (volume, effects)

## Testing

### Test Framework
- **Unity Test Framework**: Native Unity testing
- **NUnit**: Test assertions and structure
- **Edit Mode Tests**: Pure C# game logic testing
- **Play Mode Tests**: Unity component integration testing

### Running Tests
1. **In Unity Editor**:
   - Window → General → Test Runner
   - Select Edit Mode or Play Mode
   - Click "Run All"

2. **Command Line**:
   ```bash
   unity -batchmode -quit -projectPath . -runTests
   ```

### Test Coverage
- **Target**: ≥90% coverage for core game logic
- **Focus**: Game mechanics, AI algorithms, state management
- **Types**: Unit tests, integration tests, performance tests

## Building

### Desktop Builds
1. **File → Build Settings**
2. **Select Platform**: Windows/Mac/Linux
3. **Configure Settings**:
   - Resolution: 800×600 (minimum)
   - Graphics API: DirectX 11/12 (Windows)
   - Scripting Backend: Mono or IL2CPP
4. **Build**

### Build Requirements
- **Windows**: DirectX 11 compatible graphics card
- **macOS**: Metal compatible graphics card
- **Linux**: OpenGL 3.2 compatible graphics card

## Performance

### Requirements
- **Tick Rate**: 125ms/tick (±10ms tolerance) for Normal difficulty
- **Frame Rate**: 60 FPS rendering
- **Memory**: Stable usage without leaks
- **Input Latency**: <16ms for smooth gameplay

### Optimization
- **Rendering**: Efficient sprite batching
- **Memory**: Object pooling for frequently created objects
- **CPU**: Optimized game logic and AI algorithms
- **GPU**: Minimal draw calls and efficient rendering

## Development

### Code Standards
- **C# Coding Conventions**: Microsoft C# style guide
- **Unity Best Practices**: Unity coding standards
- **Documentation**: XML documentation for public APIs
- **Error Handling**: Comprehensive exception handling

### Extensibility
- **AI Strategies**: Easy addition of new AI implementations
- **Rendering Backends**: Pluggable rendering systems
- **Input Methods**: Support for different input devices
- **Platform Support**: Cross-platform compatibility

## Troubleshooting

### Common Issues

1. **Build Errors**:
   - Ensure Unity version compatibility
   - Check platform-specific settings
   - Verify all dependencies are included

2. **Performance Issues**:
   - Check graphics settings
   - Monitor frame rate in Unity Profiler
   - Optimize rendering settings

3. **Input Issues**:
   - Verify input system configuration
   - Check for input conflicts
   - Test on different platforms

### Debug Mode
- Enable debug logging in GameManager
- Use Unity Console for error messages
- Check Event Bus for event flow issues

## Contributing

### Development Setup
1. Fork the repository
2. Create a feature branch
3. Make changes following coding standards
4. Add tests for new functionality
5. Submit a pull request

### Code Review Process
- All code must pass tests
- Follow established patterns and conventions
- Include documentation for new features
- Ensure cross-platform compatibility

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Unity Technologies for the Unity game engine
- The game development community for best practices
- Contributors and testers for feedback and improvements

---

**Version**: 1.0.0  
**Last Updated**: 2024  
**Unity Version**: 2022.3 LTS+
