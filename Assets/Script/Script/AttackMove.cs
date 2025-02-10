using UnityEngine;

public class AttackMove : MonoBehaviour
{
    public Transform attackPoint; // Point d'origine de l'attaque
    public float attackRange = 0.5f; // Rayon de l'attaque
    public LayerMask enemyLayers; // Couches des ennemis
    public int attackDamage = 10; // Dégâts infligés
    private Animator animator;

    public float attackRate = 2f; // Nombre d'attaques par seconde
    private float nextAttackTime = 0f; // Temps avant la prochaine attaque
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate; // Gestion du délai entre attaques
            }
        }
    }
    void Attack()
    {
        // Animation d'attaque
        Debug.Log("Player Attacks!");

        // Détecter les ennemis dans la zone d'attaque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        animator.SetTrigger("Slash");
        // Infliger des dégâts à chaque ennemi touché
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"Hit {enemy.name}");
            enemy.GetComponent<EnemyBehavior>()?.TakeDamage(attackDamage); // Assurez-vous que vos ennemis ont une méthode TakeDamage
        }
    }

    // Dessiner le rayon dans l'éditeur pour visualiser la zone d'attaque
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}


