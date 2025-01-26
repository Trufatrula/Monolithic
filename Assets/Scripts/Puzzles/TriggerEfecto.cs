using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEfecto : MonoBehaviour
{
    [SerializeField] private string tagActivar;
    [SerializeField] private UnityEvent evento;
    [SerializeField] private bool oneTime = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagActivar))
        {
            evento.Invoke();
            if(oneTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
