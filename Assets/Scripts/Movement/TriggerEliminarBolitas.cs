using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEliminarBolitas : MonoBehaviour
{
    [SerializeField] private bool isPuntuar;
    [SerializeField] private MinijuegoBarraRotar minijuego;
    private int puntos = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BolitaMinigame"))
        {
            if(isPuntuar)
            {
                minijuego.Puntuar(puntos);
            }
            Destroy(collision.gameObject);
        }
    }
}
