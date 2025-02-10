using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrashAttack : MonoBehaviour
{
    public float cooldownTime = 30f; // Temps de récupération en secondes
    public float glitchDuration = 1f; // Durée de l'effet de glitch
    public GameObject glitchEffectPrefab; // Effet visuel de glitch
    public Camera mainCamera; // Référence à la caméra principale
    public LayerMask enemyLayer; // Layer des ennemis
    public int minEnemiesToTrigger = 5; // Nombre minimum d'ennemis pour activer l'attaque

    private bool canUseCrash = false; // Si l'attaque est débloquée
    private bool isOnCooldown = false; // Si l'attaque est en récupération

    private void Update()
    {
        if (canUseCrash && !isOnCooldown)
        {
            // Vérifie s'il y a suffisamment d'ennemis dans la caméra pour activer l'attaque
            if (ShouldTriggerCrash())
            {
                StartCoroutine(PerformCrash());
            }
        }
    }

    // Débloque l'attaque via le système d'améliorations
    public void UnlockCrash()
    {
        canUseCrash = true;
    }

    private bool ShouldTriggerCrash()
    {
        // Obtenir les limites de la caméra
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Trouver tous les ennemis dans les limites de la caméra
        Collider2D[] enemies = Physics2D.OverlapAreaAll(bottomLeft, topRight, enemyLayer);

        // Vérifier si le nombre d'ennemis dépasse le seuil défini
        return enemies.Length >= minEnemiesToTrigger;
    }

    private IEnumerator PerformCrash()
    {
        isOnCooldown = true;

        // 1. Activer l'effet visuel de glitch
        GameObject glitchEffect = Instantiate(glitchEffectPrefab, mainCamera.transform.position, Quaternion.identity);
        glitchEffect.transform.SetParent(mainCamera.transform);

        // 2. Détruire les ennemis visibles
        KillVisibleEnemies();

        // 3. Attendre la durée de l'effet
        yield return new WaitForSeconds(glitchDuration);

        // 4. Supprimer l'effet visuel
        Destroy(glitchEffect);

        // 5. Attendre le temps de récupération
        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
    }

    private void KillVisibleEnemies()
    {
        // Obtenir les limites de la caméra
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Trouver tous les ennemis dans les limites de la caméra
        Collider2D[] enemies = Physics2D.OverlapAreaAll(bottomLeft, topRight, enemyLayer);

        // Détruire tous les ennemis trouvés
        foreach (Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
