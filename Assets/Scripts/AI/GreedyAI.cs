using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame.AI
{
    public class GreedyAI : IAIStrategy
    {
        private readonly System.Random random;
        
        public GreedyAI(int? seed = null)
        {
            random = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
        }
        
        public GameLogic.Direction GetNextMove(GameLogic.GameState gameState)
        {
            if (!CanAnalyze(gameState))
            {
                return GetRandomSafeDirection(gameState);
            }
            
            Vector2Int headPosition = gameState.Snake.GetHeadPosition();
            Vector2Int nearestApple = FindNearestApple(headPosition, gameState);
            
            if (nearestApple == Vector2Int.zero)
            {
                return GetRandomSafeDirection(gameState);
            }
            
            GameLogic.Direction bestDirection = FindBestDirection(headPosition, nearestApple, gameState);
            
            return bestDirection;
        }
        
        public bool CanAnalyze(GameLogic.GameState gameState)
        {
            return gameState != null && 
                   gameState.Snake != null && 
                   gameState.Board != null &&
                   !gameState.IsGameOver() &&
                   !gameState.IsPaused();
        }
        
        public float GetConfidence(GameLogic.GameState gameState)
        {
            if (!CanAnalyze(gameState))
            {
                return 0.0f;
            }
            
            int safeMoves = GetSafeDirections(gameState).Count;
            return Mathf.Clamp01(safeMoves / 4.0f);
        }
        
        public string GetStrategyName()
        {
            return "Greedy AI";
        }
        
        private Vector2Int FindNearestApple(Vector2Int headPosition, GameLogic.GameState gameState)
        {
            HashSet<Vector2Int> applePositions = gameState.Board.GetApplePositions();
            
            if (applePositions.Count == 0)
            {
                return Vector2Int.zero;
            }
            
            Vector2Int nearestApple = Vector2Int.zero;
            float minDistance = float.MaxValue;
            
            foreach (Vector2Int applePosition in applePositions)
            {
                float distance = Vector2Int.Distance(headPosition, applePosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestApple = applePosition;
                }
            }
            
            return nearestApple;
        }
        
        private GameLogic.Direction FindBestDirection(Vector2Int headPosition, Vector2Int targetPosition, GameLogic.GameState gameState)
        {
            Vector2Int directionVector = targetPosition - headPosition;
            
            if (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y))
            {
                GameLogic.Direction horizontalDirection = directionVector.x > 0 ? GameLogic.Direction.RIGHT : GameLogic.Direction.LEFT;
                if (IsDirectionSafe(horizontalDirection, gameState))
                {
                    return horizontalDirection;
                }
            }
            else
            {
                GameLogic.Direction verticalDirection = directionVector.y > 0 ? GameLogic.Direction.UP : GameLogic.Direction.DOWN;
                if (IsDirectionSafe(verticalDirection, gameState))
                {
                    return verticalDirection;
                }
            }
            
            List<GameLogic.Direction> safeDirections = GetSafeDirections(gameState);
            if (safeDirections.Count > 0)
            {
                return safeDirections[random.Next(safeDirections.Count)];
            }
            
            return gameState.Snake.GetCurrentDirection();
        }
        
        private bool IsDirectionSafe(GameLogic.Direction direction, GameLogic.GameState gameState)
        {
            Vector2Int headPosition = gameState.Snake.GetHeadPosition();
            Vector2Int nextPosition = GetNextPosition(headPosition, direction);
            
            if (!gameState.Board.IsValidPosition(nextPosition))
            {
                return false;
            }
            
            if (gameState.Snake.IsPositionOccupied(nextPosition))
            {
                return false;
            }
            
            return true;
        }
        
        private List<GameLogic.Direction> GetSafeDirections(GameLogic.GameState gameState)
        {
            List<GameLogic.Direction> safeDirections = new List<GameLogic.Direction>();
            
            foreach (GameLogic.Direction direction in System.Enum.GetValues(typeof(GameLogic.Direction)))
            {
                if (IsDirectionSafe(direction, gameState))
                {
                    safeDirections.Add(direction);
                }
            }
            
            return safeDirections;
        }
        
        private GameLogic.Direction GetRandomSafeDirection(GameLogic.GameState gameState)
        {
            List<GameLogic.Direction> safeDirections = GetSafeDirections(gameState);
            
            if (safeDirections.Count > 0)
            {
                return safeDirections[random.Next(safeDirections.Count)];
            }
            
            return gameState.Snake.GetCurrentDirection();
        }
        
        private Vector2Int GetNextPosition(Vector2Int currentPosition, GameLogic.Direction direction)
        {
            return direction switch
            {
                GameLogic.Direction.UP => new Vector2Int(currentPosition.x, currentPosition.y + 1),
                GameLogic.Direction.DOWN => new Vector2Int(currentPosition.x, currentPosition.y - 1),
                GameLogic.Direction.LEFT => new Vector2Int(currentPosition.x - 1, currentPosition.y),
                GameLogic.Direction.RIGHT => new Vector2Int(currentPosition.x + 1, currentPosition.y),
                _ => currentPosition
            };
        }
    }
}
