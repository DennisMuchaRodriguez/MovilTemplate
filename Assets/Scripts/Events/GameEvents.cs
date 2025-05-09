using System;

public static class GameEvents
{
   
    public static event Action<string> OnSceneLoadStarted;
    public static event Action<string> OnSceneLoadCompleted;

    public static void SceneLoadStarted(string sceneName)
    {
        OnSceneLoadStarted?.Invoke(sceneName);
    }

    public static void SceneLoadCompleted(string sceneName)
    {
        OnSceneLoadCompleted?.Invoke(sceneName);
    }


    public static event Action<string> OnSoundPlayed;
    public static event Action<string> OnSoundStopped;

    public static void SoundPlayed(string soundName)
    {
        OnSoundPlayed?.Invoke(soundName);
    }

    public static void SoundStopped(string soundName)
    {
        OnSoundStopped?.Invoke(soundName);
    }

    
    public static event Action<string> OnObjectSpawned;
    public static event Action<string> OnObjectDespawned;

    public static void ObjectSpawned(string poolTag)
    {
        OnObjectSpawned?.Invoke(poolTag);
    }

    public static void ObjectDespawned(string poolTag)
    {
        OnObjectDespawned?.Invoke(poolTag);
    }
}