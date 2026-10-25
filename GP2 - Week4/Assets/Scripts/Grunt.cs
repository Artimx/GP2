using UnityEngine;

public class Grunt : Actor
{
    protected override void Awake()
    {
        base.Awake();
        // Additional initialization for Grunt can go here
        maxHealth = 40f;
        currentHealth = maxHealth;
        moveSpeed = 4;
    }
    public override void performAttack()
    {
        Debug.Log("I perform BASH attack!");
    }
}