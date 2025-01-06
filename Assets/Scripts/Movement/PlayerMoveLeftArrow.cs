using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveLeftArrow : MonoBehaviour
{
    private PlayerMovementHandler movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovementHandler>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.MoveLeft(-1f);
            return;
        }
        movement.MoveLeft(0f);
    }

    private void OnDestroy()
    {
        movement.MoveLeft(0f);
    }
}