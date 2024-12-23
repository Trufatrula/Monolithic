using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaEntrance : MonoBehaviour
{
    [SerializeField] private string escena;
    [SerializeField] private string escenaEntrance;
    [SerializeField] private Transform entrance;
    [SerializeField] private Transform playerSpawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadNormalScene(escena, escenaEntrance);
        }
    }

    public Transform GetEntrance()
    {
        return entrance;
    }

    public Transform GetPlayerSpawn()
    {
        return playerSpawn;
    }

}
