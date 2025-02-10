using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeData[] availableUpgrades; // Liste des am�liorations possibles
    public Transform upgradeUIParent; // Parent des boutons ou cartes d'am�lioration dans l'UI
    public GameObject upgradeUIPrefab; // Prefab d'une carte d'am�lioration
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public PlayerStats playerStats;
    public HealthData maxHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowUpgradeOptions()
    {
        // Supprimer les anciennes options
        foreach (Transform child in upgradeUIParent)
        {
            Destroy(child.gameObject);
        }
        upgradeUIParent.gameObject.SetActive(true);
        // S�lectionner 3 am�liorations au hasard
        for (int i = 0; i < 3; i++)
        {
            UpgradeData upgrade = availableUpgrades[Random.Range(0, availableUpgrades.Length)];

            // Cr�er une carte d'am�lioration
            GameObject upgradeUI = Instantiate(upgradeUIPrefab, upgradeUIParent);
            upgradeUI.GetComponent<UpgradeUI>().Setup(upgrade, ApplyUpgrade);
        }
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        // Appliquer l'effet de l'am�lioration
        switch (upgrade.upgradeType)
        {
            case UpgradeData.UpgradeType.Damage:
                // Exemple : augmenter les d�g�ts
                playerStats.damage += (int)upgrade.effectValue;
                break;

            case UpgradeData.UpgradeType.FireRate:
                playerStats.fireRate -= upgrade.effectValue;
                break;

            case UpgradeData.UpgradeType.MovementSpeed:
                playerStats.movementSpeed += upgrade.effectValue;
                break;

            case UpgradeData.UpgradeType.Health:
                maxHealth.currentHealth += (int)upgrade.effectValue;
                break;

            case UpgradeData.UpgradeType.Attack:
                PlayerCrashAttack crashAttack = FindFirstObjectByType<PlayerCrashAttack>();
                if (crashAttack != null)
                {
                    crashAttack.UnlockCrash();
                }
                break;

            case UpgradeData.UpgradeType.PassiveXp:
                PlayerLevelSystem hasExpPassive = FindFirstObjectByType<PlayerLevelSystem>();
                if (hasExpPassive != null)
                {
                    FindFirstObjectByType<PlayerLevelSystem>().ActivateExpPassive(upgrade.expPerTick);
                }
                break;

            case UpgradeData.UpgradeType.HealthRegen:
                PlayerBehavior hasHealthRegen = FindFirstObjectByType<PlayerBehavior>();
                if (hasHealthRegen != null)
                {
                    FindFirstObjectByType<PlayerBehavior>().ActivateHealthRegen(upgrade.hpPerTick);
                }
                break;





        }
        Time.timeScale = 1;
        Debug.Log($"Am�lioration appliqu�e : {upgrade.upgradeName}");
    }
}

