
using UnityEngine;

public class RedEnemy : EnemyBase
{
    protected override void Awake()
    {
        maxHealth = 2f;
        moveSpeed = 3f;

        base.Awake();
    }
}
