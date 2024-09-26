using UnityEngine;
using System;
using System.IO;

public static class SettingsSaver
{
    private const string _jsonFolder = "/Settings";
    
    public static void SaveToJson<T>(T data, string path)
    {
        string folder = Application.persistentDataPath + _jsonFolder;
        
        if (!Directory.Exists(folder)) // Make sure settings folders exist
            Directory.CreateDirectory(folder);
        
        //Debug.Log("I'm saving settings Json");
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(folder + path, jsonData);
    }

    public static T LoadFromJson<T>(string path)
    {
        string folder = Application.persistentDataPath + _jsonFolder;
        //Debug.Log("I'm in LoadFromJson");

        if (File.Exists(folder + path)) // Application.persistentDataPath + "/Settings/GameSettings.json"
        {
            string jsonData = File.ReadAllText(folder + path);
            return JsonUtility.FromJson<T>(jsonData);
        }
        
        Debug.LogError($"Settings file not found in {folder + path}");
        return default;
    }
}

[Serializable]
public class AudioData
{
    public float Master, Music, SFX;

    public AudioData()
    {
        Master = 1f;
        Music = 0.5f;
        SFX = 0.75f;
    }
    
    public AudioData(float master, float music, float sfx)
    {
        Master = master;
        Music = music;
        SFX = sfx;
    }
}
[Serializable]
public class VideoData
{
    public bool Fullscreen;
    public int[] Resolution;
    public int Quality;

    public VideoData()
    {
        Fullscreen = true;
        Resolution = new[] { 1920, 1080, 120 };
        Quality = 5;
    }

    public VideoData(bool fullscreen, int[] resolution, int quality)
    {
        Fullscreen = fullscreen;
        Resolution = resolution;
        Quality = quality;
    }
}
[Serializable]
public class GameplayData
{
    
}
[Serializable]
public class ControlsData
{
    
}