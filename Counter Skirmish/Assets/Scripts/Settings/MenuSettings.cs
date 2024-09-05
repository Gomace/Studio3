using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    #region Settings
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
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    #endregion Settings
}