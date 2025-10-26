"""
Core game engine components.

This module contains the fundamental game logic classes including
GameSnake (main orchestrator), Board (grid management), Snake (entity),
GameState (immutable state), and MoveCommand (command pattern).
"""

from .game_snake import GameSnake
from .board import Board
from .snake import Snake, Direction
from .state import GameState, GameStateType
from .commands import MoveCommand

__all__ = [
    "GameSnake",
    "Board", 
    "Snake",
    "Direction",
    "GameState",
    "GameStateType",
    "MoveCommand",
]
