using UnityEngine;
using UnityEngine.UI;
using SnakeGame.GameLogic;
using SnakeGame.Events;

namespace SnakeGame.UnityComponents
{
    /// <summary>
    /// Unity component that manages the game UI and displays game information.
    /// Handles UI updates based on game state changes.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text lengthText;
        [SerializeField] private Text speedText;
        [SerializeField] private Text statusText;
        [SerializeField] private Text difficultyText;
        [SerializeField] private Text controlsText;
        [SerializeField] private GameObject pauseOverlay;
        [SerializeField] private GameObject gameOverOverlay;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button easyButton;
        [SerializeField] private Button normalButton;
        [SerializeField] private Button hardButton;
        [SerializeField] private Button aiToggleButton;
        
        [Header("Game References")]
        [SerializeField] private GameSnake gameSnake;
        [SerializeField] private GameManager gameManager;
        
        private void Start()
        {
            if (gameSnake == null)
            {
                gameSnake = FindObjectOfType<GameManager>()?.GetGameSnake();
            }
            
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            
            SetupEventListeners();
            SetupButtons();
            UpdateUI();
        }
        
        private void SetupEventListeners()
        {
            if (gameSnake?.GetEventBus() != null)
            {
                EventBus eventBus = gameSnake.GetEventBus();
                
                eventBus.Subscribe<ScoreChangedEvent>(OnScoreChanged);
                eventBus.Subscribe<GamePausedEvent>(OnGamePaused);
                eventBus.Subscribe<GameResumedEvent>(OnGameResumed);
                eventBus.Subscribe<GameOverEvent>(OnGameOver);
                eventBus.Subscribe<GameRestartedEvent>(OnGameRestarted);
            }
        }
        
        private void SetupButtons()
        {
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(RestartGame);
            }
            
            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(PauseGame);
            }
            
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(ResumeGame);
            }
            
            if (easyButton != null)
            {
                easyButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Easy));
            }
            
            if (normalButton != null)
            {
                normalButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Normal));
            }
            
            if (hardButton != null)
            {
                hardButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Hard));
            }
            
            if (aiToggleButton != null)
            {
                aiToggleButton.onClick.AddListener(ToggleAI);
            }
        }
        
        private void Update()
        {
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            if (gameSnake == null) return;
            
            GameState currentState = gameSnake.GetCurrentState();
            
            // Update score
            if (scoreText != null)
            {
                scoreText.text = $"Score: {currentState.Score}";
            }
            
            // Update snake length
            if (lengthText != null)
            {
                lengthText.text = $"Length: {currentState.Snake.GetLength()}";
            }
            
            // Update speed (based on difficulty)
            if (speedText != null)
            {
                if (gameManager != null)
                {
                    DifficultyLevel difficulty = gameManager.GetCurrentDifficulty();
                    float tickRate = gameManager.GetCurrentTickRate();
                    speedText.text = $"Speed: {difficulty} ({tickRate * 1000:F0}ms)";
                }
                else
                {
                    speedText.text = "Speed: Normal";
                }
            }
            
            // Update difficulty display
            if (difficultyText != null && gameManager != null)
            {
                DifficultyLevel difficulty = gameManager.GetCurrentDifficulty();
                difficultyText.text = $"Difficulty: {difficulty}";
            }
            
            // Update status
            if (statusText != null)
            {
                if (currentState.IsGameOver())
                {
                    statusText.text = "Game Over";
                }
                else if (currentState.IsPaused())
                {
                    statusText.text = "Paused";
                }
                else
                {
                    statusText.text = "Running";
                }
            }
            
            // Update controls info
            if (controlsText != null)
            {
                controlsText.text = "Controls: Arrow Keys/WASD - Move | P - Pause | R - Restart | T - AI Toggle | 1/2/3 - Difficulty";
            }
            
            // Update overlays
            if (pauseOverlay != null)
            {
                pauseOverlay.SetActive(currentState.IsPaused());
            }
            
            if (gameOverOverlay != null)
            {
                gameOverOverlay.SetActive(currentState.IsGameOver());
            }
            
            // Update button states
            if (pauseButton != null)
            {
                pauseButton.gameObject.SetActive(!currentState.IsPaused() && !currentState.IsGameOver());
            }
            
            if (resumeButton != null)
            {
                resumeButton.gameObject.SetActive(currentState.IsPaused());
            }
            
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(currentState.IsGameOver());
            }
        }
        
        private void OnScoreChanged(ScoreChangedEvent scoreEvent)
        {
            UpdateUI();
        }
        
        private void OnGamePaused(GamePausedEvent pauseEvent)
        {
            UpdateUI();
        }
        
        private void OnGameResumed(GameResumedEvent resumeEvent)
        {
            UpdateUI();
        }
        
        private void OnGameOver(GameOverEvent gameOverEvent)
        {
            UpdateUI();
        }
        
        private void OnGameRestarted(GameRestartedEvent restartEvent)
        {
            UpdateUI();
        }
        
        public void SetGameSnake(GameSnake gameSnake)
        {
            this.gameSnake = gameSnake;
            SetupEventListeners();
        }
        
        public void SetGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
        // Button event handlers
        private void RestartGame()
        {
            if (gameManager != null)
            {
                gameManager.RestartGame();
            }
        }
        
        private void PauseGame()
        {
            if (gameSnake != null)
            {
                gameSnake.Pause();
            }
        }
        
        private void ResumeGame()
        {
            if (gameSnake != null)
            {
                gameSnake.Resume();
            }
        }
        
        private void SetDifficulty(DifficultyLevel difficulty)
        {
            if (gameManager != null)
            {
                gameManager.SetDifficulty(difficulty);
            }
        }
        
        private void ToggleAI()
        {
            if (gameManager != null)
            {
                gameManager.ToggleAI();
            }
        }
    }
}
