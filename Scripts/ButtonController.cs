using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Snake snake;

    public void SetDirectionUp()
    {
        snake.SetDirectionUp();
    }

    public void SetDirectionDown()
    {
        snake.SetDirectionDown();
    }

    public void SetDirectionLeft()
    {
        snake.SetDirectionLeft();
    }

    public void SetDirectionRight()
    {
        snake.SetDirectionRight();
    }
}
