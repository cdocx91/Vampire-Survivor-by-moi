using UnityEngine;

public class StongEnemy : EnemyBehavior
{
    public GameObject deathEffect; // Effet visuel à la mort

    override protected void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        base.Die(); 
    }
}
