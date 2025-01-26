using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinijuegoPalos : MonoBehaviour
{
    [SerializeField] private Animator animatorTapa;
    [SerializeField] List<SocketsTeclas> socketsJuego;
    [SerializeField] SocketsTeclas socketPrincipal;
    [SerializeField] Animator animatorBarrera;

    [SerializeField] SocketsTeclas socketAvanzar1;
    [SerializeField] SocketsTeclas socketAvanzar2;

    private int puntosPartida;
    [SerializeField] private TextMeshProUGUI puntuacion;
    [SerializeField] private int puntosVictoria;

    public void IniciarJuego()
    {
        Debug.Log("Que emieze");
        animatorTapa.SetTrigger("Abrir");
        socketPrincipal.SetBloqued(true);
        puntuacion.text = puntosVictoria.ToString();
        puntosPartida = puntosVictoria;

        foreach (SocketsTeclas socket in socketsJuego)
        {
            socket.SetBloqued(false);
            socket.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void Puntuar()
    {
        puntosPartida--;
        if (puntosPartida == 0)
        {
            FinalizarMinijuego();
        }
    }

    public void FinalizarMinijuego()
    {
        Debug.Log("Que termien");

        animatorTapa.SetTrigger("Cerrar");
        animatorBarrera.SetTrigger("Completado");

        socketAvanzar1.SetBloqued(false);
        socketAvanzar1.GetComponent<SpriteRenderer>().enabled = true;
        socketAvanzar2.SetBloqued(false);
        socketAvanzar2.GetComponent<SpriteRenderer>().enabled = true;

        foreach (SocketsTeclas socket in socketsJuego)
        {
            socket.RemoveAndReturnTecla();
            socket.SetBloqued(true);
            socket.gameObject.SetActive(false);
        }
    }
}
