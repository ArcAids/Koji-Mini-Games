using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneController : ScriptableObject
{
    [SerializeField] List<string> tips;

    // Queue<(int, LoadSceneMode, bool)> LevelLoadQueue;
    LevelLoader listener;

    [System.NonSerialized] protected bool inited = false;

    AsyncOperation levelLoadingOperation;

    int SceneToLoad=-1;
    LoadSceneMode mode=LoadSceneMode.Single;

    public bool IsLoading { get { if (levelLoadingOperation == null) return false; else return !levelLoadingOperation.isDone; } }
    public int CurrentSceneIndex { get => SceneManager.GetActiveScene().buildIndex; }
    public float Progress { get {if (levelLoadingOperation==null || levelLoadingOperation.isDone) return 1; else return levelLoadingOperation.progress; } }
    public virtual void Init()
    {
        if (inited)
            return;
        hideFlags = HideFlags.DontUnloadUnusedAsset;
        inited = true;
    }

    public string GetRandomTip()
    {
        if (tips != null && tips.Count > 0)
            return tips[Random.Range(0, tips.Count - 1)];
        else
            return string.Empty;
    }

    public void FadeOutAndIn()
    {
        listener.FadeOut();
    }

    void Load(int sceneIndex, bool additive, bool showLoading=true)
    {
        if (IsLoading || sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
            return;
        SceneToLoad = sceneIndex;
        mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;

        if (listener != null)
        {
            listener.PlayLevelTransition(showLoading);
        }
        else
        {
            StartLevelLoading();
            FinishLevelLoading();
        }
    }

    public virtual void StartLevelLoading()
    {
        if (SceneToLoad < 0)
            return;

        levelLoadingOperation = SceneManager.LoadSceneAsync(SceneToLoad, mode);
        levelLoadingOperation.allowSceneActivation = false;
    }

    public virtual void FinishLevelLoading()
    {
        levelLoadingOperation.allowSceneActivation = true;
        SceneToLoad = -1;
        mode = LoadSceneMode.Single;
    }

    public void Reload()
    {
        Load(CurrentSceneIndex,false,false);
    }
    public void LoadWithFade(int sceneIndex)
    {
        Load(sceneIndex, false,false);
    }
    public void Load(int sceneIndex)
    {
        Load(sceneIndex, false);
    }
    public void LoadWithoutLoadingScreen(int sceneIndex)
    {
        Load(sceneIndex,false,false);
    }
    public void Load(string sceneIndex)
    {
        Load(sceneIndex, false);
    }
    public void Load(string sceneName, bool additive)
    {
        Load(SceneManager.GetSceneByName(sceneName).buildIndex, additive);
    }
    public void LoadPreviousScene()
    {
        int level = CurrentSceneIndex - 1;
        if (level >= 0)
            Load(level);
        else
            Debug.Log("Invalid Scene");
    }

    public void LoadNextScene()
    {
        int level = CurrentSceneIndex + 1;
        if (level < SceneManager.sceneCountInBuildSettings)
            Load(level);
        else
            Debug.Log("Invalid Scene");
    }

    public void UnloadScene(int index)
    {
        Debug.Log("Attempting unload: " + index);
        if(SceneManager.GetSceneByBuildIndex(index).isLoaded)
            SceneManager.UnloadSceneAsync(index);
    }

    public void Register(LevelLoader passedEvent)
    {
        listener =passedEvent;
    }

    public void DeRegister(LevelLoader passedEvent)
    {
        if (listener == passedEvent)
            listener = null;
    }
}

public interface IOnLevelAdded
{
    void OnLevelAdded();
}
