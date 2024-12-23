using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private string entrance;
 
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
        List<DirectionData> listaFlechas = new List<DirectionData>();
        listaFlechas.Add(new DirectionData { direction = "Right", angle = 0.9998f });
        listaFlechas.Add(new DirectionData { direction = "Left", angle = 0.7777f });
        listaFlechas.Add(new DirectionData { direction = "Right", angle = 0.2222f });
        TeclasManager.Instance.CargarTeclas(listaFlechas);
    }

    public void InstantiatePlayer(Transform spawn)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Player");

        Instantiate(playerPrefab, spawn.position, Quaternion.identity);
    }

    public void LoadNormalScene(string scene, string entrance)
    {
        this.entrance = entrance;
        Debug.Log(entrance);
        SceneManager.LoadScene(scene);
    }

    public void GiveMovementArrow(string direction)
    {
        Debug.Log(direction);
    }



    public string GetEntrance()
    {
        return entrance;
    }
}
