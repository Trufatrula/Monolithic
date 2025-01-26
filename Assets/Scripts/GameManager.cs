using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerPosInicial;
    [SerializeField] private IntroJuego intro;

    private GameObject playerInstance;
    private TeclasManager teclasManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        teclasManager = TeclasManager.Instance;
        intro.StartIntro();
        //InstantiatePlayer(playerPosInicial);
        //List<DirectionData> listaFlechas = new List<DirectionData>
        //{
        //    new DirectionData { direction = "Right", angle = 0f },
        //    new DirectionData { direction = "Left", angle = 0.7777f },
        //    new DirectionData { direction = "Up", angle = 0.9998f }
        //};
        //teclasManager.CargarTeclas(listaFlechas);
    }

    public void InstantiatePlayer(Transform spawn)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Player");
        playerInstance = Instantiate(playerPrefab, spawn.position, Quaternion.identity);

        teclasManager.ReloadTeclas();
    }

    public void AddComponentToPlayer(Type component)
    {
        if(playerInstance != null)
        {
            playerInstance.AddComponent(component);
        }
        else
        {
            Debug.LogError("Player inexistente");
        }
    }

    public void RemoveComponentFromPlayer(Type component)
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance.GetComponent(component));
        }
        else
        {
            Debug.LogError("Player inexistente");
        }
    }
    
    public Transform GetPlayerCamera()
    {
        Transform playerCamPos = playerInstance.transform.Find("PuntoCamara");
        Debug.Log(playerCamPos.gameObject.name);
        return playerCamPos;
    }

    public void SetPlayerCanMove(bool canMove)
    {
        playerInstance.GetComponent<PlayerMovementHandler>().SetCanMove(canMove);
    }

    public void CambiarEscena()
    {
        teclasManager.UpdateSocketsTeclas();
    }

    public void RespawnPlayer(Transform transform)
    {
        playerInstance.transform.position = transform.position;
        SetPlayerCanMove(false);
        StartCoroutine(PantallaMuerteEsperar());
    }

    private IEnumerator PantallaMuerteEsperar()
    {
        yield return new WaitForSeconds(1.5f);

        SetPlayerCanMove(true);
    }
}
