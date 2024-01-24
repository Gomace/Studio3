using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    private CreatureBase _base;
    
    public int _level;

    public int _health;
    
    
    public CreatureBase Base => _base;
    
    public int Level => _level;
    
    public int Health => _health;
    
    public List<Ability> Abilities { get; set; }

    public Creature(CreatureBase cBase, int cLevel)
    {
        _base = cBase;
        _level = cLevel;
        _health = MaxHealth;

        // GENERATE MOVES
        Abilities = new List<Ability>();
        foreach (LearnableAbility ability in _base.LearnableAbilities)
        {
            if (ability.Level <= _level)
                Abilities.Add(new Ability(ability.Base));

            if (Abilities.Count >= 4)
                break;
        }
    }

    public int MaxHealth => Mathf.FloorToInt((_base.MaxHealth * _level) / 100f) + 10;
    public int Physical => Mathf.FloorToInt((_base.Physical * _level) / 100f) + 5;
    public int Magical => Mathf.FloorToInt((_base.Magical * _level) / 100f) + 5;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level) / 100f) + 5;
    public int Resistance => Mathf.FloorToInt((_base.Resistance * _level) / 100f) + 5;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level) / 100f) + 5;
}
