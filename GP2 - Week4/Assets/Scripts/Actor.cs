using UnityEngine;

//ABSTRACT: prevents anyone from accidentally
//attaching this script to any game object.
public abstract class Actor : MonoBehaviour
{
    //Protected instead of private, this keeps the maxHealth hidden
    //from unrelated outside script.
    //While allowing derived classes to access it.
    [Header("Base Actor Attributes")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected float attackDamage = 10f;

    //We mark awake as virtual:
    //Ensures child classes can initialize
    //their own variable in their own awake.
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    //Every enemy attacks differently, so we make this method abstract.
    public abstract void performAttack();
    public virtual void takeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount}");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}