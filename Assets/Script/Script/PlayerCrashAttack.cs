using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrashAttack : MonoBehaviour
{
    public float cooldownTime = 30f; // Temps de r�cup�ration en secondes
    public float glitchDuration = 1f; // Dur�e de l'effet de glitch
    public GameObject glitchEffectPrefab; // Effet visuel de glitch
    public Camera mainCamera; // R�f�rence � la cam�ra principale
    public LayerMask enemyLayer; // Layer des ennemis
    public int minEnemiesToTrigger = 5; // Nombre minimum d'ennemis pour activer l'attaque

    private bool canUseCrash = false; // Si l'attaque est d�bloqu�e
    private bool isOnCooldown = false; // Si l'attaque est en r�cup�ration

    private void Update()
    {
        if (canUseCrash && !isOnCooldown)
        {
            // V�rifie s'il y a suffisamment d'ennemis dans la cam�ra pour activer l'attaque
            if (ShouldTriggerCrash())
            {
                StartCoroutine(PerformCrash());
            }
        }
    }

    // D�bloque l'attaque via le syst�me d'am�liorations
    public void UnlockCrash()
    {
        canUseCrash = true;
    }

    private bool ShouldTriggerCrash()
    {
        // Obtenir les limites de la cam�ra
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Trouver tous les ennemis dans les limites de la cam�ra
        Collider2D[] enemies = Physics2D.OverlapAreaAll(bottomLeft, topRight, enemyLayer);

        // V�rifier si le nombre d'ennemis d�passe le seuil d�fini
        return enemies.Length >= minEnemiesToTrigger;
    }

    private IEnumerator PerformCrash()
    {
        isOnCooldown = true;

        // 1. Activer l'effet visuel de glitch
        GameObject glitchEffect = Instantiate(glitchEffectPrefab, mainCamera.transform.position, Quaternion.identity);
        glitchEffect.transform.SetParent(mainCamera.transform);

        // 2. D�truire les ennemis visibles
        KillVisibleEnemies();

        // 3. Attendre la dur�e de l'effet
        yield return new WaitForSeconds(glitchDuration);

        // 4. Supprimer l'effet visuel
        Destroy(glitchEffect);

        // 5. Attendre le temps de r�cup�ration
        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
    }

    private void KillVisibleEnemies()
    {
        // Obtenir les limites de la cam�ra
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Trouver tous les ennemis dans les limites de la cam�ra
        Collider2D[] enemies = Physics2D.OverlapAreaAll(bottomLeft, topRight, enemyLayer);

        // D�truire tous les ennemis trouv�s
        foreach (Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
