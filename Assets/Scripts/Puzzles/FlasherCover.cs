using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasherCover : MonoBehaviour
{
    [SerializeField] Flasher flasher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        { 
            flasher.SetCubierto(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flasher.SetCubierto(false);
        }
    }
}
