using UnityEngine;
using System.Collections.Generic;
using SnakeGame.GameLogic;
using SnakeGame.Events;

namespace SnakeGame.UnityComponents
{
    /// <summary>
    /// Unity component that handles rendering of the snake game.
    /// Manages sprite rendering for snake segments, apples, and board elements.
    /// </summary>
    public class SnakeRenderer : MonoBehaviour
    {
        [Header("Rendering Settings")]
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private Vector2 boardOffset = Vector2.zero;
        
        [Header("Sprites")]
        [SerializeField] private Sprite snakeHeadSprite;
        [SerializeField] private Sprite snakeBodySprite;
        [SerializeField] private Sprite appleSprite;
        [SerializeField] private Sprite wallSprite;
        
        [Header("Colors")]
        [SerializeField] private Color snakeHeadColor = Color.green;
        [SerializeField] private Color snakeBodyColor = Color.green;
        [SerializeField] private Color appleColor = Color.red;
        [SerializeField] private Color wallColor = Color.gray;
        
        [Header("Game References")]
        [SerializeField] private GameSnake gameSnake;
        
        private GameObject[,] cellObjects;
        private GameObject snakeHeadObject;
        private GameObject[] snakeBodyObjects;
        private GameObject[] appleObjects;
        
        private void Start()
        {
            Debug.Log("SnakeRenderer: Start() called");
            
            // Don't initialize rendering yet - wait for gameSnake to be set
            Debug.Log("SnakeRenderer: Waiting for gameSnake to be set by GameManager");
        }
        
        private void SetupEventListeners()
        {
            if (gameSnake?.GetEventBus() != null)
            {
                EventBus eventBus = gameSnake.GetEventBus();
                
                eventBus.Subscribe<TickEvent>(OnTick);
                eventBus.Subscribe<AppleSpawnedEvent>(OnAppleSpawned);
                eventBus.Subscribe<AppleEatenEvent>(OnAppleEaten);
                eventBus.Subscribe<CollisionEvent>(OnCollision);
                eventBus.Subscribe<GameOverEvent>(OnGameOver);
                eventBus.Subscribe<GameRestartedEvent>(OnGameRestarted);
            }
        }
        
        private void InitializeRendering()
        {
            Debug.Log("SnakeRenderer: Starting initialization");
            
            if (gameSnake == null) 
            {
                Debug.LogError("SnakeRenderer: gameSnake is null!");
                return;
            }
            
            GameState currentState = gameSnake.GetCurrentState();
            int width = currentState.Board.GetWidth();
            int height = currentState.Board.GetHeight();
            
            Debug.Log($"SnakeRenderer: Board size {width}x{height}");
            
            cellObjects = new GameObject[width, height];
            
            CreateWalls(width, height);
            
            InitializeSnakeRendering();
            
            InitializeAppleRendering(currentState);
            
            Debug.Log("SnakeRenderer: Initialization complete");
        }
        
        private void CreateWalls(int width, int height)
        {
            Debug.Log($"SnakeRenderer: Creating walls for {width}x{height} board");
            int wallCount = 0;
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        CreateWallSprite(x, y);
                        wallCount++;
                    }
                }
            }
            
            Debug.Log($"SnakeRenderer: Created {wallCount} wall sprites");
        }
        
        private void CreateWallSprite(int x, int y)
        {
            GameObject wallObject = new GameObject($"Wall_{x}_{y}");
            wallObject.transform.SetParent(transform);
            wallObject.transform.position = GetWorldPosition(x, y);
            
            SpriteRenderer spriteRenderer = wallObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = wallSprite;
            spriteRenderer.color = wallColor;
            spriteRenderer.sortingOrder = 0;
            
            cellObjects[x, y] = wallObject;
            
            Debug.Log($"SnakeRenderer: Created wall at ({x},{y}) with sprite: {(wallSprite != null ? "OK" : "NULL")}");
        }
        
        private void InitializeSnakeRendering()
        {
            if (gameSnake == null) return;
            
            GameState currentState = gameSnake.GetCurrentState();
            List<Vector2Int> segments = currentState.Snake.GetSegments();
            
            snakeBodyObjects = new GameObject[segments.Count];
            
            for (int i = 0; i < segments.Count; i++)
            {
                Vector2Int segment = segments[i];
                GameObject segmentObject = CreateSnakeSegment(segment, i == 0);
                snakeBodyObjects[i] = segmentObject;
            }
        }
        
        private GameObject CreateSnakeSegment(Vector2Int position, bool isHead)
        {
            GameObject segmentObject = new GameObject($"SnakeSegment_{position.x}_{position.y}");
            segmentObject.transform.SetParent(transform);
            segmentObject.transform.position = GetWorldPosition(position.x, position.y);
            
            SpriteRenderer spriteRenderer = segmentObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = isHead ? snakeHeadSprite : snakeBodySprite;
            spriteRenderer.color = isHead ? snakeHeadColor : snakeBodyColor;
            spriteRenderer.sortingOrder = 2;
            
            return segmentObject;
        }
        
        private void InitializeAppleRendering(GameState gameState)
        {
            HashSet<Vector2Int> applePositions = gameState.Board.GetApplePositions();
            appleObjects = new GameObject[applePositions.Count];
            
            int index = 0;
            foreach (Vector2Int applePosition in applePositions)
            {
                GameObject appleObject = CreateAppleSprite(applePosition);
                appleObjects[index] = appleObject;
                index++;
            }
        }
        
        private GameObject CreateAppleSprite(Vector2Int position)
        {
            GameObject appleObject = new GameObject($"Apple_{position.x}_{position.y}");
            appleObject.transform.SetParent(transform);
            appleObject.transform.position = GetWorldPosition(position.x, position.y);
            
            SpriteRenderer spriteRenderer = appleObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = appleSprite;
            spriteRenderer.color = appleColor;
            spriteRenderer.sortingOrder = 1;
            
            return appleObject;
        }
        
        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(
                x * cellSize + boardOffset.x,
                y * cellSize + boardOffset.y,
                0
            );
        }
        
        private void OnTick(TickEvent tickEvent)
        {
            UpdateSnakeRendering(tickEvent.GameState);
        }
        
        private void OnAppleSpawned(AppleSpawnedEvent appleEvent)
        {
            // Add new apple to rendering
            GameObject newApple = CreateAppleSprite(appleEvent.Position);
            
            // Expand apple objects array
            GameObject[] newAppleObjects = new GameObject[appleObjects.Length + 1];
            appleObjects.CopyTo(newAppleObjects, 0);
            newAppleObjects[appleObjects.Length] = newApple;
            appleObjects = newAppleObjects;
        }
        
        private void OnAppleEaten(AppleEatenEvent appleEvent)
        {
            // Remove eaten apple from rendering
            for (int i = 0; i < appleObjects.Length; i++)
            {
                if (appleObjects[i] != null)
                {
                    Vector3 worldPos = GetWorldPosition(appleEvent.Position.x, appleEvent.Position.y);
                    if (Vector3.Distance(appleObjects[i].transform.position, worldPos) < 0.1f)
                    {
                        Destroy(appleObjects[i]);
                        appleObjects[i] = null;
                        break;
                    }
                }
            }
        }
        
        private void OnCollision(CollisionEvent collisionEvent)
        {
            // Add visual effects for collision
            StartCoroutine(PlayCollisionEffects(collisionEvent.Position));
        }
        
        private System.Collections.IEnumerator PlayCollisionEffects(Vector2Int collisionPosition)
        {
            // Flash effect at collision point
            Vector3 worldPos = GetWorldPosition(collisionPosition.x, collisionPosition.y);
            
            // Create a temporary flash object
            GameObject flashObject = new GameObject("CollisionFlash");
            flashObject.transform.position = worldPos;
            
            SpriteRenderer flashRenderer = flashObject.AddComponent<SpriteRenderer>();
            flashRenderer.sprite = snakeHeadSprite; // Use snake head sprite as flash
            flashRenderer.color = Color.white;
            flashRenderer.sortingOrder = 10;
            
            // Flash animation
            float flashDuration = 0.2f;
            float elapsed = 0f;
            
            while (elapsed < flashDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
                Color flashColor = Color.white;
                flashColor.a = alpha;
                flashRenderer.color = flashColor;
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            Destroy(flashObject);
        }
        
        private void OnGameOver(GameOverEvent gameOverEvent)
        {
            // Add visual effects for game over
            StartCoroutine(PlayGameOverEffects());
        }
        
        private System.Collections.IEnumerator PlayGameOverEffects()
        {
            // Screen shake effect
            Vector3 originalPosition = Camera.main.transform.position;
            float shakeIntensity = 0.5f;
            float shakeDuration = 0.5f;
            float elapsed = 0f;
            
            while (elapsed < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeIntensity;
                float y = Random.Range(-1f, 1f) * shakeIntensity;
                Camera.main.transform.position = originalPosition + new Vector3(x, y, 0);
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            Camera.main.transform.position = originalPosition;
        }
        
        private void OnGameRestarted(GameRestartedEvent restartEvent)
        {
            // Clean up existing objects
            CleanupRendering();
            
            // Reinitialize rendering
            InitializeRendering();
        }
        
        private void UpdateSnakeRendering(GameState gameState)
        {
            List<Vector2Int> segments = gameState.Snake.GetSegments();
            
            // Update snake head position
            if (snakeBodyObjects.Length > 0 && snakeBodyObjects[0] != null)
            {
                snakeBodyObjects[0].transform.position = GetWorldPosition(segments[0].x, segments[0].y);
            }
            
            // Update snake body positions
            for (int i = 1; i < segments.Count && i < snakeBodyObjects.Length; i++)
            {
                if (snakeBodyObjects[i] != null)
                {
                    snakeBodyObjects[i].transform.position = GetWorldPosition(segments[i].x, segments[i].y);
                }
            }
            
            // Handle snake growth
            if (segments.Count > snakeBodyObjects.Length)
            {
                // Snake grew, add new segment
                GameObject[] newBodyObjects = new GameObject[segments.Count];
                snakeBodyObjects.CopyTo(newBodyObjects, 0);
                
                for (int i = snakeBodyObjects.Length; i < segments.Count; i++)
                {
                    newBodyObjects[i] = CreateSnakeSegment(segments[i], false);
                }
                
                snakeBodyObjects = newBodyObjects;
            }
        }
        
        private void CleanupRendering()
        {
            // Clean up snake objects
            if (snakeBodyObjects != null)
            {
                foreach (GameObject obj in snakeBodyObjects)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
            
            // Clean up apple objects
            if (appleObjects != null)
            {
                foreach (GameObject obj in appleObjects)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
            
            // Clean up cell objects
            if (cellObjects != null)
            {
                foreach (GameObject obj in cellObjects)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
        }
        
        public void SetGameSnake(GameSnake gameSnake)
        {
            this.gameSnake = gameSnake;
            Debug.Log("SnakeRenderer: gameSnake set, initializing rendering");
            SetupEventListeners();
            InitializeRendering();
        }
    }
}
