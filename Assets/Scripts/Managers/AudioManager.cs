using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop = false;
    [HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    public Sound[] sounds;

    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    public event Action<float> OnMasterVolumeChanged;
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                
                s.source.outputAudioMixerGroup = sfxMixer;

                soundDictionary.Add(s.name, s);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out Sound sound))
        {
            sound.source.Play();
        }
    }

    public void Stop(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out Sound sound))
        {
            sound.source.Stop();
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        OnMasterVolumeChanged?.Invoke(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        OnMusicVolumeChanged?.Invoke(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        OnSFXVolumeChanged?.Invoke(volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        masterMixer.audioMixer.GetFloat("MasterVolume", out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public float GetMusicVolume()
    {
        float volume;
        musicMixer.audioMixer.GetFloat("MusicVolume", out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public float GetSFXVolume()
    {
        float volume;
        sfxMixer.audioMixer.GetFloat("SFXVolume", out volume);
        return Mathf.Pow(10, volume / 20);
    }
}