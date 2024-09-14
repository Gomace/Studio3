using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _saveCurrent, _resetDefault;
    [SerializeField] private BarAndNumSyncer _masterSync, _musicSync, _sfxSync;
    #endregion SerializedFields
    
    private void Awake()
    {
        _settingsMenu.onLoadSettings += _audioManager.LoadAudio;
        _settingsMenu.onSettingsSaved += SaveAudioSettings;
        _settingsMenu.onResetSettings += ResetAudioSettings;
    }
    private void OnEnable() { _saveCurrent.SetActive(true); _resetDefault.SetActive(true); }
    private void OnDisable() { _saveCurrent.SetActive(false); _resetDefault.SetActive(false); }
    private void Start() { LoadAudioSettings(); }

    #region Volumes
        #region Sliders
    public void SetMasterVolume(float volume)
    {
        _audioManager.MasterV = volume;
        _masterSync.InputField.text = Mathf.RoundToInt(volume * 100f).ToString();
    }
    public void SetMusicVolume(float volume)
    {
        _audioManager.MusicV = volume;
        _musicSync.InputField.text = Mathf.RoundToInt(volume * 100f).ToString();
    }
    public void SetSFXVolume(float volume)
    {
        _audioManager.SFXV = volume;
        _sfxSync.InputField.text = Mathf.RoundToInt(volume * 100f).ToString();
    }
        #endregion Sliders

        #region Inputs
    public void InputMasterVolume(string input) { _audioManager.MasterV = _masterSync.Slider.value = Mathf.Clamp(Convert.ToInt32(input), 0, 100) / 100f; }
    public void InputMusicVolume(string input) { _audioManager.MusicV = _musicSync.Slider.value = Mathf.Clamp(Convert.ToInt32(input), 0, 100) / 100f; }
    public void InputSFXVolume(string input) { _audioManager.SFXV = _sfxSync.Slider.value = Mathf.Clamp(Convert.ToInt32(input), 0, 100) / 100f; }
        #endregion Inputs
    #endregion Volumes

    public void SaveAudioSettings()
    {
        AudioData data = new AudioData(_audioManager.MasterV, _audioManager.MusicV, _audioManager.SFXV);
        SettingsSaver.SaveToJson(data, AudioManager.AudioSettingsPath);
    }
    private void LoadAudioSettings(AudioData data = null)
    {
        SetMasterVolume(data?.Master ?? _audioManager.MasterV);
        SetMusicVolume(data?.Music ?? _audioManager.MusicV);
        SetSFXVolume(data?.SFX ?? _audioManager.SFXV);
    }
    public void ResetAudioSettings() { LoadAudioSettings(new AudioData()); } // Add to reset default current
}

[Serializable]
public class BarAndNumSyncer
{
    public Slider Slider;
    public TMP_InputField InputField;
}