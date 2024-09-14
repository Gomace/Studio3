using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    private const string VideoSettingsPath = "/VideoData.json";
    
    #region SerializedFields
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private GameObject _saveCurrent, _resetDefault;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private RectTransform _TxtForIndicators, _indicatorPrefab;
    [SerializeField] private Sprite _selected, _notSelected;
    #endregion SerializedFields
    
    private Resolution[] _resolutions;
    private Image[] _resIndicators;

    private void Awake()
    {
        _settingsMenu.onLoadSettings += LoadVideo;
        // _settingsMenu.onSettingsSaved += 
        // _settingsMenu.onResetSettings += 
    }
    private void OnEnable() { _saveCurrent.SetActive(true); _resetDefault.SetActive(true); }
    private void OnDisable() { _saveCurrent.SetActive(false); _resetDefault.SetActive(false); }
    private void Start()
    {
        if (_resolutionDropdown)
            SetupResolutions();
        
        LoadVideo();
    }

    public void SetFullscreen(bool isFullscreen) { Screen.fullScreen = isFullscreen; }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex) { QualitySettings.SetQualityLevel(qualityIndex); }
    
    private void SetupResolutions()
    {
        _resolutions = Screen.resolutions;
        _resIndicators = new Image[_resolutions.Length];
        
        _resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " @ " + _resolutions[i].refreshRate + "hz";;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height &&
                _resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                currentResolutionIndex = i;
        }
        
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
        
        // OptionIndicators(_resolutions.Length);
    }

    private void OptionIndicators(int options)
    {
        if (options > 6) return;
        
        for (int i = 0; i < options; ++i)
        {
            RectTransform indicator = Instantiate(_indicatorPrefab, _TxtForIndicators);
            indicator.localPosition = new Vector2(
                options % 2 == 0
                    ? 26 + (options / 2 * -1 + i) * 52
                    : (options / 2 * -1 + i) * 52,
                indicator.localPosition.y);
            indicator.GetComponent<Button>().onClick.AddListener(() => { SetResolution(i); });
        }
    }

    public void SaveVideoSettings()
    {
        VideoData data = new VideoData();
        SettingsSaver.SaveToJson(data, VideoSettingsPath);
    }
    private void LoadVideoSettings(VideoData data = null) // Something something
    {
        
    }
    private void LoadVideo() // something something
    {
        
    }
    public void ResetVideoSettings() { LoadVideoSettings(new VideoData()); }
}
