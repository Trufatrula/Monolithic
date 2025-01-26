using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivarSpace : MonoBehaviour
{
    [SerializeField] private bool canActivar = true;
    [SerializeField] private UnityEvent evento;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canActivar)
        {
            evento.Invoke();
            canActivar = false;
        }
    }
}
