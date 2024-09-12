using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private AudioManager _audioManager;
    

    private void Awake()
    {
        _settingsMenu.onSettingsSaved += SaveAudioSettings;
        _settingsMenu.onLoadSettings += LoadAudioSettings;
    }
    
    #region Volumes
    public void SetMasterVolume(float volume) { _audioManager.MasterV = volume; }
    public void SetMusicVolume(float volume) { _audioManager.MusicV = volume; }
    public void SetSFXVolume(float volume) { _audioManager.SFXV = volume; }

        #region NumInputs
    public void InputMasterVolume(string input)
    {
        _audioManager.MasterV = Convert.ToInt32(input);
    }
    public void InputMusicVolume(string input)
    {
        _audioManager.MusicV = Convert.ToInt32(input);
    }
    public void InputSFXVolume(string input)
    {
        _audioManager.SFXV = Convert.ToInt32(input);
    }
        #endregion NumInputs
    #endregion Volumes

    private void SaveAudioSettings()
    {
        AudioData data = new AudioData(_audioManager.MasterV, _audioManager.MusicV, _audioManager.SFXV);
        SettingsSaver.SaveToJson(data, AudioManager.AudioSettingsPath);
    }
    private void LoadAudioSettings()
    {
        AudioData data = SettingsSaver.LoadFromJson<AudioData>(AudioManager.AudioSettingsPath) ?? new AudioData();
        
        _audioManager.MasterV = data.Master;
        _audioManager.MusicV = data.Music;
        _audioManager.SFXV = data.SFX;
    }
}