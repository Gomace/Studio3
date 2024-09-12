using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const string AudioSettingsPath = "/AudioData.json";
    
    #region SerializedFields
    [SerializeField] private AudioSource[] _musicS = Array.Empty<AudioSource>(),
                                            _sfxS = Array.Empty<AudioSource>();
    #endregion SerializedFields

    private float _masterV, _musicV, _sfxV;
    private float[] _defMusic, _defSFX;

    #region Volumes
    public float MasterV
    {
        get => _masterV;
        set
        {
            _masterV = value;
            ReloadAudio();
        }
    }
    public float MusicV 
    {
        get => _musicV;
        set
        {
            _musicV = value;
            ReloadMusic();
        }
    }
    public float SFXV
    {
        get => _sfxV;
        set
        {
            _sfxV = value;
            ReloadSFX();
        }
    }
    #endregion Volumes

    public void Awake()
    {
        _defMusic = new float[_musicS.Length]; // Equal amounts of defaults and sources
        _defSFX = new float[_sfxS.Length];

        for (int i = 0; i < _musicS.Length; ++i) // Set default music volumes
            if (_musicS[i] != null)
                _defMusic[i] = _musicS[i].volume;
        
        for (int i = 0; i < _sfxS.Length; ++i) // Set default sfx volumes
            if (_sfxS[i] != null)
                _defSFX[i] = _sfxS[i].volume;
        
        LoadAudio();
    }

    private void LoadAudio()
    {
        AudioData data = SettingsSaver.LoadFromJson<AudioData>(AudioSettingsPath) ?? new AudioData();

        for (int i = 0; i < _musicS.Length; ++i)
            if (_musicS[i] != null)
                _musicS[i].volume = _defMusic[i] * data.Music * data.Master;

        for (int i = 0; i < _sfxS.Length; ++i)
            if (_sfxS[i] != null)
                _sfxS[i].volume = _defSFX[i] * data.SFX * data.Master;
    }

    #region ReloadingAudio
    private void ReloadAudio() // Master
    {
        ReloadMusic();
        ReloadSFX();
    }
    private void ReloadMusic() // Music
    {
        for (int i = 0; i < _musicS.Length; ++i)
            if (_musicS[i] != null)
                _musicS[i].volume = _defMusic[i] * _musicV * _masterV;
    }
    private void ReloadSFX() // SFX
    {
        for (int i = 0; i < _sfxS.Length; ++i)
            if (_sfxS[i] != null)
                _sfxS[i].volume = _defSFX[i] * _sfxV * _masterV;
    }
    #endregion ReloadingAudio
}