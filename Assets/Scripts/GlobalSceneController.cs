using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GlobalSceneController : MonoBehaviour
{
    public static GlobalSceneController Instance { get; private set; }

    private string lastEntrance;
    private GameManager gameManager;
    //private SceneController sceneController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameManager = GameManager.Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterScene(Transform playerSpawn)
    {
        InstantiatePlayerOnScene(playerSpawn);
        //StartCoroutine(AnimacionTransicionEntrar(playerSpawn));
    }

    public void ExitScene(string scene, string newEntrance)
    {
        lastEntrance = newEntrance;
        StartCoroutine(AnimacionTransicionSalir(scene));
    }

    private IEnumerator AnimacionTransicionSalir(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(scene);
    }

    //private IEnumerator AnimacionTransicionEntrar(Transform playerSpawn)
    //{
    //    yield return new WaitForSeconds(1);
    //    InstantiatePlayerOnScene(playerSpawn);
    //}

    public void InstantiatePlayerOnScene(Transform position)
    {
        gameManager.InstantiatePlayer(position);
    }

    public string GetEntrance()
    {
        return lastEntrance;
    }

    //public void SetSceneController(SceneController sceneController)
    //{
    //    this.sceneController = sceneController;
    //}
}