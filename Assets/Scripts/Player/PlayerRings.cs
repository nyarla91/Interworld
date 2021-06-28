using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRings : Transformer
{

    private static int _energy;
    public static int energy
    {
        get => _energy;
        set => Player.instance.ui.UpdateEnergy(_energy = value);
    }

    private List<Coroutine> _effects;

    private void Start()
    {
        energy = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.instance.keyRings) && energy >= 2)
        {
            energy -= 2;
            ActivateRings();
        }
    }

    public void ActivateRings()
    {
        List<Sprite> rings = Saves.Data.ringsActive;
        if (rings.Count == 0)
            return;
        Sound.Play(Player.instance.sound.ringSound);
        // Knife Juggler's Ring
        if (rings.Contains(TypesData.instance.ringJuggler))
        {
            Player.instance.weapon.CollectAllknives();
        }
        // Rogue's Ring
        if (rings.Contains(TypesData.instance.ringRogue))
        {
            StartCoroutine(Rogue());
        }
        // Golem's Ring
        if (rings.Contains(TypesData.instance.ringGolem))
        {
            Player.instance.status.InterruptImmune();
            StartCoroutine(Player.instance.status.DamageImmunity(3));
        }
        // Pyromancer's Ring
        if (rings.Contains(TypesData.instance.ringPyromancer))
        {
            if (CombatArea.current.enemies.Count > 0)
            {
                List<EnemyStatus> enemies = new List<EnemyStatus>();
                foreach (var enemy in CombatArea.current.enemies)
                {
                    EnemyStatus newStatus = enemy.GetComponent<EnemyStatus>();
                    if (newStatus != null)
                        enemies.Add(newStatus);
                }
                enemies.OrderBy(t => Vector2.Distance(t.transform.position, transform.position));
                enemies[0].Die();
            }
        }
        // Sorcerer's Ring
        if (rings.Contains(TypesData.instance.ringSorcerer))
        {
            energy++;
        }
        // Cryomancer's Ring
        if (rings.Contains(TypesData.instance.ringCryomancer))
        {
            StartCoroutine(CryomancerRing());
        }
        
    }

    private IEnumerator Rogue()
    {
        Player.instance.movement.AddSpeedModifier("rogueRing", 2);
        yield return new WaitForSeconds(2);
        Player.instance.movement.CancelSpeedModifier("rogueRing");
    }

    private IEnumerator CryomancerRing()
    {
        CombatArea.current.CryomancerRingEffect(true);
        yield return new WaitForSeconds(1.5f);
        CombatArea.current.CryomancerRingEffect(false);
    }
}
