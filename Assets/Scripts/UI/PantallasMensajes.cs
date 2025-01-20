using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallasMensajes : MonoBehaviour
{
    private Vector3 startingPos;
    [SerializeField] private float vel;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float width;


    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        float r = (vel * Time.time) % width;
        transform.position = startingPos + r * direction;
    }
}
