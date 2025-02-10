using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;



public class PlayerBehavior : PlayerStats

{
    Rigidbody2D rb;
    
    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    private Color originalColor;
    public ParticleSystem damageParticles;
    public HealthBar healthBar;
    private bool hasHealthRegen = false;
    private float hpPerTick = 0;
  
    public Vector2 LaserDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (healthData != null)
        {
            healthData.Initialize(); // Initialiser la vie
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Stocker la couleur d'origine
        }
        healthBar = GetComponent<HealthBar>();
        StartCoroutine(HealthRegen());
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * movementSpeed;
        LaserDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Vous n'aurez peut-être pas du tout besoin de worldPoint2d. Utilisez simplement worldPoint.x et worldPoint.y dans vos calculs. 
    }
    public void TakeDamage(int damage)
    {
        if (healthData != null)
        {
            healthData.TakeDamage(damage);
            Debug.Log($"You took damage !! Current health: {healthData.currentHealth}");
            healthBar.SmoothUpdateHealthBar();

            if (spriteRenderer != null)
            {
                StartCoroutine(FlashDamage());
            }

            if (damageParticles != null)
            {
                damageParticles.Play(); // Activer les particules
            }

            if (healthData.currentHealth <= 0)
            {
                Die();
            }
        }
    }
    private System.Collections.IEnumerator FlashDamage()
    {
        // Changer la couleur pour simuler un flash
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // Pause
        spriteRenderer.color = originalColor;
    }
    public void ActivateHealthRegen(float amount)
    {
        if (!hasHealthRegen)
        {
            hasHealthRegen = true;
            hpPerTick = amount;
        }
    }

    private IEnumerator HealthRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Régénère toutes les 2 secondes
            if (hasHealthRegen)
            {
                healthData.Heal(Mathf.RoundToInt(hpPerTick));
            }
        }
    }

    void Die()
    {

        Debug.Log("You are dead!");
        SceneManager.LoadScene("Main Menu");
        // Désactiver ou détruire l'ennemi
    }

    public void Refresh(int Damage,float FireRate,float MovementSpeed )
    {
        //damage = Damage;
        //fireRate = FireRate;
        movementSpeed = MovementSpeed;
        //maxHealth = MaxHealth;
    }
}
