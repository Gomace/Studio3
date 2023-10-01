using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    private void Start()
    {
        if (resolutionDropdown)
            Resolution();
    }
    
    
    #region Settings
    public void SetMusicVolume(float volume)
    {
        //player.transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
    }
    /*public void SetSensitivity(float sensitivity)
    {
        playerCam.mouseSens = sensitivity * 100f;
    }
    public void SetInvertY(bool isInverted)
    {
        playerCam.invY = isInverted;
    }
    public void SetInvertX(bool isInverted)
    {
        playerCam.invX = isInverted;
    }*/
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
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
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    #endregion Settings
}