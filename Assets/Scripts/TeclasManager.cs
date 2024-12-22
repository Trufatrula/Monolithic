using System.Collections.Generic;
using UnityEngine;

public class TeclasManager : MonoBehaviour
{
    public static TeclasManager Instance { get; private set; }

    private List<TeclaFlecha> flechas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            flechas = new List<TeclaFlecha>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConseguirFlecha(TeclaFlecha flecha)
    {
        flechas.Add(flecha);

        Transform arrowPlace = null;

        switch(flecha.GetDirection()) 
        {
            case "Up":
                arrowPlace = GameObject.Find("Arriba").transform;
                break;
            case "Down":
                arrowPlace = GameObject.Find("Abajo").transform;
                break;
            case "Left":
                arrowPlace = GameObject.Find("Izquierda").transform;
                break;
            case "Right":
                arrowPlace = GameObject.Find("Derecha").transform;
                break;
            default:
                break;
        }

        flecha.SetTargetTransform(arrowPlace);
    }
}
