using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaEntrance : MonoBehaviour
{
    [SerializeField] private string escena;
    [SerializeField] private string escenaEntrance;
    [SerializeField] private Transform entrance;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private int transitionAnimation = 0;

    private SceneController sceneController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.ExitScene(escena, escenaEntrance, transitionAnimation);
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

    public int GetTransitionAnimation()
    {
        return transitionAnimation;
    }

    public void SetSceneController(SceneController sceneController)
    {
        this.sceneController = sceneController; 
    }
}
