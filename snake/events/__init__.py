"""
Event system implementation.

This module provides the Observer pattern implementation with
typed events and centralized event bus for decoupled communication.
"""

from .event_bus import EventBus
from .events import (
    GameEvent,
    AppleSpawnedEvent,
    AppleEatenEvent,
    CollisionEvent,
    GameOverEvent,
    TickEvent,
    ScoreChangedEvent,
)

__all__ = [
    "EventBus",
    "GameEvent",
    "AppleSpawnedEvent", 
    "AppleEatenEvent",
    "CollisionEvent",
    "GameOverEvent",
    "TickEvent",
    "ScoreChangedEvent",
]
