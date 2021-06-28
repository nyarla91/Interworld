using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string startTrigger;
    [SerializeField] private bool _permanent;
    [SerializeField] private LocationExit.Side playerSide;
    [SerializeField] private Dialogue dialogue;

    private void Awake()
    {
        if (_permanent && Saves.Data.dialoguesListened.Contains(name))
            Destroy(this);
        dialogue.camera = transform.position;
        Events.OnEvent += LaunchDialogue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == (int) Layer.Player)
        {
            LaunchDialogue("");
        }
    }

    private void LaunchDialogue(string type)
    {
        if (type.Length > 0 && !type.Equals(startTrigger))
            return;
        string state = Animations.IDLE;
        switch (playerSide)
        {
            case LocationExit.Side.Up:
            {
                state += Animations.UP;
                break;
            }
            case LocationExit.Side.Down:
            {
                state += Animations.DOWN;
                break;
            }
            case LocationExit.Side.Left:
            {
                state += Animations.LEFT;
                break;
            }
            case LocationExit.Side.Right:
            {
                state += Animations.RIGHT;
                break;
            }
        }
        Player.instance.animation.PlayState(state);
        DialogueWindow.instance.StartDialogue(dialogue);
        if (_permanent && !Saves.Data.dialoguesListened.Contains(name))
        {
            Saves.Data.dialoguesListened.Add(name);
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Events.OnEvent -= LaunchDialogue;
    }
}
