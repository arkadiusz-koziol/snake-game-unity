using UnityEngine;
using SnakeGame.Events;

namespace SnakeGame.GameLogic
{
    /// <summary>
    /// Main game orchestrator that manages the game loop, state transitions, and component coordination.
    /// Follows the State pattern for game state management.
    /// </summary>
    public class GameSnake
    {
        private Board board;
        private Snake snake;
        private GameState currentState;
        private EventBus eventBus;
        private int score;
        private int tickCount;
        private bool isPaused;
        private bool isGameOver;
        
        public GameSnake(int width = 20, int height = 15, int? seed = null)
        {
            board = new Board(width, height, seed);
            snake = new Snake(width / 2, height / 2);
            eventBus = new EventBus();
            score = 0;
            tickCount = 0;
            isPaused = false;
            isGameOver = false;
            
            currentState = new GameState(board, snake, score, tickCount, GameStateType.RUNNING);
        }
        
        public GameState GetCurrentState() => currentState;
        public EventBus GetEventBus() => eventBus;
        public bool IsPaused() => isPaused;
        public bool IsGameOver() => isGameOver;
        public int GetScore() => score;
        public int GetTickCount() => tickCount;
        
        public void Pause()
        {
            if (!isGameOver)
            {
                isPaused = true;
                currentState = currentState.WithGameStateType(GameStateType.PAUSED);
                eventBus.Publish(new GamePausedEvent(tickCount));
            }
        }
        
        public void Resume()
        {
            if (!isGameOver && isPaused)
            {
                isPaused = false;
                currentState = currentState.WithGameStateType(GameStateType.RUNNING);
                eventBus.Publish(new GameResumedEvent(tickCount));
            }
        }
        
        public void ProcessTick()
        {
            if (isGameOver || isPaused) return;
            
            tickCount++;
            
            // Move snake
            snake.Move();
            
            // Check collisions
            if (CheckCollisions())
            {
                GameOver();
                return;
            }
            
            // Check apple consumption
            if (CheckAppleConsumption())
            {
                ConsumeApple();
            }
            
            // Update state
            currentState = new GameState(board, snake, score, tickCount, GameStateType.RUNNING);
            
            // Publish tick event
            eventBus.Publish(new TickEvent(tickCount, currentState));
        }
        
        private bool CheckCollisions()
        {
            Vector2Int headPosition = snake.GetHeadPosition();
            
            // Wall collision
            if (headPosition.x < 0 || headPosition.x >= board.GetWidth() ||
                headPosition.y < 0 || headPosition.y >= board.GetHeight())
            {
                eventBus.Publish(new CollisionEvent(CollisionType.WALL, headPosition, tickCount));
                return true;
            }
            
            // Self collision
            if (snake.CheckSelfCollision())
            {
                eventBus.Publish(new CollisionEvent(CollisionType.SELF, headPosition, tickCount));
                return true;
            }
            
            return false;
        }
        
        private bool CheckAppleConsumption()
        {
            Vector2Int headPosition = snake.GetHeadPosition();
            return board.HasAppleAt(headPosition.x, headPosition.y);
        }
        
        private void ConsumeApple()
        {
            Vector2Int headPosition = snake.GetHeadPosition();
            board.RemoveApple(headPosition.x, headPosition.y);
            score++;
            snake.SetGrowFlag(true);
            
            // Spawn new apple
            Vector2Int newApplePosition = board.SpawnApple();
            
            eventBus.Publish(new AppleEatenEvent(headPosition, score, snake.GetLength(), tickCount));
            eventBus.Publish(new AppleSpawnedEvent(newApplePosition, tickCount));
            eventBus.Publish(new ScoreChangedEvent(score, tickCount));
        }
        
        private void GameOver()
        {
            isGameOver = true;
            currentState = currentState.WithGameStateType(GameStateType.GAME_OVER);
            eventBus.Publish(new GameOverEvent(score, snake.GetLength(), tickCount));
        }
        
        public void ProcessInput(Direction direction)
        {
            if (isGameOver || isPaused) return;
            
            snake.SetDirection(direction);
        }
        
        public void Restart()
        {
            board = new Board(board.GetWidth(), board.GetHeight());
            snake = new Snake(board.GetWidth() / 2, board.GetHeight() / 2);
            score = 0;
            tickCount = 0;
            isPaused = false;
            isGameOver = false;
            
            currentState = new GameState(board, snake, score, tickCount, GameStateType.RUNNING);
            eventBus.Publish(new GameRestartedEvent(tickCount));
        }
    }
}
