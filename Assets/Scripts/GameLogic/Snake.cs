using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.GameLogic
{
    /// <summary>
    /// Represents the snake entity with segment management, movement, and growth mechanics.
    /// Handles self-collision detection and direction management.
    /// </summary>
    public class Snake
    {
        private readonly List<Vector2Int> segments;
        private Direction currentDirection;
        private Direction nextDirection;
        private bool growFlag;
        
        public Snake(int startX, int startY, Direction initialDirection = Direction.RIGHT)
        {
            segments = new List<Vector2Int>
            {
                new Vector2Int(startX, startY)
            };
            currentDirection = initialDirection;
            nextDirection = initialDirection;
            growFlag = false;
        }
        
        public List<Vector2Int> GetSegments() => new List<Vector2Int>(segments);
        public Vector2Int GetHeadPosition() => segments[0];
        public Direction GetCurrentDirection() => currentDirection;
        public int GetLength() => segments.Count;
        public bool ShouldGrow() => growFlag;
        
        public void SetDirection(Direction newDirection)
        {
            // Prevent 180-degree reversal
            if (!IsOppositeDirection(currentDirection, newDirection))
            {
                nextDirection = newDirection;
            }
        }
        
        public void Move()
        {
            currentDirection = nextDirection;
            Vector2Int headPosition = GetHeadPosition();
            Vector2Int newHeadPosition = GetNextPosition(headPosition, currentDirection);
            
            segments.Insert(0, newHeadPosition);
            
            if (growFlag)
            {
                growFlag = false;
            }
            else
            {
                segments.RemoveAt(segments.Count - 1);
            }
        }
        
        public void SetGrowFlag(bool shouldGrow)
        {
            growFlag = shouldGrow;
        }
        
        public bool CheckSelfCollision()
        {
            Vector2Int headPosition = GetHeadPosition();
            
            // Check if head collides with any body segment
            for (int i = 1; i < segments.Count; i++)
            {
                if (segments[i] == headPosition)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private Vector2Int GetNextPosition(Vector2Int currentPosition, Direction direction)
        {
            return direction switch
            {
                Direction.UP => new Vector2Int(currentPosition.x, currentPosition.y + 1),
                Direction.DOWN => new Vector2Int(currentPosition.x, currentPosition.y - 1),
                Direction.LEFT => new Vector2Int(currentPosition.x - 1, currentPosition.y),
                Direction.RIGHT => new Vector2Int(currentPosition.x + 1, currentPosition.y),
                _ => currentPosition
            };
        }
        
        private bool IsOppositeDirection(Direction current, Direction newDirection)
        {
            return (current == Direction.UP && newDirection == Direction.DOWN) ||
                   (current == Direction.DOWN && newDirection == Direction.UP) ||
                   (current == Direction.LEFT && newDirection == Direction.RIGHT) ||
                   (current == Direction.RIGHT && newDirection == Direction.LEFT);
        }
        
        public List<Vector2Int> GetBodySegments()
        {
            return segments.Count > 1 ? segments.GetRange(1, segments.Count - 1) : new List<Vector2Int>();
        }
        
        public bool IsPositionOccupied(Vector2Int position)
        {
            return segments.Contains(position);
        }
    }
    
    /// <summary>
    /// Enumeration for movement directions.
    /// </summary>
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
