using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public CreatureBase Base { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    
    public List<Ability> Abilities { get; set; }
    
    public int MaxHealth => Mathf.FloorToInt((Base.MaxHealth * Level) / 100f) + 10;
    public int Physical => Mathf.FloorToInt((Base.Physical * Level) / 100f) + 5;
    public int Magical => Mathf.FloorToInt((Base.Magical * Level) / 100f) + 5;
    public int Defense => Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;
    public int Resistance => Mathf.FloorToInt((Base.Resistance * Level) / 100f) + 5;
    public int Speed => Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;

    public Creature(CreatureBase cBase, int cLevel)
    {
        Base = cBase;
        Level = cLevel;
        Health = MaxHealth;

        // GENERATE MOVES
        Abilities = new List<Ability>();
        foreach (LearnableAbility ability in Base.LearnableAbilities)
        {
            if (ability.Level <= Level)
                Abilities.Add(new Ability(ability.Base));

            if (Abilities.Count >= 4)
                break;
        }
    }
}
