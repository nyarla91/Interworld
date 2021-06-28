using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip dashSound;
    public AudioClip healingSound;
    public AudioClip ringSound;
    public AudioClip deathSound;
    public List<AudioClip> throwSounds;
    public List<AudioClip> walkSounds;
    public List<AudioClip> damageSounds;

    /*private void Awake()
    {
        StartCoroutine(FootSteps());
    }

    private IEnumerator FootSteps()
    {
        while (true)
        {
            print(Controls.moveAxis.magnitude);
            if (Controls.moveAxis.magnitude > 0.2f)
            {
                Sound.PlaySound(CollectionHelper.ChooseRandomElement(walkSounds));
            }
            yield return new WaitForSeconds(0.3f);
        }
    }*/
}
