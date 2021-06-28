using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static int maxLives = 3;
    private static int _healingPotions = 3;

    public static int healingPotions
    {
        get => _healingPotions;
        set => Player.instance.ui.UpdateHealingPotions(_healingPotions = value);
    }

    private static int _lives;
    public static int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                Saves.Load();
                return;
            }
            _lives = value;
            Player.instance.ui.UpdateHearts(_lives, maxLives);
        }
    }
    
    private bool _immune;
    private bool _cheatImmune;

    private void Start()
    {
        lives = maxLives;
        healingPotions = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyHealingPotion))
            UseHealingPotion();
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.K) &&
            Input.GetKeyDown(KeyCode.M))
        {
            _cheatImmune = !_cheatImmune;
        }
    }

    public void TakeDamage(int damage)
    {
        if (_immune || _cheatImmune)
            return;
        StartCoroutine(DamageImmunity(1));
        if (lives - damage > 0)
        {
            Sound.Play(CollectionHelper.ChooseRandomElement(Player.instance.sound.damageSounds), 0.4f);
            if (CombatArea.combat)
                Player.instance.rings.ActivateRings();
        }
        else
            Sound.Play(Player.instance.sound.deathSound);
        lives-= damage;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == (int) Layer.Enemy || other.gameObject.layer == (int) Layer.FlyingEnemy)
        {
            TakeDamage(other.gameObject.GetComponent<EnemyAttack>().meleeDamage);
        }
    }

    private void UseHealingPotion()
    {
        if (healingPotions > 0 && lives < maxLives)
        {
            Sound.Play(Player.instance.sound.healingSound);
            healingPotions--;
            lives = maxLives;
        }
    }

    public void InterruptImmune()
    {
        Player.instance.status.StopAllCoroutines();
        Player.instance.animation.StopAllCoroutines();
        Player.instance.status._immune = false;
        Player.instance.animation.spriteRenderer.color = Color.white;
    }

    public IEnumerator DamageImmunity(float duration)
    {
        _immune = true;
        Coroutine blinking = StartCoroutine(Player.instance.animation.DamageBlinking());
        yield return new WaitForSeconds(duration);
        _immune = false;
        StopCoroutine(blinking);
        Player.instance.animation.spriteRenderer.color = Color.white;
    }
}
