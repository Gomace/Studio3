using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image iconDisplay;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private HealthBar healthBar;

    public void SetData(Creature creature)
    {
        iconDisplay.sprite = creature.Base.Icon;
        levelText.text = creature.Level.ToString();
        healthBar.SetHealth((float) creature.Health / creature.MaxHealth);
    }
}
