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
            CreateUIElementsIfNeeded();
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
        
        private void CreateUIElementsIfNeeded()
        {
            // Find Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No Canvas found in scene!");
                return;
            }
            
            // Create MainMenuPanel if it doesn't exist
            if (mainMenuPanel == null)
            {
                GameObject panel = new GameObject("MainMenuPanel");
                panel.transform.SetParent(canvas.transform, false);
                
                // Add RectTransform
                RectTransform rectTransform = panel.AddComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.anchoredPosition = Vector2.zero;
                
                // Add Image component
                UnityEngine.UI.Image image = panel.AddComponent<UnityEngine.UI.Image>();
                image.color = new Color(0, 0, 0.3f, 0.8f); // Dark blue
                
                mainMenuPanel = panel;
                Debug.Log("Created MainMenuPanel programmatically");
            }
            
            // Create StartGameButton if it doesn't exist
            if (startGameButton == null)
            {
                GameObject button = new GameObject("StartGameButton");
                button.transform.SetParent(mainMenuPanel.transform, false);
                
                // Add RectTransform
                RectTransform rectTransform = button.AddComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.sizeDelta = new Vector2(200, 50);
                rectTransform.anchoredPosition = new Vector2(0, 50);
                
                // Add Image component
                UnityEngine.UI.Image image = button.AddComponent<UnityEngine.UI.Image>();
                image.color = Color.green;
                
                // Add Button component
                UnityEngine.UI.Button buttonComponent = button.AddComponent<UnityEngine.UI.Button>();
                
                // Add Text
                GameObject text = new GameObject("Text");
                text.transform.SetParent(button.transform, false);
                
                RectTransform textRect = text.AddComponent<RectTransform>();
                textRect.anchorMin = Vector2.zero;
                textRect.anchorMax = Vector2.one;
                textRect.sizeDelta = Vector2.zero;
                textRect.anchoredPosition = Vector2.zero;
                
                UnityEngine.UI.Text textComponent = text.AddComponent<UnityEngine.UI.Text>();
                textComponent.text = "START";
                textComponent.fontSize = 24;
                textComponent.color = Color.black;
                textComponent.alignment = TextAnchor.MiddleCenter;
                
                startGameButton = buttonComponent;
                Debug.Log("Created StartGameButton programmatically");
            }
            
            // Create ExitButton if it doesn't exist
            if (exitButton == null)
            {
                GameObject button = new GameObject("ExitButton");
                button.transform.SetParent(mainMenuPanel.transform, false);
                
                // Add RectTransform
                RectTransform rectTransform = button.AddComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.sizeDelta = new Vector2(200, 50);
                rectTransform.anchoredPosition = new Vector2(0, -50);
                
                // Add Image component
                UnityEngine.UI.Image image = button.AddComponent<UnityEngine.UI.Image>();
                image.color = Color.red;
                
                // Add Button component
                UnityEngine.UI.Button buttonComponent = button.AddComponent<UnityEngine.UI.Button>();
                
                // Add Text
                GameObject text = new GameObject("Text");
                text.transform.SetParent(button.transform, false);
                
                RectTransform textRect = text.AddComponent<RectTransform>();
                textRect.anchorMin = Vector2.zero;
                textRect.anchorMax = Vector2.one;
                textRect.sizeDelta = Vector2.zero;
                textRect.anchoredPosition = Vector2.zero;
                
                UnityEngine.UI.Text textComponent = text.AddComponent<UnityEngine.UI.Text>();
                textComponent.text = "STOP";
                textComponent.fontSize = 24;
                textComponent.color = Color.black;
                textComponent.alignment = TextAnchor.MiddleCenter;
                
                exitButton = buttonComponent;
                Debug.Log("Created ExitButton programmatically");
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
