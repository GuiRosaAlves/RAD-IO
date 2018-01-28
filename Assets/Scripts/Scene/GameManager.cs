using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string currentSceneName;
    private string nextSceneName;
    private AsyncOperation resourceUnloadTask;
    private AsyncOperation sceneLoadTask;
    private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
    private SceneState sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] updateDelegates;


    //--------------------------------------------------------------------------------------------
    //Public Static Methods
    //--------------------------------------------------------------------------------------------
    public void SwitchScene(string nextSceneName)
    {
        if (instance != null)
        {
            if (instance.currentSceneName != nextSceneName)
            {
                instance.nextSceneName = nextSceneName;
            }
        }
    }

    //--------------------------------------------------------------------------------------------
    //Protected Methods
    //--------------------------------------------------------------------------------------------

    protected void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);

        instance = this;

        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        nextSceneName = "FaseTeste01";
        sceneState = SceneState.Run;
    }

    protected void OnDestroy()
    {
        //Clean up all the Update Delegates
        if (updateDelegates != null)
        {
            for (int i = 0; i < (int)SceneState.Count; i++)
            {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        //Clean up the singleton instance
        if (instance != null)
        {
            instance = null;
        }
    }

    protected void OnDisable()
    {
    }

    protected void OnEnable()
    {
    }

    protected void Start()
    {
    }

    protected void Update()
    {
        if (updateDelegates[(int)sceneState] != null)
        {
            updateDelegates[(int)sceneState]();
        }
    }

    //--------------------------------------------------------------------------------------------
    //Private Methods
    //--------------------------------------------------------------------------------------------
    private void UpdateSceneReset()
    {
        //Run a GC pass
        System.GC.Collect();
        sceneState = SceneState.Preload;
    }

    private void UpdateScenePreload()
    {
        sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
        sceneState = SceneState.Load;
    }

    private void UpdateSceneLoad()
    {
        //done Loading?
        if (sceneLoadTask.isDone == true)
        {
            sceneState = SceneState.Unload;
        }
        else
        {
            // update scene loading progress
        }
    }

    private void UpdateSceneUnload()
    {
        //cleaning up resources yet?
        if (resourceUnloadTask == null)
        {
            resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            // done cleaning up?
            if (resourceUnloadTask.isDone == true)
            {
                resourceUnloadTask = null;
                sceneState = SceneState.Postload;
            }
        }
    }

    private void UpdateScenePostload()
    {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }

    private void UpdateSceneReady()
    {
        //run a GC pass
        // if you have assets loaded in the scene that are
        // currently unused but may be used later
        // DONT do this here
        System.GC.Collect();
        sceneState = SceneState.Run;
    }

    //wait for sceneChange
    private void UpdateSceneRun()
    {
        if (currentSceneName != nextSceneName)
        {
            sceneState = SceneState.Reset;
        }
    }
}