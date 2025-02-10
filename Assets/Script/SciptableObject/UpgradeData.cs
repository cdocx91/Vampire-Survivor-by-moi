using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName; // Nom de l'amélioration
    public string description; // Description
    public Sprite icon; // Icône de l'amélioration
    public float effectValue; // Valeur de l'effet (par exemple, +10% de dégâts)
    public UpgradeType upgradeType; // Type d'amélioration
    public int levelRequirement;

    public bool isExpPassive; // Ajout pour différencier cette amélioration
    public float expPerTick; // Quantité d'EXP gagnée à chaque intervalle

    public bool isHealthRegen; // Nouvelle amélioration : régénération de vie
    public float hpPerTick; // Points de vie récupérés par intervalle

    public enum UpgradeType
    {
        Damage,
        FireRate,
        MovementSpeed,
        Health,
        Attack,
        PassiveXp,
        HealthRegen
    }
}
