using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTimerMinigame : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float maxLeftDistance = 3f;

    private Vector3 originalPosition;
    private bool isMovingLeft = false;


    [SerializeField] private MinijuegoBarraRotar minijuego;
    [SerializeField] private SocketsTeclas socket;
    private bool isActiveM = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
            MoveLeft();
        }
        else
        {
            isMovingLeft = false;
            ReturnToOriginalPosition();
        }
    }

    void MoveLeft()
    {
        Vector3 targetPosition = originalPosition + Vector3.left * maxLeftDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            ActivateMinigame();
        }
    }

    void ActivateMinigame()
    {
        isActiveM = true;
        minijuego.SetTimer(this, originalPosition);
        minijuego.IniciarJuego();
        socket.SetBloqued(true);
        socket.GetComponent<SpriteRenderer>().enabled = false;
        socket.RemoveAndReturnTecla();
    }

    public void FinishMinigame()
    {
        isActiveM = false;
        //minijuego.IniciarJuego();
        socket.SetBloqued(false);
        socket.GetComponent<SpriteRenderer>().enabled = true;
    }


    void ReturnToOriginalPosition()
    {
        if (!isMovingLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
    }
}