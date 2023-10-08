using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceHud : MonoBehaviour
{
    [SerializeField] private Text nameText, levelText;
    [SerializeField] private HealthBar healthBar;

    public void SetData(Creature creature)
    {
        nameText.text = creature.Base.Name;
        levelText.text = "Lvl " + creature.Level;
        healthBar.SetHealth((float) creature.Health / creature.MaxHealth);
    }
}
