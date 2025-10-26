# Architecture Decision Record (ADR) 0001: Core Architecture Decisions

## Status
Accepted

## Context
We need to implement a Classic Snake Game in C# with Unity, following Domain-Driven Design (DDD) and Object-Oriented Programming (OOP) principles. The game must be playable as a desktop application with real-time input handling and smooth rendering.

## Decision

### Language Choice: C# with Unity
**Decision**: Use C# with Unity as the implementation platform.

**Rationale**:
- C# provides excellent OOP support with strong typing and modern language features
- Unity offers robust game development framework with built-in rendering, input, and physics
- Excellent testing ecosystem (NUnit, Unity Test Framework)
- Cross-platform deployment (Windows, macOS, Linux)
- Rich ecosystem of tools and assets
- Professional game development standard
- Clean syntax that excellently supports OOP patterns and SOLID principles

**Consequences**:
- Requires Unity 2022.3 LTS or newer
- Benefits from Unity's component system and MonoBehaviour lifecycle
- Easy integration with Unity's CI/CD pipelines
- Professional game development workflow

### Rendering System: Unity's Built-in Rendering
**Decision**: Use Unity's built-in rendering system with UI Canvas and Sprite Renderers.

**Rationale**:
- Native Unity rendering with excellent performance
- Built-in UI system for game information display
- Sprite-based rendering for game objects
- Automatic optimization and batching
- Cross-platform rendering consistency
- Professional game development standard

**Consequences**:
- Requires Unity Editor for asset management
- Benefits from Unity's rendering optimizations
- Easy to create visual effects and animations

### Separation of Concerns: Game Logic vs Unity Components
**Decision**: Implement strict separation between game logic and Unity-specific components.

**Rationale**:
- Enables testing of game logic without Unity dependencies
- Allows for future platform changes or headless testing
- Follows Single Responsibility Principle
- Improves maintainability and code organization
- Enables pure C# unit testing

**Architecture**:
```
┌─────────────────┐    ┌─────────────────┐
│   Game Logic    │    │ Unity Components│
│                 │    │                 │
│ • GameSnake     │◄──►│ • SnakeRenderer │
│ • Board         │    │ • InputHandler  │
│ • Snake         │    │ • UIManager     │
│ • GameState     │    │ • AudioManager  │
└─────────────────┘    └─────────────────┘
```

**Consequences**:
- More complex initial setup
- Clear boundaries between layers
- Easier testing and debugging
- Better separation of concerns

### Event Bus: Observer Pattern Implementation
**Decision**: Implement a centralized event bus using the Observer pattern.

**Rationale**:
- Decouples game components from each other
- Enables extensible event handling
- Supports AI integration and debugging features
- Follows Open/Closed Principle

**Implementation**:
- Typed events with payload data
- Publisher-subscriber model
- Event categories: game events, input events, system events

**Consequences**:
- Additional complexity in event management
- Better extensibility for future features
- Easier integration testing

### Configuration Mechanism: Unity ScriptableObjects + JSON
**Decision**: Use Unity ScriptableObjects with JSON serialization for configuration.

**Rationale**:
- Unity-native configuration system
- Visual configuration in Unity Editor
- JSON serialization for runtime configuration
- Environment variable support through Unity's PlayerPrefs
- Hierarchical configuration structure
- Easy validation and type checking

**Precedence Order**:
1. Environment variables (highest priority)
2. JSON configuration file
3. ScriptableObject defaults (lowest priority)

**Consequences**:
- Requires Unity Editor for initial setup
- Flexible configuration management
- Clear configuration hierarchy
- Professional game development approach

### Design Patterns: Mandatory Implementation
**Decision**: Mandatory use of specific design patterns for core functionality.

**Patterns Required**:
- **State Pattern**: Game state management (RUNNING, PAUSED, GAME_OVER)
- **Command Pattern**: Snake movement commands with undo support
- **Observer Pattern**: Event bus for decoupled communication
- **Strategy Pattern**: Pluggable AI implementations

**Rationale**:
- Demonstrates understanding of OOP principles
- Provides clean, maintainable code structure
- Enables extensibility and testability
- Follows SOLID principles

**Consequences**:
- More complex initial implementation
- Better code organization and maintainability
- Easier to extend and modify

### Testing Strategy: Unity Test Framework + NUnit
**Decision**: Use Unity Test Framework with NUnit for comprehensive testing.

**Rationale**:
- Unity-native testing framework
- Support for both Edit Mode and Play Mode tests
- Integration with Unity's CI/CD pipeline
- Excellent mocking capabilities
- Professional game development standard

**Implementation**:
- Edit Mode tests for pure C# game logic
- Play Mode tests for Unity component integration
- Mock frameworks for Unity dependencies
- Automated testing in CI/CD pipeline

**Consequences**:
- Requires Unity Editor for test execution
- Professional testing workflow
- Easy integration with Unity's build pipeline

### Performance Requirements: Unity's FixedUpdate + Coroutines
**Decision**: Use Unity's FixedUpdate for game logic and Coroutines for timing.

**Specifications**:
- Easy: 200ms/tick (5 FPS)
- Normal: 125ms/tick (8 FPS) 
- Hard: 80ms/tick (12.5 FPS)
- Tolerance: ±10ms per tick

**Rationale**:
- Unity's FixedUpdate provides consistent timing
- Coroutines enable precise tick rate control
- Frame-rate independent game logic
- Professional game development approach

**Consequences**:
- Requires understanding of Unity's update cycle
- Consistent gameplay across different frame rates
- Professional timing implementation

## Alternatives Considered

### Alternative 1: Web-based Implementation (HTML5/JavaScript)
**Rejected**: While web-based implementation would be more accessible, it doesn't provide the same level of OOP pattern demonstration and professional game development experience as Unity.

### Alternative 2: Terminal-based Implementation (Python curses)
**Rejected**: Terminal-based implementation limits the visual appeal and doesn't provide modern game development experience. Unity offers better learning opportunities for professional game development.

### Alternative 3: Simple Configuration (JSON only)
**Rejected**: Unity ScriptableObjects provide better editor integration and visual configuration management than plain JSON files.

### Alternative 4: Direct Unity Integration
**Rejected**: Violates separation of concerns principle. Game logic should be independent of Unity-specific components for better testability.

## Implementation Notes

### Unity Project Structure
```
Assets/
├── Scripts/
│   ├── GameLogic/           # Pure C# game logic
│   │   ├── GameSnake.cs
│   │   ├── Board.cs
│   │   ├── Snake.cs
│   │   ├── GameState.cs
│   │   └── Commands/
│   ├── UnityComponents/     # Unity-specific components
│   │   ├── SnakeRenderer.cs
│   │   ├── InputHandler.cs
│   │   ├── UIManager.cs
│   │   └── AudioManager.cs
│   ├── Events/              # Event system
│   │   ├── EventBus.cs
│   │   └── GameEvents.cs
│   ├── AI/                  # AI strategies
│   │   ├── IAIStrategy.cs
│   │   └── GreedyAI.cs
│   └── Configuration/       # Configuration system
│       ├── GameConfig.cs
│       └── GameConfigSO.cs
├── Prefabs/                 # Game object prefabs
├── Scenes/                  # Unity scenes
├── Sprites/                 # Game sprites
├── Audio/                   # Sound effects and music
└── Tests/                   # Test assemblies
    ├── EditMode/
    └── PlayMode/
```

### Dependencies
- Unity 2022.3 LTS or newer
- Unity Test Framework
- NUnit (included with Unity)
- Unity Input System (optional, for advanced input)

### Future Considerations
- Multi-snake mode
- Network multiplayer
- Advanced AI strategies
- Mobile platform support
- VR/AR support
- Advanced visual effects

## References
- [Unity Documentation](https://docs.unity3d.com/)
- [Unity Test Framework](https://docs.unity3d.com/Packages/com.unity.test-framework@latest/)
- [Domain-Driven Design principles](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Design Patterns in C#](https://refactoring.guru/design-patterns/csharp)
- [Unity ScriptableObjects](https://docs.unity3d.com/Manual/class-ScriptableObject.html)
