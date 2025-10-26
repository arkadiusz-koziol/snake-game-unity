using UnityEngine;

namespace SnakeGame.Configuration
{
    /// <summary>
    /// Configuration data class for game settings.
    /// Contains all configurable game parameters.
    /// </summary>
    [System.Serializable]
    public class GameConfig
    {
        [Header("Board Settings")]
        public int boardWidth = 20;
        public int boardHeight = 15;
        
        [Header("Game Settings")]
        public DifficultyLevel difficulty = DifficultyLevel.NORMAL;
        public bool enableUndo = true;
        public bool enableAI = false;
        public int? gameSeed = null;
        
        [Header("Input Settings")]
        public bool enableInputDebouncing = true;
        public float inputDebounceTime = 0.1f;
        
        [Header("Rendering Settings")]
        public float cellSize = 1.0f;
        public Vector2 boardOffset = Vector2.zero;
        
        [Header("Audio Settings")]
        public bool enableSoundEffects = true;
        public float masterVolume = 1.0f;
        
        public GameConfig()
        {
            // Default constructor with sensible defaults
        }
        
        public GameConfig(int width, int height, DifficultyLevel difficulty)
        {
            this.boardWidth = width;
            this.boardHeight = height;
            this.difficulty = difficulty;
        }
        
        public float GetTickRate()
        {
            return difficulty switch
            {
                DifficultyLevel.EASY => 0.2f,    // 200ms = 5 FPS
                DifficultyLevel.NORMAL => 0.125f, // 125ms = 8 FPS
                DifficultyLevel.HARD => 0.08f,    // 80ms = 12.5 FPS
                _ => 0.125f
            };
        }
        
        public void Validate()
        {
            boardWidth = Mathf.Clamp(boardWidth, 10, 50);
            boardHeight = Mathf.Clamp(boardHeight, 10, 30);
            inputDebounceTime = Mathf.Clamp(inputDebounceTime, 0.01f, 0.5f);
            cellSize = Mathf.Clamp(cellSize, 0.1f, 2.0f);
            masterVolume = Mathf.Clamp01(masterVolume);
        }
    }
    
    /// <summary>
    /// Enumeration for difficulty levels.
    /// </summary>
    public enum DifficultyLevel
    {
        EASY,
        NORMAL,
        HARD
    }
}
