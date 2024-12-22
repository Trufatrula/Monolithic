using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaCamara : MonoBehaviour
{
    [SerializeField] Transform puntoIzq;
    [SerializeField] Transform puntoDrc;
    [SerializeField] CinemachineVirtualCamera camara;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 exitingObjectPosition = other.transform.position;
            Vector2 triggerCenterPosition = transform.position;

            if (exitingObjectPosition.x < triggerCenterPosition.x)
            {
                camara.Follow = puntoIzq;
            }
            else
            {
                camara.Follow = puntoDrc;
            }
        }
    }
}