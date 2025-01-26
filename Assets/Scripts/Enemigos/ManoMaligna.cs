using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoMaligna : MonoBehaviour
{
    [SerializeField] private GameObject punto1;
    [SerializeField] private GameObject punto2;
    [SerializeField] private GameObject puntoFinal;
    [SerializeField] private GameObject puntoMover;
    [SerializeField] private Transform respawnPlayer;
    [SerializeField] private InvocarBoss invocacion;

    [SerializeField] private int vida;
    [SerializeField] private float vel = 4f;
    [SerializeField] private int incrementoVel;
    private bool bossMuerto = false;

    private GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool patrulla;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        patrulla = true;
        vida = 4;
        vel = 4f;
        StartCoroutine(Patrullando());
    }

    private IEnumerator Patrullando()
    {
        Vector3 target = punto2.transform.position;

        while (patrulla)
        {
            puntoMover.transform.position = Vector3.MoveTowards(puntoMover.transform.position, target, vel * Time.deltaTime);

            if (puntoMover.transform.position == target)
            {
                target = (target == punto1.transform.position) ? punto2.transform.position : punto1.transform.position;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            patrulla = false;
            animator.enabled = true;
            animator.SetBool("Atacar", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Te agarro");
            gameManager.RespawnPlayer(respawnPlayer);
            invocacion.RespawnearBoss();
            vida = 4;
            vel = 4f;
        }
        else if (collision.gameObject.CompareTag("CajaAtaque"))
        {
            collision.gameObject.tag = "CajaUsada";
            vida--;
            vel = vel + incrementoVel;
            invocacion.PerderVida();
            StartCoroutine(Sufrir());
            if(vida == 0)
            {
                bossMuerto = true;
                StartCoroutine(Derrotado());
            }
        }
    }

    private IEnumerator Sufrir()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator Derrotado()
    {
        patrulla = false;
        Vector3 target = puntoFinal.transform.position;

        while (true)
        {
            puntoMover.transform.position = Vector3.MoveTowards(puntoMover.transform.position, target, vel * Time.deltaTime);
            if (puntoMover.transform.position == target)
            {
                break;
            }
            yield return null;
        }
        invocacion.FinalizarBoss();
        gameObject.SetActive(false);
    }

    private void StartPatrulla()
    {
        if (bossMuerto) { return; }
        animator.SetBool("Atacar", false);
        animator.enabled = false;
        patrulla = true;
        StartCoroutine(Patrullando());
    }
}