using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScenesAux : MonoBehaviour {

    public static SwapScenesAux instance;

    public List<string> ScenesList = new List<string>();
    private int currentSceneIndex;
    private int desiredSceneIndex;
    private string aux = "";

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentSceneIndex = ScenesList.IndexOf(GameManager.instance.currentSceneName);
        if (Input.anyKeyDown)
        {
            aux += Input.inputString;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int.TryParse(aux, out desiredSceneIndex);
            aux = "";
            desiredSceneIndex = Mathf.Clamp(desiredSceneIndex, 0, ScenesList.Count-1);
            NextScene(desiredSceneIndex);
        }
    }

    public void NextScene()
    {
        GameManager.instance.SwitchScene(ScenesList[currentSceneIndex+1]);
    }

    public void NextScene(int desiredScene)
    {
        GameManager.instance.SwitchScene(ScenesList[desiredScene]);
    }

    public void GameOver()
    {
        GameManager.instance.SwitchScene("GameOver Scene");
    }

    private void OnDestroy()
    {
        if(instance != null)
        {
            instance = null;
        }
    }
}
