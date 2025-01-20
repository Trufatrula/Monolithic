using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(QuitarPadre(collision.transform));
        }
    }

    private IEnumerator QuitarPadre(Transform player)
    {
        yield return null;
        player.parent = null;
    }

}
