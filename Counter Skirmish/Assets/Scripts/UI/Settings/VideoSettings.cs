using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private VideoManager _videoManager;
    [SerializeField] private GameObject _saveCurrent, _resetDefault;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    #endregion SerializedFields
    
    private void Awake()
    {
        _settingsMenu.onLoadSettings += _videoManager.LoadVideo;
        _settingsMenu.onLoadSettings += LoadVideo;
        _settingsMenu.onSettingsSaved += SaveVideoSettings;
        _settingsMenu.onResetSettings += ResetVideoSettings;
    }
    private void OnEnable() { _saveCurrent.SetActive(true); _resetDefault.SetActive(true); }
    private void OnDisable() { _saveCurrent.SetActive(false); _resetDefault.SetActive(false); }
    private void Start()
    {
        LoadVideoSettings();
        
        if (_resolutionDropdown)
            SetupResolutions();
    }

    #region VideoSettings
    public void SetFullscreen(bool isFullscreen) { _videoManager.Fullscreen = isFullscreen; }
    public void SetResolution(int resolutionIndex) { SetResolutionFromIndex(resolutionIndex); }
    public void SetQuality(int qualityIndex) { _videoManager.Quality = qualityIndex; }
    #endregion VideoSettings

    public void SaveVideoSettings()
    {
        VideoData data = new VideoData(_videoManager.Fullscreen, _videoManager.Resolution, _videoManager.Quality);
        SettingsSaver.SaveToJson(data, VideoManager.VideoSettingsPath);
    }
    private void LoadVideoSettings(VideoData data = null)
    {
        SetFullscreen(data?.Fullscreen ?? _videoManager.Fullscreen);
        _videoManager.Resolution = data?.Resolution ?? _videoManager.Resolution;
        SetQuality(data?.Quality ?? _videoManager.Quality);
    }
    private void LoadVideo() { LoadVideoSettings(); }
    public void ResetVideoSettings() { LoadVideoSettings(new VideoData()); }
    
    private void SetupResolutions()
    {
        _resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _videoManager.Resolutions.Length; i++)
        {
            string option = _videoManager.Resolutions[i].width + "x" + _videoManager.Resolutions[i].height + " @ " + _videoManager.Resolutions[i].refreshRate + "hz";;
            options.Add(option);

            if (_videoManager.Resolutions[i].width == Screen.currentResolution.width &&
                _videoManager.Resolutions[i].height == Screen.currentResolution.height &&
                _videoManager.Resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                currentResolutionIndex = i;
        }
        
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
    private void SetResolutionFromIndex(int resolutionIndex)
    {
        _videoManager.Resolution = new[]
        {
            _videoManager.Resolutions[resolutionIndex].width,
            _videoManager.Resolutions[resolutionIndex].height,
            _videoManager.Resolutions[resolutionIndex].refreshRate
        };
    }
}