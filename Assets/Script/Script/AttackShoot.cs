using UnityEngine;

public class AttackShoot : PlayerStats
{
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firePoint; // Le point de tir (position o� le projectile appara�t)

    private float cooldownTimer = 0f;

    void Update()
    {
        // Compte � rebours pour l'attaque
        cooldownTimer -= Time.deltaTime;

        // Si le cooldown est termin�, lancer un projectile
        if (cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = fireRate; // R�initialiser le cooldown
        }
    }
    private void Start()
    {
     
    }
    void Shoot()
    {
        // Instancier le projectile au point de tir
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Projectile>().damage = damage;

        // Calculer la direction (par exemple, en fonction de la souris ou d'une direction d�finie)
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;

        // D�finir la direction dans le script du projectile
        projectile.GetComponent<Projectile>().SetDirection(direction);

        Debug.Log("Projectile lanc� !");
    }
    public void Refresh(int Damage, float FireRate, float MovementSpeed )
    {
        damage = Damage;
        fireRate = FireRate;
        //movementSpeed = MovementSpeed;
    }
}
