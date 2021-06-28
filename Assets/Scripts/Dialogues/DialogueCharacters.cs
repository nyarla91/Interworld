using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacters : MonoBehaviour
{
    public static DialogueCharacters instance;
    
    public List<DialogueCharacter> Characters;

    private void Awake()
    {
        instance = this;
    }

    [Serializable]
    public class DialogueCharacter
    {
        [SerializeField] private string[] _name;
        public string name => _name[Localization.language];
        public Color color;
    }
}
