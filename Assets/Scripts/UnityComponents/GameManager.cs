using UnityEngine;
using SnakeGame.GameLogic;
using SnakeGame.AI;
using SnakeGame.Events;

namespace SnakeGame.UnityComponents
{
    /// <summary>
    /// Difficulty levels for the Snake game with corresponding tick rates.
    /// </summary>
    public enum DifficultyLevel
    {
        Easy = 0,    // 200ms/tick (5 FPS)
        Normal = 1,  // 125ms/tick (8 FPS)
        Hard = 2     // 80ms/tick (12.5 FPS)
    }
    
    /// <summary>
    /// Main game manager that orchestrates all Unity components and game logic.
    /// Handles Unity's lifecycle and coordinates between pure C# game logic and Unity components.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private int boardWidth = 20;
        [SerializeField] private int boardHeight = 15;
        [SerializeField] private DifficultyLevel difficulty = DifficultyLevel.Normal;
        [SerializeField] private bool enableAI = false;
        [SerializeField] private int? gameSeed = null;
        
        [Header("Components")]
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private SnakeRenderer snakeRenderer;
        [SerializeField] private UIManager uiManager;
        
        private GameSnake gameSnake;
        private IAIStrategy aiStrategy;
        private bool isGameRunning;
        private bool isGameInitialized = false;
        private float lastTickTime;
        private float currentTickRate;
        
        private void Start()
        {
            // Don't auto-start game - wait for menu
            Debug.Log("GameManager initialized - waiting for menu to start game");
        }
        
        private void FixedUpdate()
        {
            if (isGameRunning && !gameSnake.IsPaused() && !gameSnake.IsGameOver())
            {
                ProcessGameTick();
            }
        }
        
        private void Update()
        {
            if (isGameRunning)
            {
                ProcessAI();
            }
            
            ProcessGameControls();
        }
        
        private void SetupComponents()
        {
            // Setup input handler
            if (inputHandler != null)
            {
                inputHandler.SetGameSnake(gameSnake);
            }
            
            // Setup renderer
            if (snakeRenderer != null)
            {
                snakeRenderer.SetGameSnake(gameSnake);
            }
            
            // Setup UI manager
            if (uiManager != null)
            {
                uiManager.SetGameSnake(gameSnake);
                uiManager.SetGameManager(this);
            }
        }
        
        private void ProcessGameTick()
        {
            if (Time.time - lastTickTime >= currentTickRate)
            {
                gameSnake.ProcessTick();
                lastTickTime = Time.time;
            }
        }
        
        private float GetTickRateForDifficulty(DifficultyLevel difficulty)
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return 0.200f; // 200ms/tick (5 FPS)
                case DifficultyLevel.Normal:
                    return 0.125f; // 125ms/tick (8 FPS)
                case DifficultyLevel.Hard:
                    return 0.080f; // 80ms/tick (12.5 FPS)
                default:
                    return 0.125f; // Default to Normal
            }
        }
        
        private void ProcessAI()
        {
            if (enableAI && aiStrategy != null && !gameSnake.IsPaused() && !gameSnake.IsGameOver())
            {
                GameState currentState = gameSnake.GetCurrentState();
                
                if (aiStrategy.CanAnalyze(currentState))
                {
                    Direction aiDirection = aiStrategy.GetNextMove(currentState);
                    gameSnake.ProcessInput(aiDirection);
                }
            }
        }
        
        private void ProcessGameControls()
        {
            // Pause/Resume with P key
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (gameSnake.IsPaused())
                {
                    gameSnake.Resume();
                }
                else if (!gameSnake.IsGameOver())
                {
                    gameSnake.Pause();
                }
            }
            
            // Restart with R key
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (gameSnake != null)
                {
                    gameSnake.Restart();
                    lastTickTime = Time.time;
                    Debug.Log("Game Restarted!");
                }
            }
            
            // Toggle AI with T key
            if (Input.GetKeyDown(KeyCode.T))
            {
                enableAI = !enableAI;
                
                if (enableAI && aiStrategy == null)
                {
                    aiStrategy = new GreedyAI(gameSeed);
                }
                
                Debug.Log($"AI {(enableAI ? "Enabled" : "Disabled")}");
            }
            
            // Difficulty controls with number keys
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetDifficulty(DifficultyLevel.Easy);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetDifficulty(DifficultyLevel.Normal);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetDifficulty(DifficultyLevel.Hard);
            }
            
            // Quit with Escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
        
        
        private void QuitGame()
        {
            Debug.Log("Quitting Game...");
            Application.Quit();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        
        public GameSnake GetGameSnake()
        {
            return gameSnake;
        }
        
        public void SetTickRate(float newTickRate)
        {
            currentTickRate = Mathf.Clamp(newTickRate, 0.05f, 1.0f); // 20 FPS to 1 FPS
        }
        
        public void SetAIEnabled(bool enabled)
        {
            enableAI = enabled;
            
            if (enableAI && aiStrategy == null)
            {
                aiStrategy = new GreedyAI(gameSeed);
            }
        }
        
        public bool IsAIEnabled()
        {
            return enableAI;
        }
        
        public DifficultyLevel GetCurrentDifficulty()
        {
            return difficulty;
        }
        
        public void SetDifficulty(DifficultyLevel newDifficulty)
        {
            difficulty = newDifficulty;
            currentTickRate = GetTickRateForDifficulty(difficulty);
            Debug.Log($"Difficulty changed to: {difficulty}, Tick Rate: {currentTickRate}s");
        }
        
        public float GetCurrentTickRate()
        {
            return currentTickRate;
        }
        
        public void ToggleAI()
        {
            enableAI = !enableAI;
            Debug.Log($"AI {(enableAI ? "enabled" : "disabled")}");
        }
        
        public void RestartGame()
        {
            if (gameSnake != null)
            {
                gameSnake.Restart();
                lastTickTime = Time.time;
                Debug.Log("Game Restarted!");
            }
        }
        
        // Public methods for MenuManager
        public void InitializeGame()
        {
            if (isGameInitialized) return;
            
            currentTickRate = GetTickRateForDifficulty(difficulty);
            lastTickTime = Time.time;
            
            // Check for environment seed
            string seedEnv = System.Environment.GetEnvironmentVariable("SNAKE_GAME_SEED");
            if (!string.IsNullOrEmpty(seedEnv) && int.TryParse(seedEnv, out int envSeed))
            {
                gameSeed = envSeed;
                Debug.Log($"Using environment seed: {gameSeed}");
            }
            
            gameSnake = new GameSnake(boardWidth, boardHeight, gameSeed);
            SetupComponents();
            isGameInitialized = true;
            
            Debug.Log("Game initialized successfully");
        }
        
        public void StartGame()
        {
            if (!isGameInitialized)
            {
                InitializeGame();
            }
            
            isGameRunning = true;
            Debug.Log($"Snake Game Started! Difficulty: {difficulty}, Tick Rate: {currentTickRate}s");
        }
        
        public void StopGame()
        {
            isGameRunning = false;
            Debug.Log("Game stopped");
        }
        
        public void PauseGame()
        {
            if (gameSnake != null)
            {
                gameSnake.Pause();
            }
        }
        
        public void ResumeGame()
        {
            if (gameSnake != null)
            {
                gameSnake.Resume();
            }
        }
        
        public bool IsGamePaused()
        {
            return gameSnake != null && gameSnake.IsPaused();
        }
        
        private void OnDestroy()
        {
            // Cleanup when game manager is destroyed
            isGameRunning = false;
        }
    }
}
