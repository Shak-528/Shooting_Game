using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer activeSprite;

    public float initialSpeed = 5.0f; // Set your desired initial speed here
    public float speedIncreaseRate = 0.1f; // Set your desired speed increase rate here
    public float maxSpeed = 10.0f; // Set your desired maximum speed here
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = transform.right; // Default direction is right

        // Get the active SpriteRenderer
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.enabled)
            {
                activeSprite = sprite;
                break;
            }
        }

        // Check if the sprite is flipped along the X-axis
        if (activeSprite != null && activeSprite.flipX)
        {
            moveDirection = -transform.right; // If flipped, we reverse the direction
        }

        // Increase the speed, but don't let it exceed the maximum speed
        currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedIncreaseRate * Time.deltaTime);

        // Move the object in the direction it's facing
        rb.velocity = moveDirection * currentSpeed;
    }
}
