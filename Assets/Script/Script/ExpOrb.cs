using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expAmount = 10; // Quantité d'XP que cette orbe donne au joueur
    public float moveSpeed = 5f; // Vitesse à laquelle l'orbe se déplace vers le joueur
    private Transform player;

    void Start()
    {
        // Trouver le joueur dans la scène
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Faire en sorte que l'orbe se dirige vers le joueur
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ajouter l'XP au joueur
            collision.GetComponent<PlayerLevelSystem>().GainXP(expAmount);

            // Détruire l'orbe après récupération
            Destroy(gameObject);
        }
    }
}
