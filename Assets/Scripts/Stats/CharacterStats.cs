using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth { get; private set; }

    [Header("Combat")]
    public Stat damage;
    public Stat armor;
    public int criticalHitMultiplier = 2;
    public float criticalChance = 0.1f;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage, bool isCriticalHit = false)
    {
        if (isCriticalHit)
            damage *= criticalHitMultiplier;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
