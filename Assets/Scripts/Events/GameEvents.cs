using UnityEngine;

namespace SnakeGame.Events
{
    /// <summary>
    /// Base class for all game events with timestamp and event type information.
    /// </summary>
    public abstract class GameEvent
    {
        public float Timestamp { get; }
        public string EventType { get; }
        
        protected GameEvent(string eventType)
        {
            Timestamp = Time.time;
            EventType = eventType;
        }
    }
    
    /// <summary>
    /// Event published when an apple is spawned on the board.
    /// </summary>
    public class AppleSpawnedEvent : GameEvent
    {
        public Vector2Int Position { get; }
        public int TickCount { get; }
        
        public AppleSpawnedEvent(Vector2Int position, int tickCount) : base("APPLE_SPAWNED")
        {
            Position = position;
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when the snake consumes an apple.
    /// </summary>
    public class AppleEatenEvent : GameEvent
    {
        public Vector2Int Position { get; }
        public int Score { get; }
        public int SnakeLength { get; }
        public int TickCount { get; }
        
        public AppleEatenEvent(Vector2Int position, int score, int snakeLength, int tickCount) : base("APPLE_EATEN")
        {
            Position = position;
            Score = score;
            SnakeLength = snakeLength;
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when a collision is detected.
    /// </summary>
    public class CollisionEvent : GameEvent
    {
        public CollisionType CollisionType { get; }
        public Vector2Int Position { get; }
        public int TickCount { get; }
        
        public CollisionEvent(CollisionType collisionType, Vector2Int position, int tickCount) : base("COLLISION")
        {
            CollisionType = collisionType;
            Position = position;
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when the game ends.
    /// </summary>
    public class GameOverEvent : GameEvent
    {
        public int FinalScore { get; }
        public int FinalSnakeLength { get; }
        public int TickCount { get; }
        
        public GameOverEvent(int finalScore, int finalSnakeLength, int tickCount) : base("GAME_OVER")
        {
            FinalScore = finalScore;
            FinalSnakeLength = finalSnakeLength;
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published on every game tick.
    /// </summary>
    public class TickEvent : GameEvent
    {
        public int TickCount { get; }
        public GameLogic.GameState GameState { get; }
        
        public TickEvent(int tickCount, GameLogic.GameState gameState) : base("TICK")
        {
            TickCount = tickCount;
            GameState = gameState;
        }
    }
    
    /// <summary>
    /// Event published when the score changes.
    /// </summary>
    public class ScoreChangedEvent : GameEvent
    {
        public int NewScore { get; }
        public int TickCount { get; }
        
        public ScoreChangedEvent(int newScore, int tickCount) : base("SCORE_CHANGED")
        {
            NewScore = newScore;
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when the game is paused.
    /// </summary>
    public class GamePausedEvent : GameEvent
    {
        public int TickCount { get; }
        
        public GamePausedEvent(int tickCount) : base("GAME_PAUSED")
        {
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when the game is resumed.
    /// </summary>
    public class GameResumedEvent : GameEvent
    {
        public int TickCount { get; }
        
        public GameResumedEvent(int tickCount) : base("GAME_RESUMED")
        {
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Event published when the game is restarted.
    /// </summary>
    public class GameRestartedEvent : GameEvent
    {
        public int TickCount { get; }
        
        public GameRestartedEvent(int tickCount) : base("GAME_RESTARTED")
        {
            TickCount = tickCount;
        }
    }
    
    /// <summary>
    /// Enumeration for collision types.
    /// </summary>
    public enum CollisionType
    {
        WALL,
        SELF
    }
}
