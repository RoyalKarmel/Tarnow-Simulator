using UnityEngine;

public class PlayerStats : CharacterStats
{
    [Header("Stamina")]
    public float maxStamina;
    public float staminaCost = 10f;

    [HideInInspector]
    public float currentStamina;

    [Header("Speed")]
    public float moveSpeed;
    public float sprintSpeed;
    float overweightSpeed;

    [HideInInspector]
    public float speed;

    public bool isOverWeight { get; private set; }

    // Resource bars
    ResourceBar healthBar;
    public ResourceBar staminaBar { get; private set; }

    void Start()
    {
        speed = moveSpeed;
        overweightSpeed = moveSpeed / 2;
        currentStamina = maxStamina;

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        healthBar = PlayerManager.instance.playerHealthBar;
        staminaBar = PlayerManager.instance.playerStaminaBar;

        healthBar.SetMaxValue(maxHealth);
        staminaBar.SetMaxValue((int)maxStamina);
    }

    void Update()
    {
        isOverWeight = Inventory.instance.currentWeight > Inventory.instance.maxWeight;

        if (isOverWeight)
            speed = overweightSpeed;
        else
            speed = moveSpeed;

        if (!Input.GetButton("Sprint"))
            RegenerateStamina();
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

    #region CharacterStats methods
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
    #endregion

    void RegenerateStamina()
    {
        currentStamina += staminaCost * Time.deltaTime;

        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        staminaBar.SetValue((int)currentStamina);
    }
}
