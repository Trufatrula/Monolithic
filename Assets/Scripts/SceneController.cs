using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private CajaEntrance[] entrances;
    [SerializeField] private CinemachineVirtualCamera camara;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if(gameManager == null)
        {
            Debug.Log("ASDASAS");
        }

        foreach (CajaEntrance entrance in entrances)
        {
            if (entrance.name == gameManager.GetEntrance())
            {
                camara.Follow = entrance.GetEntrance();
                gameManager.InstantiatePlayer(entrance.GetPlayerSpawn());
                break;
            }
        }
    }

}
