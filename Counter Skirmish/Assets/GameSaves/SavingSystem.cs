using UnityEngine;
using System;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    public static void SaveToJson<T>(T data, string path)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
    }

    public static T LoadFromJson<T>(string path)
    {
        if (File.Exists(path)) // Application.persistentDataPath + "/SaveData/Json/SaveData.json"
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
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
public class CollectionData // All creatures
{
    private int _level;
    
    public CollectionData(Creature creature)
    {
        _level = creature.Level;
    }
}

[Serializable]
public class RosterData // Equipped creatures
{
    public string[] Names { get; private set; } // File name of CreatureBase
    public int[] Levels { get; private set; }
    public int[] Exps { get; private set; }

    public string[][] Abilities { get; private set; }
    public string[] Passives { get; private set; }
    
    public RosterData(Creature[] creatures)
    {
        int length = creatures.Length;
        
        Names = Passives = new string[length];
        Levels = Exps = new int[length];

        Abilities = new string[length][];

        for (int i = 0; i < length; ++i)
        {
            Names[i] = creatures[i].Base.name;
            Levels[i] = creatures[i].Level;
            Exps[i] = creatures[i].Exp;

            Passives[i] = creatures[i].Passive.Base.name;
            
            int abilities = creatures[i].Abilities.Length;
            Abilities[i] = new string[abilities];
            
            for (int l = 0; l < abilities; ++l)
                Abilities[i][l] = creatures[i].Abilities[l].Base.name;
        }
    }
    public RosterData(CreatureInfo[] creatures)
    {
        int length = creatures.Length;
        
        Names = Passives = new string[length];
        Levels = Exps = new int[length];

        Abilities = new string[length][];

        for (int i = 0; i < length; ++i)
        {
            Names[i] = creatures[i].Base.name;
            Levels[i] = creatures[i].Level;
            Exps[i] = creatures[i].Exp;

            Passives[i] = creatures[i].PassiveBase.name;
            
            int abilities = creatures[i].AbilityBases.Length;
            Abilities[i] = new string[abilities];
            
            for (int l = 0; l < abilities; ++l)
                Abilities[i][l] = creatures[i].AbilityBases[l].name;
        }
    }
}

/*
public void SaveProgress()
{
    SavingSystem.SaveToJson(this);
}
 
public void LoadProgress()
{
    RosterData data = SaveSystem.LoadFromJson();
    
    _level = data.level;
    
    Vector3 position;
    position.x = data.position[0];
    position.y = data.position[1];
    position.z = data.position[2];
    transform.position = position;
}
*/