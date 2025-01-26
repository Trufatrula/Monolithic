using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSpace : MonoBehaviour
{
    private PlayerMovementHandler movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovementHandler>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            movement.JumpSpace();
        }
    }
}