using UnityEngine;

namespace SnakeGame.GameLogic
{
    /// <summary>
    /// Immutable game state snapshot for AI decision making and command operations.
    /// Follows the State pattern for game state management.
    /// </summary>
    public class GameState
    {
        public Board Board { get; }
        public Snake Snake { get; }
        public int Score { get; }
        public int TickCount { get; }
        public GameStateType StateType { get; }
        
        public GameState(Board board, Snake snake, int score, int tickCount, GameStateType stateType)
        {
            Board = board;
            Snake = snake;
            Score = score;
            TickCount = tickCount;
            StateType = stateType;
        }
        
        public GameState WithGameStateType(GameStateType newStateType)
        {
            return new GameState(Board, Snake, Score, TickCount, newStateType);
        }
        
        public bool IsGameOver()
        {
            return StateType == GameStateType.GAME_OVER;
        }
        
        public bool IsPaused()
        {
            return StateType == GameStateType.PAUSED;
        }
        
        public bool IsRunning()
        {
            return StateType == GameStateType.RUNNING;
        }
        
        public GameState Copy()
        {
            return new GameState(Board, Snake, Score, TickCount, StateType);
        }
    }
    
    /// <summary>
    /// Enumeration for game state types.
    /// </summary>
    public enum GameStateType
    {
        RUNNING,
        PAUSED,
        GAME_OVER
    }
}
