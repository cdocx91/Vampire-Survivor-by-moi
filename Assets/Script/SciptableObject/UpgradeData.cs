using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName; // Nom de l'am�lioration
    public string description; // Description
    public Sprite icon; // Ic�ne de l'am�lioration
    public float effectValue; // Valeur de l'effet (par exemple, +10% de d�g�ts)
    public UpgradeType upgradeType; // Type d'am�lioration
    public int levelRequirement;

    public bool isExpPassive; // Ajout pour diff�rencier cette am�lioration
    public float expPerTick; // Quantit� d'EXP gagn�e � chaque intervalle

    public bool isHealthRegen; // Nouvelle am�lioration : r�g�n�ration de vie
    public float hpPerTick; // Points de vie r�cup�r�s par intervalle

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
