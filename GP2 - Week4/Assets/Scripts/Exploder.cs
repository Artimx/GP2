using UnityEngine;

public class Exploder : Actor
{
    public float explosionRadius = 5f;
    protected override void Awake()
    {
        base.Awake();
        // Additional initialization for Exploder can go here
        maxHealth = 100f;
        currentHealth = maxHealth;
        moveSpeed = 2;
    }
    public override void performAttack()
    {
        Explode();
    }

    public override void takeDamage(float damageAmount)
    {
        Explode();
    }
    private void Explode()
    {
        Debug.Log("BOOM");
        Destroy(gameObject);
    }
}
