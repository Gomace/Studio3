using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Creature
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level;

    public InstanceUnit Unit { get; private set; }
    
    public CreatureBase Base => _base;
    public int Level => _level;
    
    public int Health { get; set; }
    public int Resource { get; set; }

    public Ability[] Abilities { get; set; } = new Ability[4];
    public Passive Passive { get; set; }

    public Dictionary<Stat, int> Stats { get; private set; }
    public Dictionary<Stat, int> StatBoosts { get; private set; }

    // Stats
    public int MaxHealth { get; private set; }
    public int MaxResource { get; private set; }

    public int Physical => GetStat(Stat.Physical);
    public int Magical => GetStat(Stat.Magical);
    public int Defense => GetStat(Stat.Defense);
    public int Resistance => GetStat(Stat.Resistance);
    public int Speed => GetStat(Stat.Speed);

    public void Initialize(InstanceUnit unit)
    {
        Unit = unit;

        // GENERATE MOVES
        for (int i = 0; i < Abilities.Length; ++i)
        {
            /*if (Abilities[i] == null) // TODO Fix when abilities are coming from the Hub
            {*/
                if (i < _base.LearnableAbilities.Length)
                {
                    if (_base.LearnableAbilities[i].Level <= _level)
                        Abilities[i] = new Ability(_base.LearnableAbilities[i].Base, this);
                }
                else
                    break;
            //}
            /*else
                ReplaceAbility();*/
        }
        
        if (Passive == null) // TODO Fix when passives are coming from the Hub
        {
            if (_base.PossiblePassives.Length > 0)
                Passive = new Passive(_base.PossiblePassives[Random.Range(0, _base.PossiblePassives.Length)].Base);
        }
        /*else
            ReplaceAbility();*/
        /*foreach (LearnableAbility ability in Base.LearnableAbilities)
        {
            if (ability.Level <= Level)
                Abilities.Add(new Ability(ability.Base));

            if (Abilities.Count >= 4)
                break;
        }*/
        
        CalculateStats();
        ClearBoosts();
        
        Health = MaxHealth;
        Resource = MaxResource;
    }

    public void PerformAbility(int slotNum, Vector3 mouse)
    {
        // Unit state = casting;
        Ability _ability = Abilities[slotNum]; // Get ability from creature
        
        if (_ability.Cooldown > 0)
            return;
        //Debug.Log($"Resource: {Resource}, Ability cost: {_ability.Base.Resource}, Ability: {_ability.Base.Name}, Power: {_ability.Base.Power}");
        if (Resource < _ability.Base.Resource)
            return;
        Resource -= _ability.Base.Resource; // Spend resource
        Unit.UpdateResource();
        
        _ability.Cooldown = _ability.Base.Cooldown; // Go on cooldown
        Unit.ActivateCooldown(_ability);

        _ability.Cast(mouse);
        //PlayAttackAnim(); // Attacking animation
        
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
    }
    
    public Ability GetRandomAbility()
    {
        int r = Random.Range(0, Abilities.Length - 1);
        return Abilities[r];
    }
    
    public void TakeDamage(Ability ability, Creature attacker)
    {
        //Debug.Log($"{attacker.Base.Name} attacked {Base.Name}");
        float critical = 1f;
        if (Random.Range(0f, 100f) <= 4f * (ability.Base.CritChance * attacker.Base.CritChance))
            critical = 1.5f * (ability.Base.CritDamage * attacker.Base.CritDamage);

        float metric = ability.Base.Metric(ability.Base.PowerSource(attacker, this));

        float modifiers = Random.Range(0.85f, 1f) * Base.GetEffectiveness(ability.Base.Type) * critical;
        float att = (2 * attacker.Level + 10) / 250f;
        float def = att * ability.Base.Power * Mathf.Max(ability.Base.Style(this, metric), 0f) + 2;
        int damage = Mathf.FloorToInt(def * modifiers);

        //Debug.Log($"The final damage is {damage}");
        Health -= damage;
        Unit.UpdateHealth();
        if (Health > 0)
            return;
        Debug.Log($"{attacker.Base.Name} killed {Base.Name} with {ability.Base.Name}");
        Health = 0;
        Unit.CreatureDead(attacker);
        // Death animation
    }
    
    private void CalculateStats()
    {
        Stats = new Dictionary<Stat, int>
        {
            { Stat.Physical, Mathf.FloorToInt((_base.Physical * _level) / 100f) + 5 },
            { Stat.Magical, Mathf.FloorToInt((_base.Magical * _level) / 100f) + 5 },
            { Stat.Defense, Mathf.FloorToInt((_base.Defense * _level) / 100f) + 5 },
            { Stat.Resistance, Mathf.FloorToInt((_base.Resistance * _level) / 100f) + 5 },
            { Stat.Speed, Mathf.FloorToInt((_base.Speed * _level) / 100f) + 5 }
        };

        MaxHealth = Mathf.FloorToInt((_base.MaxHealth * _level) / 100f) + 10;
        MaxResource = Mathf.FloorToInt((_base.MaxResource * 0.2f) + ((_base.MaxResource * 0.8f) * _level) / 100f) + 10;
    }

    private int GetStat(Stat stat)
    {
        int _statVal = Stats[stat];
        
        // Apply stat boost
        int _boost = StatBoosts[stat];
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

            StatBoosts[_stat] = Mathf.Clamp(StatBoosts[_stat] + _boost, -6, 6);
        }
    }
    
    private void ClearBoosts()
    {
        StatBoosts = new Dictionary<Stat, int>()
        {
            {Stat.Physical, 0},
            {Stat.Magical, 0},
            {Stat.Defense, 0},
            {Stat.Resistance, 0},
            {Stat.Speed, 0}
        };
    }
}