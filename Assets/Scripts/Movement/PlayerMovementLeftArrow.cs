using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLeftArrow : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }
}