using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private CajaEntrance[] entrances;
    [SerializeField] private CinemachineVirtualCamera camara;
    [SerializeField] private Image[] pantallasTransicion;

    private GlobalSceneController gSceneController;

    private void Start()
    {
        gSceneController = GlobalSceneController.Instance;
        //gSceneController.SetSceneController(this);

        foreach (CajaEntrance entrance in entrances)
        {
            entrance.SetSceneController(this);
            if (entrance.name == gSceneController.GetEntrance())
            {
                camara.Follow = entrance.GetEntrance();
                gSceneController.EnterScene(entrance.GetPlayerSpawn());
                pantallasTransicion[entrance.GetTransitionAnimation()].GetComponent<Animator>().SetTrigger("DerechaSalir");
                break;
            }
            pantallasTransicion[0].GetComponent<Animator>().SetTrigger("DerechaSalir");
        }
    }

    public void ExitScene(string scene, string newEntrance, int transicionAnimation)
    {
        gSceneController.ExitScene(scene, newEntrance);
        pantallasTransicion[transicionAnimation].GetComponent<Animator>().SetTrigger("DerechaEntrar");
    }
}
