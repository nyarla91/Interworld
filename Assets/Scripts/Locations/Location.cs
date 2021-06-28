using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : Transformer
{
    [SerializeField] private string[] _locationName;
    public AudioClip music;
    public string locationName => _locationName[Localization.language];
    public List<LocationExit> exits;
}