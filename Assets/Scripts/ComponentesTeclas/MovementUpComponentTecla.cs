using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUpComponentTecla : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float maxUpDistance = 3f;
    [SerializeField] private bool doesReturn = true;
    [SerializeField] private SocketsTeclas socket;

    private Vector3 originalPosition;
    private bool isMovingUp = false;
    private bool playerCollision = false;
    private bool hasReturned = false;

    void Awake()
    {
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isMovingUp = true;
            hasReturned = false;
            if(doesReturn)
            {
                socket.Lock(true);
            }
            MoveUp();
        }
        else
        {
            isMovingUp = false;
            if (doesReturn && !playerCollision && !hasReturned)
            {

                ReturnToOriginalPosition();
            }
        }
    }

    void MoveUp()
    {
        if(playerCollision) { return; }
        Vector3 targetPosition = originalPosition + Vector3.up * maxUpDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ReturnToOriginalPosition()
    {
        if (!isMovingUp)
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

    public void SetPlayerCollision(bool collided)
    {
        playerCollision = collided;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerCollision = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerCollision = false;
    //    }
    //}
}

