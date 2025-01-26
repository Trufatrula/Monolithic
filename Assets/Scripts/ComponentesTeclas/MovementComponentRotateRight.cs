using UnityEngine;

public class MovementComponentRotateRight : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}