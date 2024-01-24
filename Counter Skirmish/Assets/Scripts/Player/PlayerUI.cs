using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private HealthBar _healthBar;

    public void SetData(Creature creature)
    {
        _icon.sprite = creature.Base.Icon;
        _level.text = creature.Level.ToString();
        _healthBar.SetHealth((float) creature.Health / creature.MaxHealth);
    }
}
