using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDownComponentTecla : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float maxDownDistance = 3f;
    [SerializeField] private bool doesReturn = true;
    [SerializeField] private SocketsTeclas socket;

    private Vector3 originalPosition;
    private bool isMovingDown = false;
    private bool playerCollision = false;
    private bool hasReturned = false;

    void Awake()
    {
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isMovingDown = true;
            hasReturned = false;
            if (doesReturn)
            {
                socket.Lock(true);
            }
            MoveDown();
        }
        else
        {
            isMovingDown = false;
            if (doesReturn && !playerCollision && !hasReturned)
            {

                ReturnToOriginalPosition();
            }
        }
    }

    void MoveDown()
    {
        if (playerCollision) { return; }
        Vector3 targetPosition = originalPosition + Vector3.down * maxDownDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ReturnToOriginalPosition()
    {
        if (!isMovingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
                hasReturned = true;
                socket.Lock(false);
            }

        }
    }
}