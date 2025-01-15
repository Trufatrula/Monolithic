using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private CajaEntrance[] entrances;
    [SerializeField] private CinemachineVirtualCamera camara;
    [SerializeField] private Image[] pantallasTransicion;

    [SerializeField] private List<CajaMovediza> puzleBoxes;

    private GlobalSceneController gSceneController;
    [SerializeField] private string fileName = "Campo";
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/scene" + fileName + "Data.json";
        Debug.Log(saveFilePath);
        gSceneController = GlobalSceneController.Instance;
        //gSceneController.SetSceneController(this);

        //LoadSceneData();

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
        //SaveSceneData();
        gSceneController.ExitScene(scene, newEntrance);
        pantallasTransicion[transicionAnimation].GetComponent<Animator>().SetTrigger("DerechaEntrar");
    }

    public void SaveSceneData()
    {
        SceneData sceneData = new SceneData();

        sceneData.cajas = new List<CajaMovedizaData>();
        foreach (var box in puzleBoxes)
        {
            CajaMovedizaData boxData = new CajaMovedizaData
            {
                position = box.transform.position,
                isPlaced = box.GetPlaced()
            };
            sceneData.cajas.Add(boxData);
        }

        string json = JsonUtility.ToJson(sceneData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log(saveFilePath);
    }

    public void LoadSceneData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(json);

            for (int i = 0; i < puzleBoxes.Count-1; i++)
            {
                Debug.Log(i);
                CajaMovediza box = puzleBoxes[i];
                CajaMovedizaData state = sceneData.cajas[i];

                box.SetPlaced(state.isPlaced);
                box.transform.position = state.position;
            }

            Debug.Log("Loaded " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("ERROR NO SAVE FILE");
        }
    }

}
