# UML Class Diagram - Classic Snake Game

## Class Diagram (ASCII)

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                                Snake Game Architecture                          │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GameSnake     │    │     Board       │    │     Snake       │
│                 │    │                 │    │                 │
│ - width: int    │◄──►│ - width: int    │    │ - segments: []  │
│ - height: int   │    │ - height: int   │    │ - direction     │
│ - difficulty    │    │ - apples: []    │    │ - grow_flag     │
│ - score: int    │    │ - rng: Random   │    │                 │
│ - state: State  │    │                 │    │ + move()        │
│                 │    │ + spawn_apple() │    │ + grow()        │
│ + tick()        │    │ + is_free()     │    │ + get_head()    │
│ + handle_input()│    │ + get_cell()    │    │ + get_body()    │
│ + update()      │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GameState     │    │  MoveCommand    │    │   EventBus      │
│                 │    │                 │    │                 │
│ - board: Board  │    │ - direction     │    │ - subscribers   │
│ - snake: Snake  │    │ - game_state    │    │                 │
│ - score: int    │    │                 │    │ + subscribe()   │
│ - tick: int     │    │ + execute()     │    │ + publish()     │
│ - paused: bool  │    │ + unexecute()   │    │ + unsubscribe() │
│                 │    │                 │    │                 │
│ + copy()        │    │                 │    │                 │
│ + is_game_over()│    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  RendererCli    │    │ InputController │    │   StrategyAI    │
│                 │    │                 │    │                 │
│ - screen        │    │ - last_input    │    │ + next_move()   │
│ - buffer        │    │ - debounce      │    │                 │
│                 │    │                 │    │                 │
│ + render()      │    │ + get_input()   │    │                 │
│ + clear()       │    │ + is_valid()    │    │                 │
│ + draw_board()  │    │ + debounce()    │    │                 │
│ + draw_header() │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GreedyAI      │    │   GameEvents    │    │   Direction     │
│                 │    │                 │    │                 │
│ + next_move()   │    │ - APPLE_SPAWNED │    │ - UP            │
│ + find_path()   │    │ - APPLE_EATEN   │    │ - DOWN          │
│ + is_safe()     │    │ - COLLISION     │    │ - LEFT          │
│                 │    │ - GAME_OVER     │    │ - RIGHT         │
│                 │    │ - TICK          │    │                 │
│                 │    │ - SCORE_CHANGED │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

## Class Responsibilities

### Core Game Engine Classes

#### GameSnake
- **Purpose**: Main game orchestrator and game loop controller
- **Responsibilities**:
  - Manage game state transitions
  - Coordinate tick timing and updates
  - Handle input processing
  - Integrate all game components
- **Key Methods**: `tick()`, `handle_input()`, `update()`

#### Board
- **Purpose**: Game board management and apple placement
- **Responsibilities**:
  - Manage grid dimensions and boundaries
  - Handle apple spawning on free cells
  - Provide collision detection for walls
  - Maintain random number generation for apple placement
- **Key Methods**: `spawn_apple()`, `is_free()`, `get_cell()`

#### Snake
- **Purpose**: Snake entity management and movement
- **Responsibilities**:
  - Maintain snake segments and position
  - Handle movement and growth
  - Provide collision detection for self-collision
  - Manage direction changes
- **Key Methods**: `move()`, `grow()`, `get_head()`, `get_body()`

#### GameState
- **Purpose**: Immutable game state snapshot
- **Responsibilities**:
  - Encapsulate complete game state
  - Provide state validation
  - Enable state copying for undo functionality
  - Support AI decision making
- **Key Methods**: `copy()`, `is_game_over()`

### Command Pattern Implementation

#### MoveCommand
- **Purpose**: Encapsulate snake movement as reversible commands
- **Responsibilities**:
  - Execute snake movement
  - Provide undo functionality
  - Maintain command state
- **Key Methods**: `execute()`, `unexecute()`

### Observer Pattern Implementation

#### EventBus
- **Purpose**: Centralized event communication system
- **Responsibilities**:
  - Manage event subscriptions
  - Publish events to subscribers
  - Decouple game components
- **Key Methods**: `subscribe()`, `publish()`, `unsubscribe()`

#### GameEvents
- **Purpose**: Define typed game events
- **Responsibilities**:
  - Define event types and payloads
  - Provide event validation
  - Support event categorization

### Strategy Pattern Implementation

#### StrategyAI
- **Purpose**: Interface for AI implementations
- **Responsibilities**:
  - Define AI strategy interface
  - Enable pluggable AI implementations
- **Key Methods**: `next_move()`

#### GreedyAI
- **Purpose**: Simple greedy AI implementation
- **Responsibilities**:
  - Implement pathfinding to nearest apple
  - Provide safe move validation
  - Execute AI strategy
- **Key Methods**: `next_move()`, `find_path()`, `is_safe()`

### Input/Output Classes

#### RendererCli
- **Purpose**: Terminal rendering and display management
- **Responsibilities**:
  - Render game board and snake
  - Display header information
  - Manage terminal state
  - Provide double-buffering
- **Key Methods**: `render()`, `draw_board()`, `draw_header()`

#### InputController
- **Purpose**: Input handling and processing
- **Responsibilities**:
  - Capture keyboard input
  - Implement input debouncing
  - Validate input commands
  - Handle non-blocking input
- **Key Methods**: `get_input()`, `is_valid()`, `debounce()`

### Supporting Classes

#### Direction
- **Purpose**: Enumeration for movement directions
- **Responsibilities**:
  - Define valid movement directions
  - Provide direction validation
  - Support direction calculations

## Design Patterns Used

### 1. State Pattern
- **Implementation**: GameState class with state transitions
- **Purpose**: Manage game state (RUNNING, PAUSED, GAME_OVER)
- **Benefits**: Clean state management, easy state validation

### 2. Command Pattern
- **Implementation**: MoveCommand class
- **Purpose**: Encapsulate snake movement as reversible commands
- **Benefits**: Enables undo functionality, decouples movement from execution

### 3. Observer Pattern
- **Implementation**: EventBus and GameEvents classes
- **Purpose**: Decouple game components through event communication
- **Benefits**: Loose coupling, extensible event handling

### 4. Strategy Pattern
- **Implementation**: StrategyAI interface with GreedyAI implementation
- **Purpose**: Pluggable AI implementations
- **Benefits**: Easy AI swapping, extensible AI strategies

## Relationships and Dependencies

### Composition Relationships
- GameSnake contains Board, Snake, GameState
- Board contains apples and grid data
- Snake contains segments and direction

### Dependency Relationships
- GameSnake depends on EventBus for event publishing
- RendererCli depends on GameState for rendering data
- InputController provides input to GameSnake
- StrategyAI implementations depend on GameState for decision making

### Interface Relationships
- StrategyAI defines interface for AI implementations
- GreedyAI implements StrategyAI interface
- EventBus provides interface for event communication

## Key Design Principles

### Separation of Concerns
- Game logic separated from rendering
- Input handling isolated from game logic
- AI strategies decoupled from game engine

### Single Responsibility Principle
- Each class has one clear responsibility
- Methods are focused and cohesive
- Clear boundaries between components

### Open/Closed Principle
- StrategyAI interface allows new AI implementations
- EventBus supports new event types
- Command pattern enables new command types

### Dependency Inversion
- High-level modules depend on abstractions
- StrategyAI interface decouples AI from game engine
- EventBus abstraction decouples components
