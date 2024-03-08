using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Creature
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level;

    private InstanceUnit _unit;
    
    private int _maxHealth, _maxResource;
    
    private Dictionary<Stat, int> _stats, _statBoosts;

    public CreatureBase Base => _base;
    public int Level => _level;
    
    public int Health { get; set; }
    public int Resource { get; set; }

    public Ability[] Abilities { get; set; } = new Ability[4];

    public Dictionary<Stat, int> Stats => _stats;
    public Dictionary<Stat, int> StatBoosts => _statBoosts;

    // Stats
    public int MaxHealth => _maxHealth;
    public int MaxResource => _maxResource;
    
    public int Physical => GetStat(Stat.Physical);
    public int Magical => GetStat(Stat.Magical);
    public int Defense => GetStat(Stat.Defense);
    public int Resistance => GetStat(Stat.Resistance);
    public int Speed => GetStat(Stat.Speed);

    public void Initialize(InstanceUnit unit)
    {
        _unit = unit;

        // GENERATE MOVES
        for (int i = 0; i < Abilities.Length; ++i)
        {
            /*if (Abilities[i] == null) // TODO Fix when abilities are coming from the Hub
            {*/
                if (i < _base.LearnableAbilities.Length)
                {
                    if (_base.LearnableAbilities[i].Level <= _level)
                        Abilities[i] = new Ability(_base.LearnableAbilities[i].Base);
                }
                else
                    break;
            //}
            /*else
                ReplaceAbility();*/
        }
        /*foreach (LearnableAbility ability in Base.LearnableAbilities)
        {
            if (ability.Level <= Level)
                Abilities.Add(new Ability(ability.Base));

            if (Abilities.Count >= 4)
                break;
        }*/
        
        CalculateStats();
        ClearBoosts();
        
        Health = _maxHealth;
        Resource = _maxResource;
    }

    public void CastAbility(int slotNum, bool modifier)
    {
        if (Abilities[slotNum] == null)
            return;
        
        // Unit state = casting;
        Ability _ability = Abilities[slotNum]; // Get ability from creature
        
        if (Resource < _ability.Base.Resource)
            return;
        Resource -= _ability.Base.Resource; // Spend resource
        
        _ability.Cast(_unit, this, modifier);
        if (modifier)
            return;
        
        /* TODO maybe make ScriptObj for AbilityEffects
        if (ability.Base.Category == abilityCategory.Status)
        {
            var effects = ability.Base.Effects;
            if (effects.Boosts != null)
            {
                if (ability.Base.Target == AbilityTarget.Self)
                    sourceUnit.Creature.ApplyBoosts(effects.Boosts);
                else
                    targetUnit.Creature.ApplyBoosts(effects.Boosts);
            }
        }*/
        
        //_enemyUnit.PlayHitAnim(); // Play this on its own by the enemy, not here
        //_enemyUnit.Creature.TakeDamage(ability, _playerUnit.Creature);
        //_enemyHud.UpdateHealth();
        
        //PlayAttackAnim(); // Attacking animation
    }
    
    public Ability GetRandomAbility()
    {
        int r = Random.Range(0, Abilities.Length - 1);
        return Abilities[r];
    }
    
    public void TakeDamage(Ability ability, Creature attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= 4f * (ability.Base.CritChance * attacker.Base.CritChance))
            critical = 1.5f * (ability.Base.CritDamage * attacker.Base.CritDamage);
        
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
    
    private void CalculateStats()
    {
        _stats = new Dictionary<Stat, int>
        {
            { Stat.Physical, Mathf.FloorToInt((_base.Physical * _level) / 100f) + 5 },
            { Stat.Magical, Mathf.FloorToInt((_base.Magical * _level) / 100f) + 5 },
            { Stat.Defense, Mathf.FloorToInt((_base.Defense * _level) / 100f) + 5 },
            { Stat.Resistance, Mathf.FloorToInt((_base.Resistance * _level) / 100f) + 5 },
            { Stat.Speed, Mathf.FloorToInt((_base.Speed * _level) / 100f) + 5 }
        };

        _maxHealth = Mathf.FloorToInt((_base.MaxHealth * _level) / 100f) + 10;
        _maxResource = Mathf.FloorToInt((_base.MaxResource * _level) / 100f) + 10;
    }

    private int GetStat(Stat stat)
    {
        int _statVal = _stats[stat];
        
        // Apply stat boost
        int _boost = _statBoosts[stat];
        float[] _boostValues = { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        if (_boost >= 0)
            _statVal = Mathf.FloorToInt(_statVal * _boostValues[_boost]);
        else
            _statVal = Mathf.FloorToInt(_statVal / _boostValues[-_boost]);

        return _statVal;
    }

    private void ApplyBoosts(StatBoost[] statBoosts)
    {
        foreach (StatBoost statBoost in statBoosts)
        {
            Stat _stat = statBoost.Stat;
            int _boost = statBoost.Boost;

            _statBoosts[_stat] = Mathf.Clamp(_statBoosts[_stat] + _boost, -6, 6);
        }
    }
    
    private void ClearBoosts()
    {
        _statBoosts = new Dictionary<Stat, int>()
        {
            {Stat.Physical, 0},
            {Stat.Magical, 0},
            {Stat.Defense, 0},
            {Stat.Resistance, 0},
            {Stat.Speed, 0}
        };
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}
