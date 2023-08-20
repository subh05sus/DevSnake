using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class SnakeMenu : MonoBehaviour
{
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = true;
    private int score = 0;
    // public Text scoreText; // Reference to the UI Text element
    private int sankeIdle = 0;
    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    private float updateTimer = 0f;
    public float updateInterval = 2f; // Time interval in seconds

    private void Start()
    {
        ResetState();
    }
    private void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= updateInterval)
        {
            updateTimer = 0f; // Reset the timer

            sankeIdle = Mathf.RoundToInt(Random.Range(0, 100));

            // Only allow turning up or down while moving in the x-axis
            if (direction.x != 0f)
            {
                if (sankeIdle % 2 == 0)
                {
                    input = Vector2Int.up;
                }
                else
                {
                    input = Vector2Int.down;
                }
            }
            // Only allow turning left or right while moving in the y-axis
            else if (direction.y != 0f)
            {
                if (sankeIdle % 2 == 0)
                {
                    input = Vector2Int.right;
                }
                else
                {
                    input = Vector2Int.left;
                }
            }
        }
    }


    private void FixedUpdate()
    {
        // Wait until the next update before proceeding
        if (Time.time < nextUpdate)
        {
            return;
        }

        // Set the new direction based on the input
        if (input != Vector2Int.zero)
        {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        // Set the next update time based on the speed
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        score++;

    }

    public void ResetState()
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            // ResetState();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            if (moveThroughWalls)
            {
                Traverse(other.transform);
            }
            else
            {
                // ResetState();
            }
        }
    }

    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f)
        {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        }
        else if (direction.y != 0f)
        {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }

        transform.position = position;
    }

}
