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
        levelText.text = creature._level.ToString();
        healthBar.SetHealth((float) creature._health / creature.MaxHealth);
    }
}
