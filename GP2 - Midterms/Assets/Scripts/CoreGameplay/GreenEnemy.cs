
using UnityEngine;

public class GreenEnemy : EnemyBase
{
    protected override void Awake()
    {
        maxHealth = 4f;
        moveSpeed = 2f;

        base.Awake();
    }
}
