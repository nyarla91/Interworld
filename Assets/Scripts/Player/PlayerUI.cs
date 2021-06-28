using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : UIAlpha
{
    [SerializeField] private List<Image> _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private List<Image> _healingPotions;
    [SerializeField] private List<Image> _knives;
    [SerializeField] private List<Image> _energy;

    private bool _blinking;

    private float _healthAlpha;

    private float healthAlpha
    {
        get => _healthAlpha;
        set
        {
            _healthAlpha = value;
            foreach (var heart in _hearts)
            {
                heart.color = new Color(1, 1, 1, value);
            }
        }
    }

    public void UpdateHearts(int lives, int maxHearts)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].enabled = i + 1 <= maxHearts;
        }
        for (int i = 0; i < maxHearts; i++)
        {
            if (lives >= i + 1)
                _hearts[i].sprite = _fullHeart;
            else
                _hearts[i].sprite = _emptyHeart;
        }
        _blinking = lives == 1 && PlayerStatus.healingPotions > 0;
    }
    public void UpdateKnives(int knives)
    {
        for (int i = 0; i < _knives.Count; i++)
        {
            if (knives >= i + 1)
                _knives[i].color = Color.white;
            else
                _knives[i].color = Color.gray;
        }
    }

    public void UpdateHealingPotions(int potions)
    {
        for (int i = 0; i < _healingPotions.Count; i++)
        {
            if (potions >= i + 1)
                _healingPotions[i].color = Color.white;
            else
                _healingPotions[i].color = Color.gray;
        }
        _blinking = PlayerStatus.lives == 1 && potions > 0;
    }

    public void UpdateEnergy(int energy)
    {
        for (int i = 0; i < _energy.Count; i++)
        {
            if (energy >= i + 1)
                _energy[i].color = Color.white;
            else
                _energy[i].color = Color.gray;
        }
    }

    private void Update()
    {
        if (!_blinking)
        {
            healthAlpha = 1;
        }
        else
        {
            healthAlpha = (Mathf.Sin(Time.time * 8) + 1) / 2 * 0.5f + 0.5f;
        }
    }
}
