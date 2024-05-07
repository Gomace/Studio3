using UnityEngine;
using System;
using System.IO;
using System.Linq;

//using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    private const string _jsonFolder = "/SaveData/Json";
    
    public static void SaveToJson<T>(T data, string path)
    {
        string folder = Application.persistentDataPath + _jsonFolder;
        
        if (!Directory.Exists(folder)) // Make sure save folders exist
            Directory.CreateDirectory(folder);
        
        //Debug.Log("I'm saving Json");
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(folder + path, jsonData);
    }

    public static T LoadFromJson<T>(string path)
    {
        string folder = Application.persistentDataPath + _jsonFolder;
        //Debug.Log("I'm in LoadFromJson");

        if (File.Exists(folder + path)) // Application.persistentDataPath + "/SaveData/Json/SaveData.json"
        {
            string jsonData = File.ReadAllText(folder + path);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            Debug.LogError($"Save file not found in {folder + path}");
            return default(T);
        }
    }

    /*public static void SaveToBinary<T>(T data, string path) // Binary
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T LoadFromBinary<T>(string path) where T : class
    {        
        if (File.Exists(path)) // Application.persistentDataPath + "/SaveData/Binary/CreatureSave.bin";
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T data = formatter.Deserialize(stream) as T;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return default(T);
        }
    }*/
}

[Serializable]
public class ProgressData
{
    public bool NewGame { get; private set; }

    public ProgressData()
    {
        
    }
}

[Serializable]
public class CollectionData // All unequipped creatures
{
    public string[] Names; // File name of CreatureBase
    public int[] Levels;
    public int[] Exps;

    public AbilityNames[] Abilities;
    public string[] Passives;
    
    public AbilityNames[] LearnedAbilities;

    public void ApplyCreature(Creature[] creatures)
    {
        int length = 0,
            i = 0;

        length += creatures.Where(creature => creature != null).Count(creature => creature.Base != null);

        ArrayLengths(length);

        foreach (Creature creature in creatures)
        {
            if (creature == null)
                continue;

            if (creature.Base == null)
                continue;
            
            Names[i] = creature.Base.name;
            Levels[i] = creature.Level;
            Exps[i] = creature.Exp;

            Passives[i] = creature.Passive.Base.name;

            if (creature.LearnedAbilities != null)
                LearnedAbilities[i] = new AbilityNames(creature.LearnedAbilities.Count(ability => ability != null))
                {
                    Names = creature.LearnedAbilities.ToArray()
                };

            int abilities = 0,
                l = 0;

            abilities += creature.Abilities.Count(ability => ability != null);

            Abilities[i] = new AbilityNames(abilities);

            foreach (Ability ability in creature.Abilities)
            {
                if (ability == null)
                    continue;
                
                Abilities[i].Names[l++] = ability.Base.name;
            }
            
            ++i;
        }
    }
    
    public void ApplyCreatureInfo(CreatureInfo[] creatures)
    {
        int length = 0,
            i = 0;

        length += creatures.Where(creature => creature != null).Count(creature => creature.Base != null);

        ArrayLengths(length);

        foreach (CreatureInfo creature in creatures)
        {
            if (creature == null)
                continue;
            
            if (creature.Base == null)
                continue;
            
            Names[i] = creature.Base.name;
            Levels[i] = creature.Level;
            Exps[i] = creature.Exp;

            Passives[i] = creature.PassiveBase.name;
            
            int abilities = 0,
                l = 0;

            abilities += creature.AbilityBases.Count(ability => ability != null);

            Abilities[i] = new AbilityNames(abilities);

            foreach (AbilityBase ability in creature.AbilityBases)
            {
                if (ability == null)
                    continue;
                
                Abilities[i].Names[l++] = ability.name;
            }
            
            if (creature.LearnedAbilities != null)
            {
                int learned = 0,
                    s = 0;

                learned += creature.LearnedAbilities.Count(ability => ability != null);

                LearnedAbilities[i] = new AbilityNames(learned);

                foreach (AbilityBase ability in creature.LearnedAbilities)
                {
                    if (ability == null)
                        continue;

                    LearnedAbilities[i].Names[s++] = ability.name;
                }
            }
            
            ++i;
        }
    }

    private void ArrayLengths(int length)
    {
        Names = new string[length];
        Levels = new int[length];
        Exps = new int[length];

        Abilities = new AbilityNames[length];
        Passives = new string[length];
        LearnedAbilities = new AbilityNames[length];
    }
}

[Serializable]
public class RosterData // Equipped creatures
{
    public string[] Names; // File name of CreatureBase
    public int[] Levels;
    public int[] Exps;
    
    public bool[] Rental;
    
    public AbilityNames[] Abilities;
    public string[] Passives;

    public AbilityNames[] LearnedAbilities;
    
    public void ApplyCreature(Creature[] creatures)
    {
        int length = 0,
            i = 0;

        length += creatures.Where(creature => creature != null).Count(creature => creature.Base != null);

        ArrayLengths(length);

        foreach (Creature creature in creatures)
        {
            if (creature == null)
                continue;

            if (creature.Base == null)
                continue;
            
            Names[i] = creature.Base.name;
            Levels[i] = creature.Level;
            Exps[i] = creature.Exp;

            Rental[i] = creature.Rental;
            
            Passives[i] = creature.Passive.Base.name;

            if (creature.LearnedAbilities != null)
                LearnedAbilities[i] = new AbilityNames(creature.LearnedAbilities.Count(ability => ability != null))
                {
                    Names = creature.LearnedAbilities.ToArray()
                };

            int abilities = 0,
                l = 0;

            abilities += creature.Abilities.Count(ability => ability != null);

            Abilities[i] = new AbilityNames(abilities);

            foreach (Ability ability in creature.Abilities)
            {
                if (ability == null)
                    continue;
                
                Abilities[i].Names[l++] = ability.Base.name;
            }
            
            ++i;
        }
    }
    
    public void ApplyCreatureInfo(CreatureInfo[] creatures)
    {
        int length = 0,
            i = 0;

        length += creatures.Where(creature => creature != null).Count(creature => creature.Base != null);

        ArrayLengths(length);

        foreach (CreatureInfo creature in creatures)
        {
            if (creature == null)
                continue;
            
            if (creature.Base == null)
                continue;
            
            Names[i] = creature.Base.name;
            Levels[i] = creature.Level;
            Exps[i] = creature.Exp;

            Rental[i] = creature.Rental;

            Passives[i] = creature.PassiveBase.name;
            
            int abilities = 0,
                l = 0;

            abilities += creature.AbilityBases.Count(ability => ability != null);

            Abilities[i] = new AbilityNames(abilities);

            foreach (AbilityBase ability in creature.AbilityBases)
            {
                if (ability == null)
                    continue;
                
                Abilities[i].Names[l++] = ability.name;
            }

            if (creature.LearnedAbilities != null)
            {
                int learned = 0,
                    s = 0;

                learned += creature.LearnedAbilities.Count(ability => ability != null);

                LearnedAbilities[i] = new AbilityNames(learned);

                foreach (AbilityBase ability in creature.LearnedAbilities)
                {
                    if (ability == null)
                        continue;

                    LearnedAbilities[i].Names[s++] = ability.name;
                }
            }

            ++i;
        }
    }

    private void ArrayLengths(int length)
    {
        Names = new string[length];
        Levels = new int[length];
        Exps = new int[length];

        Rental = new bool[length];

        Abilities = new AbilityNames[length];
        Passives = new string[length];
        LearnedAbilities = new AbilityNames[length];
    }
}

[Serializable]
public class AbilityNames
{
    public string[] Names;
    
    public AbilityNames(int amount) => Names = new string[amount];
}