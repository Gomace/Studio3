using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creature
{
    public CreatureBase Base { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Resource { get; set; }
    
    public List<Ability> Abilities { get; set; }
    
    public int MaxHealth => Mathf.FloorToInt((Base.MaxHealth * Level) / 100f) + 10;
    public int MaxResource => Mathf.FloorToInt((Base.MaxResource * Level) / 100f) + 10;
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
        Resource = MaxResource;

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
    
    public void PerformPlayerAbility()
    {
        // Unit state = casting;

        Ability ability = Abilities[0]; // Catch ability from creature
        Resource--; // Spend resource
        
        //PlayAttackAnim(); // Attacking animation
        
        //_enemyUnit.PlayHitAnim(); // Play this on its own by the enemy, not here
        //_enemyUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        //_enemyHud.UpdateHealth();
    }
    
    public void TakeDamage(Ability ability, Creature attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= 4f /** ability.CritChance * attacker.CritDamage*/)
            critical = 1.5f/* * ability.CritDamage * attacker.CritDamage*/;
        
        float type = TypeChart.GetEffectiveness(CreatureType.Earth/*ability.Base.Type*/, CreatureType.Arcane/*this.Base.Type1*/) * TypeChart.GetEffectiveness(CreatureType.Energy/*ability.Base.Type*/, CreatureType.Dark/*this.Base.Type2*/);

        DamageDetails damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };

        //float attack = (ability.Base.IsSpecial) ? attacker.Magical : attacker.Physical;
        //float defense = (ability.Base.IsSpecial) ? Resistance : Defense;
        
        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * ability.Base.Power * ((float)attacker.Physical / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            damageDetails.Fainted = true;
        }
    }
     
     public Ability GetRandomAbility()
     {
        int r = Random.Range(0, Abilities.Count);
        return Abilities[r];
     }
}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}
