using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRightComponentTecla : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float maxRightDistance = 3f;
    [SerializeField] private bool doesReturn = true;

    private Vector3 originalPosition;
    private bool isMovingRight = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isMovingRight = true;
            MoveRight();
        }
        else
        {
            isMovingRight = false;

            if (doesReturn)
            {
                ReturnToOriginalPosition();
            }
        }
    }

    void MoveRight()
    {
        Vector3 targetPosition = originalPosition + Vector3.right * maxRightDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ReturnToOriginalPosition()
    {
        if (!isMovingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
    }
}