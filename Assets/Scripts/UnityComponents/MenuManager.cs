using UnityEngine;
using UnityEngine.UI;
using SnakeGame.UnityComponents;

namespace SnakeGame.UnityComponents
{
    /// <summary>
    /// Manages the main menu UI and game state transitions.
    /// Handles menu navigation and game initialization.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu UI Elements")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button pauseMenuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button backToMenuButton;
        
        [Header("Game References")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
        
        private bool isGameStarted = false;
        
        private void Start()
        {
            // Find GameManager if not assigned
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            
            // Find UIManager if not assigned
            if (uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
            }
            
            SetupMenuButtons();
            ShowMainMenu();
            
            Debug.Log("GUI Menu initialized - ready for user interaction");
            Debug.Log("=== TEMPORARY KEYBOARD CONTROLS ===");
            Debug.Log("Press SPACE to START GAME");
            Debug.Log("Press ESCAPE to EXIT GAME");
            Debug.Log("================================");
        }
        
        private void SetupMenuButtons()
        {
            if (startGameButton != null)
            {
                startGameButton.onClick.AddListener(StartGame);
            }
            
            if (exitButton != null)
            {
                exitButton.onClick.AddListener(ExitGame);
            }
            
            if (pauseMenuButton != null)
            {
                pauseMenuButton.onClick.AddListener(ShowPauseMenu);
            }
            
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(ResumeGame);
            }
            
            if (backToMenuButton != null)
            {
                backToMenuButton.onClick.AddListener(BackToMainMenu);
            }
        }
        
        private void Update()
        {
            // Handle escape key for menu navigation
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGameStarted)
                {
                    if (gameManager != null && gameManager.IsGamePaused())
                    {
                        ResumeGame();
                    }
                    else
                    {
                        ShowPauseMenu();
                    }
                }
                else
                {
                    ExitGame();
                }
            }
            
            // Temporary keyboard controls for testing without UI
            if (!isGameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    StartGame();
                }
            }
        }
        
        public void StartGame()
        {
            Debug.Log("Starting Game from Menu...");
            
            // Initialize game if not already started
            if (!isGameStarted && gameManager != null)
            {
                gameManager.InitializeGame();
                isGameStarted = true;
            }
            
            // Start the game
            if (gameManager != null)
            {
                gameManager.StartGame();
            }
            
            // Ensure UIManager is connected to GameManager
            if (uiManager != null && gameManager != null)
            {
                uiManager.SetGameManager(gameManager);
            }
            
            // Show game UI
            ShowGameUI();
        }
        
        public void ExitGame()
        {
            Debug.Log("Exiting Game...");
            Application.Quit();
        }
        
        public void ShowPauseMenu()
        {
            if (gameManager != null)
            {
                gameManager.PauseGame();
            }
            
            ShowMainMenu();
        }
        
        public void ResumeGame()
        {
            if (gameManager != null)
            {
                gameManager.ResumeGame();
            }
            
            ShowGameUI();
        }
        
        public void BackToMainMenu()
        {
            Debug.Log("Returning to Main Menu...");
            
            // Stop the game
            if (gameManager != null)
            {
                gameManager.StopGame();
            }
            
            isGameStarted = false;
            ShowMainMenu();
        }
        
        private void ShowMainMenu()
        {
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
            }
            
            if (gamePanel != null)
            {
                gamePanel.SetActive(false);
            }
            
            // Show/hide appropriate buttons
            if (startGameButton != null)
            {
                startGameButton.gameObject.SetActive(!isGameStarted);
            }
            
            if (pauseMenuButton != null)
            {
                pauseMenuButton.gameObject.SetActive(isGameStarted);
            }
            
            if (resumeButton != null)
            {
                resumeButton.gameObject.SetActive(false);
            }
            
            if (backToMenuButton != null)
            {
                backToMenuButton.gameObject.SetActive(isGameStarted);
            }
        }
        
        private void ShowGameUI()
        {
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(false);
            }
            
            if (gamePanel != null)
            {
                gamePanel.SetActive(true);
            }
            
            // Show/hide appropriate buttons
            if (startGameButton != null)
            {
                startGameButton.gameObject.SetActive(false);
            }
            
            if (pauseMenuButton != null)
            {
                pauseMenuButton.gameObject.SetActive(false);
            }
            
            if (resumeButton != null)
            {
                resumeButton.gameObject.SetActive(false);
            }
            
            if (backToMenuButton != null)
            {
                backToMenuButton.gameObject.SetActive(false);
            }
        }
        
        public bool IsGameStarted()
        {
            return isGameStarted;
        }
    }
}
