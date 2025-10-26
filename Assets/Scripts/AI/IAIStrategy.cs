using UnityEngine;

namespace SnakeGame.AI
{
    /// <summary>
    /// Interface for AI strategy implementations using the Strategy pattern.
    /// Enables pluggable AI implementations for different difficulty levels.
    /// </summary>
    public interface IAIStrategy
    {
        /// <summary>
        /// Gets the next move direction based on the current game state.
        /// </summary>
        /// <param name="gameState">Current game state</param>
        /// <returns>Next direction to move</returns>
        GameLogic.Direction GetNextMove(GameLogic.GameState gameState);
        
        /// <summary>
        /// Checks if the AI can analyze the current game state.
        /// </summary>
        /// <param name="gameState">Current game state</param>
        /// <returns>True if AI can analyze, false otherwise</returns>
        bool CanAnalyze(GameLogic.GameState gameState);
        
        /// <summary>
        /// Gets the confidence level of the AI's decision (0.0 to 1.0).
        /// </summary>
        /// <param name="gameState">Current game state</param>
        /// <returns>Confidence level</returns>
        float GetConfidence(GameLogic.GameState gameState);
        
        /// <summary>
        /// Gets the name of the AI strategy.
        /// </summary>
        /// <returns>Strategy name</returns>
        string GetStrategyName();
    }
}
