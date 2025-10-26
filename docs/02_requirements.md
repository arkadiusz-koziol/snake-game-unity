# Functional Requirements - Classic Snake Game (Unity)

## Required Behaviors

### Core Game Mechanics

#### Movement System
- **Snake Movement**: Snake moves one cell per tick in the direction of the last valid input
- **Direction Control**: Support both arrow keys (↑↓←→) and WASD keys
- **Movement Restrictions**: Snake cannot reverse 180° within the same tick
- **Smooth Movement**: Consistent movement timing without stuttering
- **Unity Integration**: Use Unity's Input System for responsive input handling

#### Apple Mechanics
- **Apple Spawning**: Red circle sprites spawn on randomly selected free cells
- **Apple Consumption**: Snake head consumes apple when moving onto its position
- **Score System**: Score increases by 1 point per apple eaten
- **Growth Mechanism**: Snake length increases by 1 segment on the next tick after eating
- **Apple Replacement**: New apple spawns immediately after consumption
- **Visual Feedback**: Smooth animations for apple consumption and snake growth

#### Collision Detection
- **Wall Collision**: Game ends when snake head hits board boundaries
- **Self-Collision**: Game ends when snake head hits any body segment
- **Collision Response**: Game state switches to GAME_OVER with visible UI message
- **No Wrap-Around**: Snake cannot pass through walls
- **Visual Effects**: Particle effects or screen shake on collision

#### Game State Management
- **Game States**: RUNNING, PAUSED, GAME_OVER
- **Pause/Resume**: P key toggles between RUNNING and PAUSED states
- **Quit Functionality**: Escape key exits game with proper Unity cleanup
- **State Persistence**: Game state maintained consistently across operations
- **UI State**: UI elements reflect current game state (pause overlay, game over screen)

### Input Handling
- **Unity Input System**: Use Unity's modern Input System for responsive input
- **Input Debouncing**: Prevents multiple direction changes within single tick
- **Key Mapping**: Consistent key bindings across all game states
- **Mouse Support**: Optional mouse controls for menu navigation
- **Controller Support**: Optional gamepad support for enhanced gameplay

### Rendering System
- **Unity Rendering**: Use Unity's built-in rendering pipeline
- **Sprite Rendering**: Efficient sprite-based rendering for game objects
- **UI Canvas**: Modern UI system for game information display
- **Real-time Updates**: Smooth visual updates on every tick
- **Visual Effects**: Particle systems and animations for enhanced gameplay
- **Performance**: Optimized rendering with minimal draw calls

## Optional Features

### Undo Functionality
- **Undo Command**: U key reverts the last move when enabled
- **State Restoration**: Complete restoration of previous game state
- **Configuration**: Can be disabled via configuration
- **Limitations**: Single-step undo only

### Artificial Intelligence
- **AI Toggle**: T key enables/disables AI control
- **Strategy Pattern**: Pluggable AI implementations
- **Greedy Strategy**: Simple pathfinding to nearest apple
- **Performance**: AI must make ≥20 consecutive safe moves
- **Toggle During Gameplay**: Can be enabled/disabled mid-game

### Configuration System
- **Unity ScriptableObjects**: Visual configuration in Unity Editor
- **JSON Configuration**: Runtime configuration via JSON files
- **Environment Variables**: Override configuration via Unity PlayerPrefs
- **Precedence**: env > JSON > ScriptableObject defaults
- **Validation**: Configuration validation with error handling
- **Default Settings**: Works out of the box with sensible defaults

### Difficulty Levels
- **Easy**: 200ms/tick (5 FPS)
- **Normal**: 125ms/tick (8 FPS) (default)
- **Hard**: 80ms/tick (12.5 FPS)
- **Runtime Change**: Difficulty can be changed via configuration
- **Unity Integration**: Use Unity's FixedUpdate for consistent timing

## Technical Requirements

### Architecture Requirements
- **Separation of Concerns**: Game logic isolated from Unity components
- **Design Patterns**: Mandatory use of State, Command, Observer, Strategy patterns
- **Modularity**: Clean module boundaries and interfaces
- **Extensibility**: Easy addition of new features
- **Unity Integration**: Proper use of Unity's component system and lifecycle

### Performance Requirements
- **Tick Rate Consistency**: ±10ms tolerance for all difficulty levels
- **Memory Stability**: No memory leaks during extended play
- **Input Responsiveness**: <16ms input latency for smooth gameplay
- **Rendering Performance**: Maintain 60 FPS with smooth visual updates
- **Unity Optimization**: Efficient use of Unity's rendering pipeline

### Testing Requirements
- **Deterministic Behavior**: Configurable RNG seed for reproducible tests
- **Test Coverage**: ≥90% coverage for core modules
- **Unity Test Framework**: Use Unity Test Framework for comprehensive testing
- **Edit Mode Tests**: Pure C# game logic testing without Unity dependencies
- **Play Mode Tests**: Unity component integration testing
- **Integration Tests**: End-to-end gameplay testing

### Platform Requirements
- **Primary Platform**: Desktop (Windows, macOS, Linux)
- **Unity Version**: Unity 2022.3 LTS or newer
- **C# Version**: C# 9.0+ features supported
- **Dependencies**: Minimal external dependencies beyond Unity
- **Build Targets**: Standalone builds for all supported platforms

## Configuration Options

### Board Configuration
- **Dimensions**: Configurable width and height (default: 20×15)
- **Boundaries**: Fixed walls at borders
- **Grid System**: Rectangular coordinate system

### Game Configuration
- **Difficulty**: easy|normal|hard
- **Enable Undo**: Boolean flag for undo functionality
- **RNG Seed**: Optional integer for deterministic behavior
- **Tick Rate**: Configurable per difficulty level

### Display Configuration
- **Header Panel**: Toggleable information display
- **Visual Elements**: Configurable symbols for game objects
- **Color Support**: Optional color rendering (future enhancement)

## Event System

### Required Events
- **APPLE_SPAWNED**: Published when new apple is placed
- **APPLE_EATEN**: Published when snake consumes apple
- **COLLISION**: Published when collision is detected
- **GAME_OVER**: Published when game ends
- **TICK**: Published on every game tick
- **SCORE_CHANGED**: Published when score updates

### Event Payloads
- **Position Data**: Coordinates for relevant events
- **Score Information**: Current score value
- **Tick Information**: Current tick number
- **Game State**: Current game state information

## Quality Attributes

### Reliability
- **Error Handling**: Graceful handling of all error conditions
- **Recovery**: Proper cleanup after exceptions
- **Stability**: No crashes during normal operation

### Usability
- **Intuitive Controls**: Easy-to-learn key bindings
- **Visual Clarity**: Clear game state representation
- **Responsive Feedback**: Immediate response to user actions

### Maintainability
- **Code Quality**: Clean, well-documented code
- **Testability**: Easy to test individual components
- **Extensibility**: Simple addition of new features

### Performance
- **Efficiency**: Minimal resource usage
- **Scalability**: Consistent performance across different systems
- **Responsiveness**: Real-time gameplay experience
