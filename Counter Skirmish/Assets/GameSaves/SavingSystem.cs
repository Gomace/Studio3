using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{

    public static void SaveProgress(Creature creature)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/creature.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData(creature);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProgressData LoadProgress()
    {
        string path = Application.persistentDataPath + "/creature.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData data = formatter.Deserialize(stream) as ProgressData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }
}

/*
 public void SavePlayer()
 {
    SaveSystem.SaveProgress(this);
 }
 
 public void LoadProgress()
 {
    ProgressData data = SaveSystem.LoadProgress();
    
    _level = data.level;
    _health = data.health;
    
    Vector3 position;
    position.x = data.position[0];
    position.y = data.position[1];
    position.z = data.position[2];
    transform.position = position;
 */
