using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRightArrow : MonoBehaviour
{
    private PlayerMovementHandler movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovementHandler>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.MoveRight(1f);
            return;
        }
        movement.MoveRight(0f);
    }

    private void OnDestroy()
    {
        movement.MoveRight(0f);
    }
}