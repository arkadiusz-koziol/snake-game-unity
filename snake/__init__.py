"""
Snake Game - A terminal-based implementation of the classic Snake game.

This package implements a complete Snake game with clean architecture,
following Domain-Driven Design principles and Object-Oriented Programming patterns.
"""

__version__ = "1.0.0"
__author__ = "Snake Game Team"
__description__ = "A terminal-based Snake game implementation"

from .engine.game_snake import GameSnake
from .engine.state import GameState, GameStateType
from .engine.board import Board
from .engine.snake import Snake, Direction
from .events.event_bus import EventBus
from .ai.strategy import StrategyAI
from .io.renderer_cli import RendererCli
from .io.input_controller import InputController
from .config.settings import GameConfig

__all__ = [
    "GameSnake",
    "GameState", 
    "GameStateType",
    "Board",
    "Snake",
    "Direction",
    "EventBus",
    "StrategyAI",
    "RendererCli",
    "InputController",
    "GameConfig",
]
