using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRightArrow : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
