using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private string entrance;
    private GameObject playerInstance;
    private TeclasManager teclasManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        List<DirectionData> listaFlechas = new List<DirectionData>
        {
            new DirectionData { direction = "Right", angle = 0.9998f },
            new DirectionData { direction = "Left", angle = 0.7777f },
            new DirectionData { direction = "Right", angle = 0.2222f }
        };
        teclasManager = TeclasManager.Instance;
        teclasManager.CargarTeclas(listaFlechas);
    }

    public void InstantiatePlayer(Transform spawn)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Player");

        playerInstance = Instantiate(playerPrefab, spawn.position, Quaternion.identity);
    }

    private void AddComponentToPlayer(string componentName)
    {
        componentName = componentName + "Component";
        Type componentType = Type.GetType(componentName + ", UnityEngine");

        if (componentType != null && playerInstance != null)
        {
            playerInstance.AddComponent(componentType);
        }
        else
        {
            Debug.LogError(componentName);
        }
    }

    public void UpdateAllComponents()
    {
        foreach(KeyValuePair<string, int> teclas in teclasManager.GetAllTeclas())
        {
            AddComponentToPlayer(teclas.Key);
        }
    }

    public void LoadNormalScene(string scene, string entrance)
    {
        this.entrance = entrance;
        Debug.Log(entrance);
        SceneManager.LoadScene(scene);
    }

    public string GetEntrance()
    {
        return entrance;
    }
}
