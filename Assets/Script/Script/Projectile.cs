using UnityEngine;

public class Projectile : PlayerStats
{
    public float speed = 5f; // Vitesse du projectile
    public float lifeTime = 3f; // Durée de vie avant disparition
    public GameObject explosionProjectilePrefab; // Prefab des projectiles secondaires
    public int explosionCount = 3; // Nombre de projectiles créés lors de l'explosion

    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Exemple : Si le projectile touche un ennemi
        if (collision.CompareTag("Enemy"))
        {
            // Appliquer des dégâts à l'ennemi (si un script Enemy existe)
            collision.GetComponent<EnemyBehavior>()?.TakeDamage(damage);
            collision.GetComponent<StongEnemy>()?.TakeDamage(damage);

            // Créer des projectiles secondaires
            if (explosionProjectilePrefab != null)
                Explode();

            // Détruire le projectile
            Destroy(gameObject);
        }
    }
    void Explode()
    {
        // Calculer les directions des projectiles secondaires
        for (int i = 0; i < explosionCount; i++)
        {
            // Calculer un angle basé sur le nombre de projectiles
            float angle = i * (360f / explosionCount);
            Vector2 newDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

            // Instancier un projectile secondaire
            GameObject newProjectile = Instantiate(explosionProjectilePrefab, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().SetDirection(newDirection);
        }

        Debug.Log("Explosion avec " + explosionCount + " projectiles !");
    }

    public void Refresh(int Damage, float FireRate, float MovementSpeed )
    {
        damage = Damage;
        fireRate = FireRate;
        //movementSpeed = MovementSpeed;
        print("DAMAGE: " + damage + "::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
    }
}
