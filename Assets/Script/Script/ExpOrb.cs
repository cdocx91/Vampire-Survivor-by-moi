using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expAmount = 10; // Quantit� d'XP que cette orbe donne au joueur
    public float moveSpeed = 5f; // Vitesse � laquelle l'orbe se d�place vers le joueur
    private Transform player;

    void Start()
    {
        // Trouver le joueur dans la sc�ne
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

            // D�truire l'orbe apr�s r�cup�ration
            Destroy(gameObject);
        }
    }
}
