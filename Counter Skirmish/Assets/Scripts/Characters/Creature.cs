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
    
    /*public bool TakeDamage(Ability ability, Creature attacker)
     {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * ability.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);
        
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            return true;
        }
        
        return false;
     }
     
     public Ability GetRandomAbility()
     {
        int r = Random.Range(0, Abilities.Count);
        return Abilities[r];
     }*/
}
