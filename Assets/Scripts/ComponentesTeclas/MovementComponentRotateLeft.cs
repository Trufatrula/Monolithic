using UnityEngine;

public class MovementComponentRotateLeft : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}