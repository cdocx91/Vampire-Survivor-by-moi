using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public float speed = 2f; // Vitesse de déplacement de l'ennemi

    public int maxHealth = 100; // Vie maximale de l'IA
    private int currentHealth;  // Vie actuelle de l'IA

    public GameObject expOrbPrefab; // Référence au prefab d'orbe d'expérience
    public int numberOfOrbs = 1; // Nombre d'orbes à faire apparaître

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
            // Calculer la direction vers le joueur
            Vector2 direction = (player.position - transform.position).normalized;

            // Déplacer l'ennemi vers le joueur
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy hit! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {
        Debug.Log("Enemy died!");
        // Désactiver ou détruire l'ennemi
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SpawnExpOrbs();
    }
    private void SpawnExpOrbs()
    {
        for (int i = 0; i < numberOfOrbs; i++)
        {
            // Position aléatoire autour de l'ennemi
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 spawnPosition = transform.position + (Vector3)randomOffset;
            Debug.Log("ERROR", this);
            // Créer une orbe d'expérience
            Instantiate(expOrbPrefab, spawnPosition, Quaternion.identity);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehavior>().TakeDamage(10); // Inflige 10 point de dégàts
        }
    }
}
