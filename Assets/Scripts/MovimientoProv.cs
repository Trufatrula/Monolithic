using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoProv : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    [SerializeField] private float vel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(inputH * vel, rb.velocity.y);
    }
}
