using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public const float MUSIC_VOLUME = 0.8f;

    public static Music instance;
    
    private static float _backgroundVolume;
    public static float backgroundVolume
    {
        get => _backgroundVolume;
        set
        {
            _backgroundVolume = value;
            CountVolume();
        }
    }
    private static float _overrideVolume;
    public static float overrideVolume
    {
        get => _overrideVolume;
        set
        {
            _overrideVolume = value;
            CountVolume();
        }
    }
    
    [SerializeField] private AudioSource _backgroundaudioSource;
    [SerializeField] private AudioSource _overrideAudioSource;
    public AudioClip battleMusic;

    private void Awake()
    {
        Saves.LoadConfig();
        instance = this;
    }

    public void PlayBackground(AudioClip music)
    {
        if (music == null)
        {
            _backgroundaudioSource.Stop();
            return;
        }
        _backgroundaudioSource.clip = music;
        _backgroundaudioSource.Play();
        StopAllCoroutines();
        StartCoroutine(Fade(false));
    }

    public void PlayOverride(AudioClip music)
    {
        _overrideAudioSource.clip = music;
        _overrideAudioSource.Play();
        StopAllCoroutines();
        StartCoroutine(Fade(true));
    }

    public void StopOverride()
    {
        _backgroundaudioSource.Play();
        StopAllCoroutines();
        StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool toOverride)
    {
        float targetOverrideVolume = BoolHelper.BoolToInt(toOverride);
        while (Mathf.Abs(overrideVolume - targetOverrideVolume) > 0.01f)
        {
            overrideVolume = Mathf.Lerp(overrideVolume, targetOverrideVolume, Time.deltaTime * 2);
            backgroundVolume = 1 - overrideVolume;
            yield return null;
        }
        overrideVolume = targetOverrideVolume;
        backgroundVolume = 1 - overrideVolume;
    }
    
    public static void VolumeSetting(float volume)
    {
        Saves.Config.musicVolume = volume;
        CountVolume();
    }
    
    public static void SaveConfig()
    {
        Saves.SaveConfig();
        print(Saves.Config.musicVolume);
    }

    private static void CountVolume()
    {
        instance._backgroundaudioSource.volume = backgroundVolume * Saves.Config.musicVolume * MUSIC_VOLUME;
        instance._overrideAudioSource.volume = overrideVolume * Saves.Config.musicVolume * MUSIC_VOLUME;
    }
}
