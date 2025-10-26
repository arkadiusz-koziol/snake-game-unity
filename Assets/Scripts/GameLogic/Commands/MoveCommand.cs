using UnityEngine;

namespace SnakeGame.GameLogic.Commands
{
    /// <summary>
    /// Command pattern implementation for reversible snake movement.
    /// Enables undo functionality by encapsulating movement operations.
    /// </summary>
    public class MoveCommand : ICommand
    {
        private readonly Snake snake;
        private readonly Direction direction;
        private readonly GameState previousState;
        private readonly GameState newState;
        
        public MoveCommand(Snake snake, Direction direction, GameState previousState, GameState newState)
        {
            this.snake = snake;
            this.direction = direction;
            this.previousState = previousState;
            this.newState = newState;
        }
        
        public void Execute()
        {
            snake.SetDirection(direction);
            snake.Move();
        }
        
        public void Undo()
        {
            // Restore previous snake state
            // This would require implementing state restoration in Snake class
            // For now, this is a placeholder for the undo functionality
        }
        
        public bool CanUndo()
        {
            return previousState != null;
        }
        
        public GameState GetPreviousState()
        {
            return previousState;
        }
        
        public GameState GetNewState()
        {
            return newState;
        }
    }
    
    /// <summary>
    /// Interface for command pattern implementation.
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
        bool CanUndo();
    }
}
