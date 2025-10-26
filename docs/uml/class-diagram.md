# UML Class Diagram - Classic Snake Game (Unity C#)

## Class Diagram (ASCII)

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                            Snake Game Unity Architecture                        │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GameSnake     │    │     Board       │    │     Snake       │
│                 │    │                 │    │                 │
│ - width: int    │◄──►│ - width: int    │    │ - segments: []  │
│ - height: int   │    │ - height: int   │    │ - direction     │
│ - score: int    │    │ - apples: []    │    │ - grow_flag     │
│ - state: State  │    │ - rng: Random   │    │                 │
│ - eventBus      │    │ - seed: int?    │    │ + move()        │
│                 │    │                 │    │ + grow()        │
│ + tick()        │    │ + spawn_apple() │    │ + get_head()    │
│ + handle_input()│    │ + is_free()     │    │ + get_body()    │
│ + update()      │    │ + get_cell()    │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GameState     │    │  MoveCommand    │    │   EventBus      │
│                 │    │                 │    │                 │
│ - board: Board  │    │ - direction     │    │ - subscribers   │
│ - snake: Snake  │    │ - game_state    │    │                 │
│ - score: int    │    │ - timestamp     │    │ + subscribe()   │
│ - tickCount: int│    │                 │    │ + publish()     │
│ - stateType     │    │ + execute()     │    │ + unsubscribe() │
│                 │    │ + undo()        │    │                 │
│ + is_game_over()│    │                 │    │                 │
│ + is_paused()   │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  GameManager    │    │  InputHandler   │    │ SnakeRenderer   │
│  (Unity)        │    │  (Unity)        │    │  (Unity)        │
│                 │    │                 │    │                 │
│ - gameSnake     │    │ - gameSnake     │    │ - gameSnake     │
│ - aiStrategy    │    │ - debounceTime  │    │ - cellSize      │
│ - difficulty    │    │ - lastInput     │    │ - sprites       │
│ - tickRate      │    │                 │    │ - colors        │
│                 │    │ + process_input()│   │                 │
│ + FixedUpdate() │    │ + get_direction()│   │ + render_snake()│
│ + Start()       │    │ + set_debounce() │   │ + render_apples()│
│ + OnDestroy()   │    │                 │    │ + cleanup()     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   UIManager     │    │   GreedyAI      │    │  GameConfigSO   │
│  (Unity)        │    │                 │    │  (Unity)        │
│                 │    │ - gameSnake     │    │                 │
│ - gameSnake     │    │ - rng: Random   │    │ - boardWidth    │
│ - gameManager   │    │ - seed: int     │    │ - boardHeight   │
│ - uiElements    │    │                 │    │ - difficulty    │
│                 │    │ + get_next_move()│   │ - enableAI      │
│ + update_ui()   │    │ + can_analyze() │    │ - gameSeed      │
│ + setup_events()│    │ + is_safe_move()│    │                 │
│ + set_manager() │    │                 │    │ + get_config()  │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   GameEvents    │    │ IAIStrategy     │    │  GameConfig     │
│                 │    │                 │    │                 │
│ + TickEvent     │    │ + get_next_move()│   │ - boardWidth    │
│ + AppleEvent    │    │ + can_analyze() │    │ - boardHeight   │
│ + CollisionEvent│    │                 │    │ - difficulty    │
│ + GameOverEvent │    │                 │    │ - enableAI      │
│ + ScoreEvent    │    │                 │    │ - gameSeed      │
│                 │    │                 │    │                 │
│ + publish()     │    │                 │    │ + validate()    │
│ + subscribe()   │    │                 │    │ + get_defaults()│
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

## Architecture Overview

### Pure C# Game Logic Layer
- **GameSnake**: Main game orchestrator
- **Board**: Grid management and apple placement
- **Snake**: Entity with movement and growth
- **GameState**: Immutable state snapshots
- **MoveCommand**: Command pattern for undo functionality
- **EventBus**: Observer pattern implementation

### Unity Component Layer
- **GameManager**: Main Unity coordinator
- **InputHandler**: Unity Input System integration
- **SnakeRenderer**: Sprite-based rendering
- **UIManager**: UI management and updates
- **GameConfigSO**: Unity ScriptableObject configuration

### AI Strategy Layer
- **IAIStrategy**: Strategy pattern interface
- **GreedyAI**: Intelligent pathfinding implementation

### Event System
- **GameEvents**: Typed event definitions
- **EventBus**: Decoupled communication system

## Key Design Patterns

1. **State Pattern**: GameState for immutable state management
2. **Command Pattern**: MoveCommand for undo functionality
3. **Observer Pattern**: EventBus for decoupled communication
4. **Strategy Pattern**: IAIStrategy for pluggable AI
5. **Component Pattern**: Unity's component system integration

## Separation of Concerns

- **Pure C# Logic**: Game rules, state management, AI algorithms
- **Unity Components**: Rendering, input, UI, configuration
- **Event System**: Decoupled communication between layers
- **Configuration**: Flexible settings via ScriptableObjects and environment variables