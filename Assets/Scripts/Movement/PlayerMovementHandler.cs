using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float horizontalInput = 0f;

    private float moveLeft;
    private float moveRight;

    private bool canMove = true;
    private bool isGrounded = true;
    private bool isRefreshed = false;
    private bool canRefresh = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Color defaultColor;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        defaultColor = sprite.color;
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            horizontalInput = moveLeft + moveRight;
            MovePlayer();
        }

        CheckGrounded();
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

    public void Jump()
    {
        if (isGrounded && !isRefreshed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            sprite.color = Color.white;
        }
        else if(!isGrounded && isRefreshed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.25f);
            isGrounded = false;
            isRefreshed = false;
            sprite.color = Color.white;
        }
    }

    public void JumpSpace()
    {
        if (!canRefresh) { return; }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        sprite.color = defaultColor;
        isRefreshed = true;
        canRefresh = false;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if(isGrounded) 
        { 
            canRefresh = true;
            sprite.color = defaultColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}

