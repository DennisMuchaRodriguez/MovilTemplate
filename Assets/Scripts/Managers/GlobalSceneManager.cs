using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GlobalSceneManager : MonoBehaviour
{
    public static GlobalSceneManager Instance { get; private set; }

    public event Action<string> OnSceneLoaded;
    public event Action<string> OnSceneUnloaded;
    public event Action<string> OnSceneChanged;

    private Dictionary<string, AsyncOperation> loadingOperations = new Dictionary<string, AsyncOperation>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, bool additive = true)
    {
        if (!loadingOperations.ContainsKey(sceneName))
        {
            LoadSceneMode mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
            operation.completed += (op) => OnSceneLoadCompleted(sceneName);
            loadingOperations.Add(sceneName, operation);
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
            operation.completed += (op) => OnSceneUnloadCompleted(sceneName);
        }
    }

    private void OnSceneLoadCompleted(string sceneName)
    {
        loadingOperations.Remove(sceneName);
        OnSceneLoaded?.Invoke(sceneName);
    }

    private void OnSceneUnloadCompleted(string sceneName)
    {
        OnSceneUnloaded?.Invoke(sceneName);
    }

    public void SetActiveScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            SceneManager.SetActiveScene(scene);
            OnSceneChanged?.Invoke(sceneName);
        }
    }

    public bool IsSceneLoaded(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }
}
