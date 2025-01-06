using UnityEngine;

public class MovementLeftComponentTecla : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float maxLeftDistance = 3f;
    [SerializeField] private bool doesReturn = true;

    private Vector3 originalPosition;
    private bool isMovingLeft = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
            MoveLeft();
        }
        else
        {
            isMovingLeft = false;

            if (doesReturn)
            {
                ReturnToOriginalPosition();
            }
        }
    }

    void MoveLeft()
    {
        Vector3 targetPosition = originalPosition + Vector3.left * maxLeftDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ReturnToOriginalPosition()
    {
        if (!isMovingLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
    }
}