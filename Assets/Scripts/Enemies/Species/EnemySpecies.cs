using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecies : Transformer
{
    [SerializeField] protected EnemyMovement movement;
    [SerializeField] protected EnemyStatus status;
    [SerializeField] protected new EnemyAnimation animation;

    protected float distanceToPlayer => Vector2.Distance(transform.position, Player.instance.transform.position);
    protected float pathToPlayer => movement.aiPath.remainingDistance;

    protected Vector2 directiontoPlayer => (Player.instance.transform.position - transform.position).normalized;
}
