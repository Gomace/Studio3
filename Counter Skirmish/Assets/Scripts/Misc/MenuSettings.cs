using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    
    [SerializeField] private TMPro.TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;
    
    private void Start()
    {
        if (_resolutionDropdown)
            Resolution();
    }
    
    
    #region Settings
    public void SetMusicVolume(float volume)
    {
        //_player.transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
    }
    /*public void SetSensitivity(float sensitivity)
    {
        _playerCam.mouseSens = sensitivity * 100f;
    }
    public void SetInvertY(bool isInverted)
    {
        _playerCam.invY = isInverted;
    }
    public void SetInvertX(bool isInverted)
    {
        _playerCam.invX = isInverted;
    }*/
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    private void Resolution()
    {
        _resolutions = Screen.resolutions;
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
    }
    #endregion Settings
}