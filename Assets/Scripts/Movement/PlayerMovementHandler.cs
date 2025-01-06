using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private float horizontalInput = 0f; // Store input for movement

    private float moveLeft;
    private float moveRight;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        horizontalInput = moveLeft + moveRight;

        MovePlayer();
    }

    void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    public void MoveLeft(float moveLeft)
    {
        this.moveLeft = moveLeft;
    }

    public void MoveRight(float moveRight)
    {
        this.moveRight = moveRight;
    }

}
