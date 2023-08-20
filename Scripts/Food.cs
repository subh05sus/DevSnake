using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    public Sprite[] foodSprites;
    private Snake snake;

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();
    }

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        // Round the values to ensure it aligns with the grid
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));
        
        int randomSpriteIndex = Random.Range(0, foodSprites.Length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = foodSprites[randomSpriteIndex];
        }
        // Prevent the food from spawning on the snake
        while (snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                if (y > bounds.max.y)
                {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }

}
