# TODO.md - Snake Game Implementation Tasks

## Table of Contents

### Phase I - Plan Classic Snake (Analysis & Design)
- [x] 1. [Create Problem & Goals Document](#1-create-problem--goals-document)
- [x] 2. [Define Functional Requirements](#2-define-functional-requirements)
- [x] 3. [Create Architecture Decision Record](#3-create-architecture-decision-record)
- [x] 4. [Design UML/Concept Sketch](#4-design-umlconcept-sketch)
- [x] 5. [Define Testing Strategy](#5-define-testing-strategy)
- [x] 6. [Create Delivery Plan](#6-create-delivery-plan)

### Phase II - Implement Core Engine & Minimal Play Loop
- [ ] 7. [Create Project Scaffold](#7-create-project-scaffold)
- [ ] 8. [Implement Core Game Engine](#8-implement-core-game-engine)
- [ ] 9. [Implement Movement System](#9-implement-movement-system)
- [ ] 10. [Implement Apple Spawning & Eating](#10-implement-apple-spawning--eating)
- [ ] 11. [Implement Collision Detection](#11-implement-collision-detection)
- [ ] 12. [Implement Basic Controls](#12-implement-basic-controls)
- [ ] 13. [Implement Tick Rate Management](#13-implement-tick-rate-management)
- [ ] 14. [Implement Deterministic Behavior](#14-implement-deterministic-behavior)
- [ ] 15. [Create Runnable Application](#15-create-runnable-application)

### Phase III - Add Features (Undo, Observer Events, AI, Config, UX)
- [ ] 16. [Implement Observer Events](#16-implement-observer-events)
- [ ] 17. [Implement Undo Functionality](#17-implement-undo-functionality)
- [ ] 18. [Implement Strategy AI](#18-implement-strategy-ai)
- [ ] 19. [Implement Configuration System](#19-implement-configuration-system)
- [ ] 20. [Enhance Renderer with Header Panel](#20-enhance-renderer-with-header-panel)
- [ ] 21. [Implement Input Debouncing](#21-implement-input-debouncing)

### Phase IV - Finalize, Harden, and Document
- [ ] 22. [Ensure Full Playability](#22-ensure-full-playability)
- [ ] 23. [Validate Performance Requirements](#23-validate-performance-requirements)
- [ ] 24. [Achieve Test Coverage Goals](#24-achieve-test-coverage-goals)
- [ ] 25. [Complete Documentation](#25-complete-documentation)
- [ ] 26. [Setup Packaging](#26-setup-packaging)
- [ ] 27. [Implement CI/CD Pipeline](#27-implement-cicd-pipeline)
- [ ] 28. [Quality Gate Validation](#28-quality-gate-validation)

---

## Task Descriptions

### Phase I - Plan Classic Snake (Analysis & Design)

### 1. Create Problem & Goals Document

**Category:** Phase I  
**Description:** Create a 1-2 page document `docs/01_problem_goals.md` describing the game purpose, player experience, constraints, and success metrics.  
**Acceptance Criteria:**
- Document describes game purpose and player experience
- Defines constraints (POSIX terminals, Windows support risk)
- Specifies success metrics (smooth 125ms tick on 20×15 board)
- Document is 1-2 pages in length
- Committed under `docs/` directory

### 2. Define Functional Requirements

**Category:** Phase I  
**Description:** Create `docs/02_requirements.md` listing required behaviors and optional features.  
**Acceptance Criteria:**
- Lists required behaviors: movement, apple eating, growth, collisions, pause, quit
- Lists optional features: Undo, AI
- Document is clear and comprehensive
- Committed under `docs/` directory

### 3. Create Architecture Decision Record

**Category:** Phase I  
**Description:** Create `docs/adr/0001-architecture.md` stating language choice, CLI rendering, separation of concerns, and configuration mechanism.  
**Acceptance Criteria:**
- States Python 3.11+ as language choice
- Specifies CLI rendering via curses
- Defines separation of engine vs renderer
- Describes event bus and configuration mechanism
- No open TODOs in ADR file

### 4. Design UML/Concept Sketch

**Category:** Phase I  
**Description:** Create `docs/uml/class-diagram.png` (or .md ASCII sketch) covering core classes and their relationships.  
**Acceptance Criteria:**
- Covers: GameSnake, Board, Snake, MoveCommand, EventBus, StrategyAI, RendererCli, InputController, GameState
- Shows class responsibilities and relationships
- Image renders correctly in repo
- Committed under `docs/uml/` directory

### 5. Define Testing Strategy

**Category:** Phase I  
**Description:** Create `docs/03_testing-strategy.md` defining unit test targets, deterministic RNG seeding, coverage goals, and CI matrix.  
**Acceptance Criteria:**
- Defines unit test targets for core components
- Specifies deterministic RNG seeding for tests
- Sets coverage goal (≥90% core)
- Defines CI matrix (Py 3.11/3.12)
- Committed under `docs/` directory

### 6. Create Delivery Plan

**Category:** Phase I  
**Description:** Create `docs/04_plan.md` with milestones mapping to Phases II–IV, estimates, and risks.  
**Acceptance Criteria:**
- Maps milestones to Phases II–IV
- Includes time estimates
- Identifies risks (Windows curses, non-blocking input)
- No open TODOs in plan file
- Committed under `docs/` directory

### Phase II - Implement Core Engine & Minimal Play Loop

### 7. Create Project Scaffold

**Category:** Phase II  
**Description:** Create the basic project structure with required modules and packages.  
**Acceptance Criteria:**
- Create `snake/` package with modules:
  - `engine/game_snake.py`, `engine/board.py`, `engine/snake.py`, `engine/commands.py`, `engine/state.py`
  - `io/renderer_cli.py`, `io/input_controller.py`
  - `events/event_bus.py` (may emit minimal events or stubs)
  - `ai/strategy.py` (interface only)
- All modules have proper imports and basic structure

### 8. Implement Core Game Engine

**Category:** Phase II  
**Description:** Implement the core game engine classes: GameSnake, Board, Snake, and GameState.  
**Acceptance Criteria:**
- Implement `GameSnake` class for game orchestration
- Implement `Board` class for grid management and apple placement
- Implement `Snake` class for segment management and movement
- Implement `GameState` class for immutable state snapshots
- All classes have proper type hints and basic functionality

### 9. Implement Movement System

**Category:** Phase II  
**Description:** Implement snake movement with arrow keys or WASD, preventing 180° reversal within same tick.  
**Acceptance Criteria:**
- Snake moves one cell per tick in pressed direction
- Cannot reverse 180° within the same tick
- Supports both arrow keys and W/A/S/D
- Movement is smooth and responsive

### 10. Implement Apple Spawning & Eating

**Category:** Phase II  
**Description:** Implement apple spawning on free cells and eating mechanics with score and growth.  
**Acceptance Criteria:**
- Apples spawn on free cells when head enters *
- Score increases by 1 per apple eaten
- Snake length increases by 1 on next tick after eating
- New apple spawns on free cell after eating
- Works with deterministic RNG seed

### 11. Implement Collision Detection

**Category:** Phase II  
**Description:** Implement collision detection for walls and self-collision, ending game on collision.  
**Acceptance Criteria:**
- Wall collision detection at board borders
- Self-collision detection when head hits body
- Game state switches to GAME_OVER on collision
- Visible "Game Over" banner is rendered
- No wrap-around behavior

### 12. Implement Basic Controls

**Category:** Phase II  
**Description:** Implement basic game controls: P for pause/resume, Q for quit with terminal restoration.  
**Acceptance Criteria:**
- P key pauses/resumes game
- Q key quits and restores terminal
- Controls work during gameplay
- Terminal state is properly restored on quit

### 13. Implement Tick Rate Management

**Category:** Phase II  
**Description:** Implement tick rate management with Normal difficulty at 125ms/tick (±10ms tolerance).  
**Acceptance Criteria:**
- Normal difficulty: 125ms/tick (±10ms tolerance)
- Measured over 200 ticks for accuracy
- Tick timing is consistent and stable
- Performance remains steady

### 14. Implement Deterministic Behavior

**Category:** Phase II  
**Description:** Implement deterministic behavior using configurable RNG seed for testing.  
**Acceptance Criteria:**
- Setting GAME_SEED env yields identical first apple position across runs
- RNG seed is properly isolated and configurable
- Deterministic behavior works for testing
- Same seed produces identical game behavior

### 15. Create Runnable Application

**Category:** Phase II  
**Description:** Create a runnable application that can be started with `python -m snake` or `python main.py`.  
**Acceptance Criteria:**
- Application starts with `python -m snake` or `python main.py`
- README quickstart works
- Clean startup/shutdown
- No uncaught exceptions
- Terminal state restored on exit

### Phase III - Add Features (Undo, Observer Events, AI, Config, UX)

### 16. Implement Observer Events

**Category:** Phase III  
**Description:** Implement full Observer event bus with typed events and payloads.  
**Acceptance Criteria:**
- Publish events: APPLE_SPAWNED, APPLE_EATEN, COLLISION, GAME_OVER, TICK, SCORE_CHANGED
- Events include payloads (positions, score, tick)
- Event bus supports subscription and publishing
- Events are properly typed and validated

### 17. Implement Undo Functionality

**Category:** Phase III  
**Description:** Implement undo functionality using Command pattern or state mementos.  
**Acceptance Criteria:**
- U key undoes previous move when enabled in config
- Previous state restored: position, direction, grow flags, apple positions, score
- Board re-renders after undo
- Works with grow/no-grow transitions
- Undo can be disabled via configuration

### 18. Implement Strategy AI

**Category:** Phase III  
**Description:** Implement pluggable Strategy AI with simple greedy solver.  
**Acceptance Criteria:**
- T key toggles AI mode
- AI performs ≥20 consecutive safe moves towards next apple
- Works at Normal difficulty
- AI can be toggled during gameplay
- Simple greedy strategy implementation

### 19. Implement Configuration System

**Category:** Phase III  
**Description:** Implement flexible configuration system supporting YAML files and environment variables.  
**Acceptance Criteria:**
- Support config.yaml with board dimensions, difficulty, enableUndo, seed
- Environment variable overrides
- Configuration precedence: env > YAML > defaults
- Configuration validation and error handling
- Defaults work out of the box

### 20. Enhance Renderer with Header Panel

**Category:** Phase III  
**Description:** Enhance CLI rendering with header panel showing game state information.  
**Acceptance Criteria:**
- Header panel shows: score, length, speed, paused, AI flags
- Double-buffer flicker-free redraw
- Information is clearly displayed and updated
- Rendering is smooth and responsive

### 21. Implement Input Debouncing

**Category:** Phase III  
**Description:** Implement input debouncing to prevent multiple direction changes in one tick.  
**Acceptance Criteria:**
- Prevents two direction changes in one tick
- Non-blocking input remains responsive under load
- Input handling is smooth and consistent
- No input loss or double-moves

### Phase IV - Finalize, Harden, and Document

### 22. Ensure Full Playability

**Category:** Phase IV  
**Description:** Ensure complete playability with all features working correctly.  
**Acceptance Criteria:**
- Can move, eat apples, grow, pause/resume, toggle AI, undo (if enabled)
- Game ends on collisions
- All controls work as expected
- Game is fully playable with default settings

### 23. Validate Performance Requirements

**Category:** Phase IV  
**Description:** Validate performance requirements with steady tick timing over extended periods.  
**Acceptance Criteria:**
- Maintain steady tick timing (±10ms) across ≥1,000 ticks on Normal difficulty
- Performance remains consistent
- No frame rate drops or stuttering
- Memory usage remains stable

### 24. Achieve Test Coverage Goals

**Category:** Phase IV  
**Description:** Achieve comprehensive test coverage for core modules.  
**Acceptance Criteria:**
- Core modules reach ≥90% unit test coverage
- Mutation or branch-condition tests cover critical logic
- Tests cover collision, growth, and edge cases
- All tests pass consistently

### 25. Complete Documentation

**Category:** Phase IV  
**Description:** Complete comprehensive documentation including README and CHANGELOG.  
**Acceptance Criteria:**
- README.md complete with install, run, controls, config, platform notes
- Include ASCII example in README
- Create CHANGELOG.md (v1.0.0)
- Documentation is clear and comprehensive

### 26. Setup Packaging

**Category:** Phase IV  
**Description:** Setup proper packaging with pyproject.toml and entry points.  
**Acceptance Criteria:**
- Create pyproject.toml or setup.cfg with entry point snake
- Versioning set to 1.0.0
- Package can be installed and run
- Entry point works correctly

### 27. Implement CI/CD Pipeline

**Category:** Phase IV  
**Description:** Implement comprehensive CI/CD pipeline with matrix builds and coverage reporting.  
**Acceptance Criteria:**
- GitHub Actions workflow with matrix build
- Runs on Python 3.11 & 3.12
- Includes lint, type-check, tests
- Coverage reporting
- Status badge added to README

### 28. Quality Gate Validation

**Category:** Phase IV  
**Description:** Validate quality gate requirements and create final release.  
**Acceptance Criteria:**
- Zero runtime warnings
- Lint/type-check clean
- Terminal reliably restored after exceptions
- All acceptance tests green locally and in CI
- Tags/release created (v1.0.0) with release notes
- Final ADR docs/adr/0003-release-1-0.md summarizing scope and limitations
