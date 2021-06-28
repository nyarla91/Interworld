using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public const float SOUND_VOLUME = 1;
    
    public static Sound instance;

    [SerializeField] private GameObject _soundPrefab;

    private void Awake()
    {
        Saves.LoadConfig();
        instance = this;
    }

    public static void Play(AudioClip clip)
    {
        Play(clip, 1);
    }

    public static void Play(AudioClip clip, float volumeModifier)
    {
        AudioSource newSound = Instantiate(instance._soundPrefab, Player.instance.transform.position, Quaternion.identity).GetComponent<AudioSource>();
        newSound.clip = clip;
        newSound.volume = Saves.Config.soundVolume * volumeModifier * SOUND_VOLUME;
        newSound.Play();
    }
    
    public static void VolumeSetting(float volume)
    {
        Saves.Config.soundVolume = volume;
    }
}
