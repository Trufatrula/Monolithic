using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class MinijuegoBarraRotar : MonoBehaviour
{
    [SerializeField] private float duracion;
    [SerializeField] private int puntosVictoria;
    private int puntosPartida;
    [SerializeField] private TextMeshProUGUI puntuacion;

    [SerializeField] private BoxCollider2D spawnBolas;
    [SerializeField] private GameObject bola;
    [SerializeField] private float spawnCooldown;
    private bool isSpawning = false;

    [SerializeField] private Animator animatorTapa;
    [SerializeField] List<SocketsTeclas> socketsJuego;

    [SerializeField] GameObject marcaComplecion;

    [SerializeField] List<GameObject> canastasIzq;
    [SerializeField] GameObject canastaActiva;
    [SerializeField] List<GameObject> canastasDrc;
    private bool empiezaIzq = true;

    [SerializeField] private Animator barreraCompletado;
    [SerializeField] private SocketsTeclas socketTimer;
    private MovementTimerMinigame timer;
    private Vector3 timerOriginalPosition;

    public void IniciarJuego()
    {
        Debug.Log("Que emieze");

        animatorTapa.SetTrigger("Abrir");
        canastaActiva.GetComponent<Animator>().SetTrigger("Sacar");
        puntuacion.text = puntosVictoria.ToString();
        puntosPartida = puntosVictoria;

        foreach (SocketsTeclas socket in socketsJuego)
        {
            socket.SetBloqued(false);
            socket.GetComponent<SpriteRenderer>().enabled = true;
        }

        StartCoroutine(TimerPartida());
        StartCoroutine(SpawnerBolas());
    }

    private IEnumerator TimerPartida()
    {
        Vector3 startPosition = timer.transform.position;
        float elapsedTime = 0f;
        isSpawning = true;

        while (elapsedTime < duracion)
        {
            elapsedTime += Time.deltaTime;

            timer.transform.position = Vector3.Lerp(startPosition, timerOriginalPosition, elapsedTime / duracion);

            yield return null;
        }

        timer.transform.position = timerOriginalPosition;

        isSpawning = false;
        FinalizarMinijuego();
    }

    private IEnumerator SpawnerBolas()
    {
        yield return new WaitForSeconds(spawnCooldown);

        while (isSpawning)
        {
            Vector2 center = spawnBolas.bounds.center;
            Vector2 size = spawnBolas.bounds.size;
            float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

            Vector2 randomPosition = new Vector2(randomX, randomY);
            Instantiate(bola, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void FinalizarMinijuego()
    {
        Debug.Log("Que termien");

        animatorTapa.SetTrigger("Cerrar");

        timer.FinishMinigame();

        foreach (SocketsTeclas socket in socketsJuego)
        {
            socket.RemoveAndReturnTecla();
            socket.SetBloqued(true);
            socket.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Puntuar(int puntos)
    {
        puntosPartida -= puntos;
        puntuacion.text = puntosPartida.ToString();
        if (puntosPartida <= 0)
        {
            puntuacion.text = "0";
            StopAllCoroutines();
            FinalizarMinijuego();
            barreraCompletado.SetTrigger("Completado");
            socketTimer.SetBloqued(true);
            socketTimer.GetComponent<SpriteRenderer>().enabled = false;
            marcaComplecion.SetActive(true);

        }
        else
        {
            if(empiezaIzq)
            {
                canastaActiva.GetComponent<Animator>().SetTrigger("Meter");
                int indexLista = Random.Range(0, canastasDrc.Count);
                Debug.Log(indexLista);
                canastaActiva = canastasDrc[indexLista];
                canastaActiva.GetComponent<Animator>().SetTrigger("SacarDrc");
                Debug.Log("TocaDerecha");
                empiezaIzq = false;
            }
            else
            {
                canastaActiva.GetComponent<Animator>().SetTrigger("Meter");
                int indexLista = Random.Range(0, canastasIzq.Count);
                Debug.Log(indexLista);
                canastaActiva = canastasIzq[indexLista];
                canastaActiva.GetComponent<Animator>().SetTrigger("Sacar");
                Debug.Log("TocaIzquiersd");
                empiezaIzq = true;
            }
        }
    }

    public void SetTimer(MovementTimerMinigame timer, Vector3 timerOriginalPosition)
    {
        this.timer = timer;
        this.timerOriginalPosition = timerOriginalPosition;
    }
}
