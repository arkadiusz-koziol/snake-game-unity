Phase I — Plan Classic Snake (Analysis & Design)

Description

Analyze and design a CLI-based classic Snake game with clean separation of game logic from rendering and explicit use of patterns (State, Command, Observer, Strategy). Produce a concise problem statement, goals, requirements, and initial architecture artifacts (UML/sketch). Define scope, risks, and a delivery plan for subsequent phases.

Acceptance Criteria
	•	Problem & Goals: A 1–2 page document docs/01_problem_goals.md describing the game purpose, player experience, constraints, and success metrics (e.g., smooth 125 ms tick on 20×15 board).
	•	Functional Requirements: docs/02_requirements.md listing required behaviors (movement, apple eating, growth, collisions, pause, quit) and optional features (Undo, AI).
	•	Architecture Decision Record (ADR): docs/adr/0001-architecture.md stating language (Python 3.11+), CLI rendering via curses, separation of engine vs renderer, event bus, and configuration mechanism.
	•	UML/Concept Sketch: docs/uml/class-diagram.png (or .md ASCII sketch) covering: GameSnake, Board, Snake, MoveCommand, EventBus, StrategyAI, RendererCli, InputController, GameState. Includes class responsibilities and relationships.
	•	Testing Strategy: docs/03_testing-strategy.md defining unit test targets, deterministic RNG seeding, coverage goal (≥90% core), and CI matrix (Py 3.11/3.12).
	•	Delivery Plan: docs/04_plan.md with milestones mapping to Phases II–IV, estimates, and risks (e.g., Windows curses, non-blocking input).

Definition of Done
	•	All documents committed under docs/ and reviewed (peer review checklist in the file footers).
	•	No open TODOs in ADR and plan files.
	•	Documents render correctly in repo (images present, links valid).
	•	CI placeholder workflow exists (.github/workflows/ci.yml) running a no-op job (to be expanded later).

Dependencies / Notes
	•	Targets POSIX terminals; Windows support noted as a risk (requires windows-curses).
	•	Outputs from this phase are inputs for Phase II implementation.
