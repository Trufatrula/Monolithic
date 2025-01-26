using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversacion : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject convo;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void ComenzarConversacion()
    {
        gameManager.SetPlayerCanMove(false);
        convo.SetActive(true);
    }

    public void EndConversacion()
    {
        gameManager.SetPlayerCanMove(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ComenzarConversacion(); 
        }
    }

}
