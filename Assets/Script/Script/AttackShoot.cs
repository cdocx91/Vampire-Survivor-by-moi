using UnityEngine;

public class AttackShoot : PlayerStats
{
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firePoint; // Le point de tir (position où le projectile apparaît)

    private float cooldownTimer = 0f;

    void Update()
    {
        // Compte à rebours pour l'attaque
        cooldownTimer -= Time.deltaTime;

        // Si le cooldown est terminé, lancer un projectile
        if (cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = fireRate; // Réinitialiser le cooldown
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

        // Calculer la direction (par exemple, en fonction de la souris ou d'une direction définie)
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;

        // Définir la direction dans le script du projectile
        projectile.GetComponent<Projectile>().SetDirection(direction);

        Debug.Log("Projectile lancé !");
    }
    public void Refresh(int Damage, float FireRate, float MovementSpeed )
    {
        damage = Damage;
        fireRate = FireRate;
        //movementSpeed = MovementSpeed;
    }
}
