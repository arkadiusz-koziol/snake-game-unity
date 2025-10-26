using UnityEngine;

namespace SnakeGame.Configuration
{
    /// <summary>
    /// Unity ScriptableObject for game configuration.
    /// Allows visual configuration in Unity Editor and runtime loading.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Snake Game/Game Configuration")]
    public class GameConfigSO : ScriptableObject
    {
        [SerializeField] private GameConfig config = new GameConfig();
        
        public GameConfig Config
        {
            get
            {
                config.Validate();
                return config;
            }
            set => config = value;
        }
        
        private void OnValidate()
        {
            config.Validate();
        }
        
        /// <summary>
        /// Loads configuration from Unity PlayerPrefs (environment variables).
        /// </summary>
        public void LoadFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("BoardWidth"))
                config.boardWidth = PlayerPrefs.GetInt("BoardWidth");
            
            if (PlayerPrefs.HasKey("BoardHeight"))
                config.boardHeight = PlayerPrefs.GetInt("BoardHeight");
            
            if (PlayerPrefs.HasKey("Difficulty"))
                config.difficulty = (DifficultyLevel)PlayerPrefs.GetInt("Difficulty");
            
            if (PlayerPrefs.HasKey("EnableUndo"))
                config.enableUndo = PlayerPrefs.GetInt("EnableUndo") == 1;
            
            if (PlayerPrefs.HasKey("EnableAI"))
                config.enableAI = PlayerPrefs.GetInt("EnableAI") == 1;
            
            if (PlayerPrefs.HasKey("GameSeed"))
                config.gameSeed = PlayerPrefs.GetInt("GameSeed");
            
            if (PlayerPrefs.HasKey("EnableInputDebouncing"))
                config.enableInputDebouncing = PlayerPrefs.GetInt("EnableInputDebouncing") == 1;
            
            if (PlayerPrefs.HasKey("InputDebounceTime"))
                config.inputDebounceTime = PlayerPrefs.GetFloat("InputDebounceTime");
            
            if (PlayerPrefs.HasKey("CellSize"))
                config.cellSize = PlayerPrefs.GetFloat("CellSize");
            
            if (PlayerPrefs.HasKey("MasterVolume"))
                config.masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            
            config.Validate();
        }
        
        /// <summary>
        /// Saves configuration to Unity PlayerPrefs.
        /// </summary>
        public void SaveToPlayerPrefs()
        {
            PlayerPrefs.SetInt("BoardWidth", config.boardWidth);
            PlayerPrefs.SetInt("BoardHeight", config.boardHeight);
            PlayerPrefs.SetInt("Difficulty", (int)config.difficulty);
            PlayerPrefs.SetInt("EnableUndo", config.enableUndo ? 1 : 0);
            PlayerPrefs.SetInt("EnableAI", config.enableAI ? 1 : 0);
            
            if (config.gameSeed.HasValue)
                PlayerPrefs.SetInt("GameSeed", config.gameSeed.Value);
            
            PlayerPrefs.SetInt("EnableInputDebouncing", config.enableInputDebouncing ? 1 : 0);
            PlayerPrefs.SetFloat("InputDebounceTime", config.inputDebounceTime);
            PlayerPrefs.SetFloat("CellSize", config.cellSize);
            PlayerPrefs.SetFloat("MasterVolume", config.masterVolume);
            
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Resets configuration to default values.
        /// </summary>
        public void ResetToDefaults()
        {
            config = new GameConfig();
        }
    }
}
