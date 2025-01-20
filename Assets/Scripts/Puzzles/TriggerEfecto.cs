using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEfecto : MonoBehaviour
{
    [SerializeField] private string tagg;
    [SerializeField] private UnityEvent evento;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagg))
        {
            collision.tag = "Untagged";
            evento.Invoke();
            Destroy(gameObject);
        }
    }
}
