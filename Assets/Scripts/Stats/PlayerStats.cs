using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Stamina")]
    public int maxStamina;
    public int currentStamina { get; private set; }

    [Header("Speed")]
    public float speed;

    [Header("Weight")]
    public float maxWeight = 300;

    [HideInInspector]
    public float currentWeight;

    ResourceBar healthBar;
    ResourceBar staminaBar;

    void Start()
    {
        currentStamina = maxStamina;

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        healthBar = PlayerManager.instance.playerHealthBar;
        staminaBar = PlayerManager.instance.playerStaminaBar;

        healthBar.SetMaxValue(maxHealth);
        staminaBar.SetMaxValue(maxStamina);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);

        healthBar.SetValue(currentHealth);
    }

    public override void TakeDamage(int damage, bool isCriticalHit = false)
    {
        base.TakeDamage(damage, isCriticalHit);

        healthBar.SetValue(currentHealth);
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
