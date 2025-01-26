using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DevolverCajas : MonoBehaviour
{
    [SerializeField] private Transform puntoRespawnIzq;
    [SerializeField] private Transform puntoRespawnDrc;
    [SerializeField] private float tiempoRespawn = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CajaUsada") || collision.CompareTag("CajaAtaque"))
        {
            StartCoroutine(DevolverCajaAlSpawn(collision.gameObject));
        }   
    }

    private IEnumerator DevolverCajaAlSpawn(GameObject caja)
    {
        yield return new WaitForSeconds(tiempoRespawn);
        caja.tag = "CajaAtaque";
        caja.GetComponent<Rigidbody2D>().velocity = Vector3.zero;   
        if(caja.name.Contains("Izq"))
        {
            caja.transform.position = puntoRespawnIzq.position;
        }
        else
        {
            caja.transform.position = puntoRespawnDrc.position;
        }
    }
}
