using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCaerCaja : MonoBehaviour
{
    [SerializeField] private bool caer;
    [SerializeField] private float distanceCaer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzleBox"))
        {
            if(caer)
            {
                collision.GetComponent<CajaMovediza>().StartMoving(distanceCaer);
            }
            else
            {

            }

        }
    }
}
