using UnityEngine;

public class Bird : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Vector2 currentDirection;
    private void Start() => GetRandomDirection();
    private void FixedUpdate() => transform.Translate(currentDirection * moveSpeed * Time.deltaTime);
    private void GetRandomDirection()
    {
        switch (gameObject.name)
        {
            case "blueBird": 
                currentDirection = new Vector2(1f,1f);
                break;
            case "greenBird": 
                currentDirection = new Vector2(1f, -1f);
                break;
            case "orangeBird": 
                currentDirection = new Vector2(-1f, 1f);
                break;
            case "pinkBird": 
                currentDirection = new Vector2(-1f, -1f);
                break;
        }
    }

}
