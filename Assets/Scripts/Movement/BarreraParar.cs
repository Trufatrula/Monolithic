using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraParar : MonoBehaviour
{
    [SerializeField] MovementUpComponentTecla movUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movUp.SetPlayerCollision(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movUp.SetPlayerCollision(false);
        }
    }
}
