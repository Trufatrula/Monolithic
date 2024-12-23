using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeclasManager : MonoBehaviour
{
    public static TeclasManager Instance { get; private set; }

    private List<DirectionData> flechas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            flechas = new List<DirectionData>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarTeclas(List<DirectionData> savedFlechas)
    {
        flechas = savedFlechas;
        List<string> directionsRep = new List<string>();

        if(!(flechas.Count == 0))
        {
            for (int i = flechas.Count - 1; i >= 0; i--)
            {
                if (!directionsRep.Contains(flechas[i].direction))
                {
                    directionsRep.Add(flechas[i].direction);
                    GenerateFlecha(flechas[i]);
                    GameManager.Instance.GiveMovementArrow(flechas[i].direction);
                }
            }
        }
    }

    public void ConseguirFlecha(TeclaFlecha flecha)
    {
        flechas.Add(new DirectionData { direction = flecha.GetDirection(), angle = flecha.GetOriginalRotation().z });
        Debug.Log(flechas[flechas.Count-1].angle + " " + flechas[flechas.Count - 1].direction);

        Transform arrowPlace = GetSocketTecla(flecha.GetDirection());

        PlaceFlechaInSocket(flecha, arrowPlace);
        flecha.SetTargetTransform(arrowPlace);
    }

    private void GenerateFlecha(DirectionData flechaData)
    {
        TeclaFlecha flechaPrefab = Resources.Load<TeclaFlecha>("Flecha");
        Transform socketTecla = GetSocketTecla(flechaData.direction);

        TeclaFlecha flechorra = Instantiate(flechaPrefab, socketTecla.position, new Quaternion(0, 0, flechaData.angle, 1));
        PlaceFlechaInSocket(flechorra, socketTecla);
    }

    private Transform GetSocketTecla(string direction)
    {
        switch (direction)
        {
            case "Up":
                return GameObject.Find("Arriba").transform;
            case "Down":
                return GameObject.Find("Abajo").transform;
            case "Left":
                return GameObject.Find("Izquierda").transform;
            case "Right":
                return GameObject.Find("Derecha").transform;
            default:
                return null;
        }
    }

    private void PlaceFlechaInSocket(TeclaFlecha flecha, Transform socket)
    {
        Debug.Log("HOLA");
        if(socket.childCount > 0)
        {
            Destroy(socket.GetChild(0).gameObject);
        }
        flecha.transform.parent = socket;
    }
}
