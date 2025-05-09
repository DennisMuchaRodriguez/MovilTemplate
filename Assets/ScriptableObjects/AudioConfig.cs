using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Game/Audio Configuration")]
public class AudioConfig : ScriptableObject
{
    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 0.8f;

    [Header("Sound Clips")]
    public AudioClip[] backgroundMusic;
    public AudioClip[] soundEffects;
}