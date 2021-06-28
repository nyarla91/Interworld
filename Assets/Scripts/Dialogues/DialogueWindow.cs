using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : UIAlpha
{
    public static DialogueWindow instance;

    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _speech;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        StartCoroutine(DialogueSpeech(dialogue));
    }
    
    private IEnumerator DialogueSpeech(Dialogue dialogue)
    {
        Vector2 oldAnchor = CameraFollow.instance.anchor;
        float oldInterpolation = CameraFollow.instance.interpolation;
        float oldSize = CameraFollow.instance.size;
        
        CameraFollow.instance.ApplySettings(dialogue.camera, 0, 4);
        Controls.freezedControls++;
        Player.instance.ui.Hide();
        if (dialogue.beginningTrigger.Length > 0)
            Events.OnEvent(dialogue.beginningTrigger);
        Show();
        
        foreach (var line in dialogue.lines)
        {
            _characterName.text = line.character.name;
            _characterName.color = line.character.color;
            _speech.text = "";
            char[] litters = line.sentence[Localization.language].ToCharArray();
            foreach (var litter in litters)
            {
                _speech.text += litter;
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(Controls.instance.keyNextSentence));
        }
        CameraFollow.instance.ApplySettings(oldAnchor, oldInterpolation, oldSize);
        Controls.freezedControls--;
        Player.instance.ui.Show();
        if (dialogue.endingTrigger.Length > 0)
            Events.OnEvent(dialogue.endingTrigger);
        Hide();
    }
}

[Serializable]
public class Dialogue
{
    public string beginningTrigger;
    public string endingTrigger;
    public Vector2 camera;
    public List<DialogueLine> lines;
    
    [Serializable]
    public class DialogueLine
    {
        [SerializeField] private int _character;
        public DialogueCharacters.DialogueCharacter character => DialogueCharacters.instance.Characters[_character];
        [TextArea(5, 10)] public string[] sentence;
    }
}
