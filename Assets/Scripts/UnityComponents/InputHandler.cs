using UnityEngine;
using SnakeGame.GameLogic;
using SnakeGame.Events;

namespace SnakeGame.UnityComponents
{
    /// <summary>
    /// Unity component that handles input processing and command generation.
    /// Integrates with Unity's Input System for responsive input handling.
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private GameSnake gameSnake;
        [SerializeField] private bool enableInputDebouncing = true;
        
        private Direction lastInputDirection;
        private float lastInputTime;
        private const float INPUT_DEBOUNCE_TIME = 0.1f; // 100ms debounce
        
        private void Start()
        {
            if (gameSnake == null)
            {
                gameSnake = FindObjectOfType<GameManager>()?.GetGameSnake();
            }
        }
        
        private void Update()
        {
            if (gameSnake == null || gameSnake.IsGameOver() || gameSnake.IsPaused())
            {
                return;
            }
            
            ProcessInput();
        }
        
        private void ProcessInput()
        {
            Direction? inputDirection = GetInputDirection();
            
            if (!inputDirection.HasValue)
            {
                return;
            }
            
            Direction direction = inputDirection.Value;
            
            // Input debouncing
            if (enableInputDebouncing)
            {
                if (Time.time - lastInputTime < INPUT_DEBOUNCE_TIME)
                {
                    return;
                }
                
                if (lastInputDirection == direction)
                {
                    return;
                }
            }
            
            lastInputDirection = direction;
            lastInputTime = Time.time;
            
            gameSnake.ProcessInput(direction);
        }
        
        private Direction? GetInputDirection()
        {
            // Arrow keys
            if (Input.GetKeyDown(KeyCode.UpArrow))
                return Direction.UP;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                return Direction.DOWN;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                return Direction.LEFT;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                return Direction.RIGHT;
            
            // WASD keys
            if (Input.GetKeyDown(KeyCode.W))
                return Direction.UP;
            if (Input.GetKeyDown(KeyCode.S))
                return Direction.DOWN;
            if (Input.GetKeyDown(KeyCode.A))
                return Direction.LEFT;
            if (Input.GetKeyDown(KeyCode.D))
                return Direction.RIGHT;
            
            return null; // No input
        }
        
        public void SetGameSnake(GameSnake gameSnake)
        {
            this.gameSnake = gameSnake;
        }
        
        public void SetInputDebouncing(bool enabled)
        {
            enableInputDebouncing = enabled;
        }
    }
}
