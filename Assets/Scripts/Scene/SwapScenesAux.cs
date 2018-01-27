using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScenesAux : MonoBehaviour {

    public static SwapScenesAux instance;

    public List<string> ScenesList = new List<string>();
    private int currentSceneIndex;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentSceneIndex = ScenesList.IndexOf(GameManager.instance.currentSceneName);
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        GameManager.instance.SwitchScene(ScenesList[currentSceneIndex+1]);
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
