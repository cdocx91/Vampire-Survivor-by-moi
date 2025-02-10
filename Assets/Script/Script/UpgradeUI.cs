using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    private UpgradeData upgrade;
    private System.Action<UpgradeData> onSelectCallback;
    public void Setup(UpgradeData upgradeData, System.Action<UpgradeData> onSelect)
    {
        upgrade = upgradeData;
        titleText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;
        iconImage.sprite = upgrade.icon;
        onSelectCallback = onSelect;
    }
    public void OnSelect()
    {
        onSelectCallback?.Invoke(upgrade);
        transform.parent.gameObject.SetActive(false); // Fermer l'UI après sélection
    }
}
