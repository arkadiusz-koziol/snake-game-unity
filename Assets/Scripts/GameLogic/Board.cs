using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.GameLogic
{
    /// <summary>
    /// Represents the game board with grid management and apple placement.
    /// Handles collision detection for walls and manages apple spawning.
    /// </summary>
    public class Board
    {
        private readonly int width;
        private readonly int height;
        private readonly HashSet<Vector2Int> applePositions;
        private readonly System.Random random;
        
        public Board(int width, int height, int? seed = null)
        {
            this.width = width;
            this.height = height;
            this.applePositions = new HashSet<Vector2Int>();
            this.random = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
            
            // Spawn initial apple
            SpawnApple();
        }
        
        public int GetWidth() => width;
        public int GetHeight() => height;
        
        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
        
        public bool IsValidPosition(Vector2Int position)
        {
            return IsValidPosition(position.x, position.y);
        }
        
        public bool HasAppleAt(int x, int y)
        {
            return applePositions.Contains(new Vector2Int(x, y));
        }
        
        public bool HasAppleAt(Vector2Int position)
        {
            return HasAppleAt(position.x, position.y);
        }
        
        public void RemoveApple(int x, int y)
        {
            applePositions.Remove(new Vector2Int(x, y));
        }
        
        public void RemoveApple(Vector2Int position)
        {
            RemoveApple(position.x, position.y);
        }
        
        public Vector2Int SpawnApple()
        {
            List<Vector2Int> freePositions = GetFreePositions();
            
            if (freePositions.Count == 0)
            {
                // No free positions available
                return Vector2Int.zero;
            }
            
            Vector2Int applePosition = freePositions[random.Next(freePositions.Count)];
            applePositions.Add(applePosition);
            
            return applePosition;
        }
        
        private List<Vector2Int> GetFreePositions()
        {
            List<Vector2Int> freePositions = new List<Vector2Int>();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    if (!applePositions.Contains(position))
                    {
                        freePositions.Add(position);
                    }
                }
            }
            
            return freePositions;
        }
        
        public HashSet<Vector2Int> GetApplePositions()
        {
            return new HashSet<Vector2Int>(applePositions);
        }
        
        public bool IsPositionFree(Vector2Int position)
        {
            return IsValidPosition(position) && !applePositions.Contains(position);
        }
        
        public int GetFreePositionCount()
        {
            return (width * height) - applePositions.Count;
        }
    }
}
