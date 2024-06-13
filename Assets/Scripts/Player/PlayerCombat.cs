using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.GetComponent<CharacterStats>())
                Attack(enemy.GetComponent<CharacterStats>());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
