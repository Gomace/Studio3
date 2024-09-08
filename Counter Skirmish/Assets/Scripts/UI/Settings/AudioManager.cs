using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    private const string _audioSettingsPath = "/AudioData.json";
    
    #region SerializedFields
    [SerializeField] private AudioSource[] _musicS = Array.Empty<AudioSource>(),
                                            _sfxS = Array.Empty<AudioSource>();
    #endregion SerializedFields
    
    private float _masterV, _musicV, _sfxV;
    private float[] _defMusic, _defSFX;
    
    private void Awake()
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

    private void SaveAudio()
    {
        AudioData prefs = new AudioData(_masterV, _musicV, _sfxV);
    }
    private void LoadAudio()
    {
        AudioData prefs = SettingsSaver.LoadFromJson<AudioData>(_audioSettingsPath) ?? new AudioData();

        _masterV = prefs.Master;
        _musicV = prefs.Music;
        _sfxV = prefs.SFX;
        
        for (int i = 0; i < _musicS.Length; ++i)
            if (_musicS[i] != null)
                _musicS[i].volume = _defMusic[i] * _musicV * _masterV;

        for (int i = 0; i < _sfxS.Length; ++i)
            if (_sfxS[i] != null)
                _sfxS[i].volume = _defSFX[i] * _sfxV * _masterV;
    }

    public void SetMusicVolume(float volume)
    {
        //_player.transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
    }
}