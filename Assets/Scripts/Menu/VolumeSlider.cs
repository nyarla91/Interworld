using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LWRP;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private bool _music;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        if (!_music)
            _slider.value = Saves.Config.soundVolume;
        else
            _slider.value = Saves.Config.musicVolume;
    }
}
