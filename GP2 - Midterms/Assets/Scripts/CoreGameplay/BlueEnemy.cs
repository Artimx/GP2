
using UnityEngine;

public class BlueEnemy : EnemyBase
{
    protected override void Awake()
    {
        maxHealth = 1f;
        moveSpeed = 5f;

        base.Awake();
    }
}
