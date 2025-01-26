using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Xml.Linq;

public class TeclasManager : MonoBehaviour
{
    public static TeclasManager Instance { get; private set; }

    [SerializeField] private List<Transform> socketsFlechas;
    [SerializeField] private Transform espacio;

    [SerializeField] private Animator letrasRevelar;
    [SerializeField] private List<GameObject> letrasSitions;
    private GameManager gameManager;

    private SocketsTeclas[] socketsTeclas;

    private List<DirectionData> flechas;
    private Dictionary<string, int> teclasDisponibles;
    private Dictionary<string, Type> teclasComponentes = new Dictionary<string, Type>
    {
        { "Left", typeof(PlayerMoveLeftArrow) },
        { "Right", typeof(PlayerMoveRightArrow) },
        { "Down", typeof(PlayerMoveDownArrow) },
        { "Up", typeof(PlayerMoveUpArrow) },
        { "Space", typeof(PlayerMoveSpace) }
    };

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
            socketsTeclas = new SocketsTeclas[75];
            gameManager = GameManager.Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateSocketsTeclas();
        flechasInterfazPosOriginal = flechasInterfaz.localPosition;
        flechasInterfazPosOffset = flechasInterfazPosOriginal;
    }

    private void Update()
    {
        if (!moverse) return;

        if(flechasInterfaz.localPosition == flechasInterfazPosOffset)
        {
            moverse = false;
            return;
        }

        flechasInterfaz.localPosition = Vector3.Lerp(flechasInterfaz.localPosition, flechasInterfazPosOffset, 5f * Time.deltaTime);
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
                    Debug.Log(flechas[i].direction);
                    GenerateFlecha(flechas[i]);
                }
                AddTecla(flechas[i].direction);
            }
        }
    }

    public void ConseguirTecla(TeclaItem tecla)
    {
        ToggleTargetPosition(false);

        if(tecla is TeclaFlecha flecha)
        {
            flechas.Add(new DirectionData { direction = flecha.GetDirection(), angle = flecha.GetOriginalRotation().z });
            AddTecla(flecha.GetDirection());
            Debug.Log("Conseguida " + flecha.GetDirection());
            //Debug.Log(flechas[flechas.Count-1].angle + " " + flechas[flechas.Count - 1].direction);

            Transform arrowPlace = GetSocketTecla(flecha.GetDirection());

            PlaceFlechaInSocket(flecha, arrowPlace);
        }
        else
        {
            AddTecla(tecla.GetTeclaValue());
            Debug.Log("Conseguida " + tecla.GetTeclaValue());
            PlaceTeclaInSocketLetra(tecla);
        }

        //flecha.SetTargetTransform(arrowPlace);
        tecla.PlaceTeclaInSocket();
    }

    public void LoseTecla(TeclaItem tecla)
    {
        if(tecla is TeclaFlecha flecha)
        {
            for (int i = flechas.Count - 1; i >= 0; i--)
            {
                if (flechas[i].direction.Equals(flecha.GetDirection()))
                {
                    flechas.RemoveAt(i);
                    break;
                }
            }
            flecha.transform.parent = null;
            RemoveTecla(flecha.GetDirection());

            Debug.Log("Perdida " + flecha.GetDirection());
        }
        else
        {
            tecla.transform.parent = null;
            RemoveTecla(tecla.GetTeclaValue());
        }

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
            if(socket.GetChild(0).gameObject != flecha.gameObject)
            {
                Destroy(socket.GetChild(0).gameObject);
            }
        }
        flecha.transform.parent = socket;
    }

    private void PlaceTeclaInSocketLetra(TeclaItem tecla)
    {
        if(tecla.GetTeclaValue().Equals("Space"))
        {
            tecla.transform.parent = espacio;
        }
        else
        {
            letrasRevelar.SetTrigger("Revelar");
            tecla.GetComponent<SpriteRenderer>().sortingOrder = 5;
            switch (tecla.GetTeclaValue())
            {

                case "k":
                    tecla.transform.parent = letrasSitions[0].transform;
                    return;
                case "e":
                    tecla.transform.parent = letrasSitions[1].transform;
                    return;
                case "r":
                    tecla.transform.parent = letrasSitions[2].transform;
                    return;
                case "o":
                    tecla.transform.parent = letrasSitions[3].transform;
                    return;
                case "n":
                    tecla.transform.parent = letrasSitions[4].transform;
                    return;
                case "t":
                    tecla.transform.parent = letrasSitions[5].transform;
                    return;
                case "a":
                    tecla.transform.parent = letrasSitions[6].transform;
                    return;
                case "s":
                    tecla.transform.parent = letrasSitions[7].transform;
                    return;
                default:
                    return;
            }
        }
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

    public void AddTecla(string tecla)
    {
        if (teclasComponentes.TryGetValue(tecla, out Type teclaComponent))
        {
            if (!teclasDisponibles.ContainsKey(tecla))
            {
                teclasDisponibles[tecla] = 0;
                Debug.Log("Debug AddTecla " + tecla);
                gameManager.AddComponentToPlayer(teclaComponent);
            }
            teclasDisponibles[tecla]++;
        }
        else
        {
            Debug.LogError("No existe " + tecla);
        }
    }

    public void RemoveTecla(string tecla)
    {
        if (teclasComponentes.TryGetValue(tecla, out Type teclaComponent))
        {
            if (teclasDisponibles.ContainsKey(tecla))
            {
                teclasDisponibles[tecla]--;
                if (teclasDisponibles[tecla] <= 0)
                {
                    gameManager.RemoveComponentFromPlayer(teclaComponent);
                    teclasDisponibles.Remove(tecla);
                    Debug.Log("Quitada " + tecla);
                }
                else
                {
                    for (int i = flechas.Count - 1; i >= 0; i--)
                    {
                        if (flechas[i].direction.Equals(tecla))
                        {
                                GenerateFlecha(flechas[i]);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No existe " + tecla);
        }
    }

    public void ReloadTeclas()
    {
        foreach (var tecla in teclasDisponibles)
        {
            if (teclasComponentes.TryGetValue(tecla.Key, out Type teclaComponent))
            {
                gameManager.AddComponentToPlayer(teclaComponent);
            }
            else
            {
                Debug.LogError(tecla + " no disponible");
            }
        }
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

    public void UpdateSocketsTeclas()
    {
        Array.Clear(socketsTeclas, 0, socketsTeclas.Length);
        socketsTeclas = FindObjectsOfType<SocketsTeclas>();
    }
    public void AparecerTecla()
    {
        letrasRevelar.SetTrigger("Aparecer");
    }
    public SocketsTeclas[] GetSocketsTeclas()
    {
        return socketsTeclas;
    }
}
