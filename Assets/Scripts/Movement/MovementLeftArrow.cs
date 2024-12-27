using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLeftArrow : MonoBehaviour
{
    public float speed = 5f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
