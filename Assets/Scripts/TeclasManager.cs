using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TeclasManager : MonoBehaviour
{
    public static TeclasManager Instance { get; private set; }

    [SerializeField] private List<Transform> socketsFlechas;

    private List<DirectionData> flechas;

    private Dictionary<string, int> teclasDisponibles; 

    private bool moverse = false;
    [SerializeField] Transform flechasInterfaz;
    private Vector3 flechasInterfazPosOriginal;
    private Vector3 flechasInterfazPosOffset;
    [SerializeField] Transform teclasInterfaz;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            flechas = new List<DirectionData>();
            teclasDisponibles = new Dictionary<string, int>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        flechasInterfazPosOriginal = flechasInterfaz.position;
        flechasInterfazPosOffset = flechasInterfazPosOriginal;
    }

    private void Update()
    {
        if (!moverse) return;

        if(flechasInterfaz.position == flechasInterfazPosOffset)
        {
            moverse = false;
            return;
        }

        flechasInterfaz.position = Vector3.Lerp(flechasInterfaz.position, flechasInterfazPosOffset, 5f * Time.deltaTime);
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

                AddTecla(flechas[i].direction);
                Debug.Log(GetAllTeclas());
            }
        }
    }

    public void ConseguirFlecha(TeclaFlecha flecha)
    {
        ToggleTargetPosition(false);
        flechas.Add(new DirectionData { direction = flecha.GetDirection(), angle = flecha.GetOriginalRotation().z });
        AddTecla(flecha.GetDirection());
        Debug.Log(flechas[flechas.Count-1].angle + " " + flechas[flechas.Count - 1].direction);

        Transform arrowPlace = GetSocketTecla(flecha.GetDirection());

        PlaceFlechaInSocket(flecha, arrowPlace);
        //flecha.SetTargetTransform(arrowPlace);
    }

    private void GenerateFlecha(DirectionData flechaData)
    {
        TeclaFlecha flechaPrefab = Resources.Load<TeclaFlecha>("Flecha");
        Transform socketTecla = GetSocketTecla(flechaData.direction);
        Quaternion rotationTecla = Quaternion.identity;
        rotationTecla.z = flechaData.angle;

        TeclaFlecha flechorra = Instantiate(flechaPrefab, socketTecla.position, rotationTecla);
        PlaceFlechaInSocket(flechorra, socketTecla);
    }

    private Transform GetSocketTecla(string direction)
    {
        switch (direction)
        {
            case "Up":
                return socketsFlechas[0];
            case "Down":
                return socketsFlechas[1];
            case "Left":
                return socketsFlechas[2];
            case "Right":
                return socketsFlechas[3];
            default:
                return null;
        }
    }

    private void PlaceFlechaInSocket(TeclaFlecha flecha, Transform socket)
    {
        Debug.Log("Flecha Placeada en socket");
        if (socket.childCount > 0)
        {
            Destroy(socket.GetChild(0).gameObject);
        }
        flecha.transform.parent = socket;
    }

    public void ToggleTargetPosition(bool original)
    {
        if (original)
        {
            flechasInterfazPosOffset = flechasInterfazPosOriginal;
        }
        else
        {
            flechasInterfazPosOffset = flechasInterfazPosOriginal + new Vector3(0, -5f, 0);
        }
        moverse = true;
    }

    public void AddTecla(string tipoTecla, int cantidad = 1)
    {
        if (teclasDisponibles.ContainsKey(tipoTecla))
        {
            teclasDisponibles[tipoTecla] += cantidad;
        }
        else
        {
            teclasDisponibles[tipoTecla] = cantidad;
        }
    }

    public bool QuitarTecla(string tipoTecla, int cantidad = 1)
    {
        if (teclasDisponibles.ContainsKey(tipoTecla) && teclasDisponibles[tipoTecla] >= cantidad)
        {
            teclasDisponibles[tipoTecla] -= cantidad;

            if (teclasDisponibles[tipoTecla] == 0)
            {
                teclasDisponibles.Remove(tipoTecla);
            }
            return true;
        }
        return false;
    }

    public int GetCantidadTecla(string tipoTecla)
    {
        if (teclasDisponibles.ContainsKey(tipoTecla))
        {
            return teclasDisponibles[tipoTecla];
        }
        return 0;
    }

    public Dictionary<string, int> GetAllTeclas()
    {
        return new Dictionary<string, int>(teclasDisponibles);
    }

}
