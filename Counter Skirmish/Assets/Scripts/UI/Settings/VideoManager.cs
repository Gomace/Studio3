using UnityEngine;

public class VideoManager : MonoBehaviour
{
    public const string VideoSettingsPath = "/VideoData.json";

    private bool _fullscreen;
    private int[] _resolution;
    private int _quality;
    private Resolution[] _resolutions;

    #region VideoSettings
    public bool Fullscreen
    {
        get => _fullscreen;
        set
        {
            _fullscreen = value;
            ReloadFullscreen();
        }
    }
    public int[] Resolution
    {
        get => _resolution;
        set
        {
            _resolution = value;
            ReloadResolution();
        }
    }
    public int Quality
    {
        get => _quality;
        set
        {
            _quality = value;
            ReloadQuality();
        }
    }
    public Resolution[] Resolutions => _resolutions;
    #endregion VideoSettings

    private void Awake()
    {
        _resolutions = Screen.resolutions;
        
        LoadVideo();
    }

    public void LoadVideo()
    {
        VideoData data = SettingsSaver.LoadFromJson<VideoData>(VideoSettingsPath) ?? new VideoData();

        _fullscreen = data.Fullscreen;
        _resolution = data.Resolution;
        _quality = data.Quality;
        
        ReloadVideo();
    }
    
    #region ReloadingVideo
    private void ReloadVideo() // All video settings
    {
        ReloadFullscreen();
        ReloadResolution();
        ReloadQuality();
    }
    private void ReloadFullscreen() { Screen.fullScreen = _fullscreen; } // Fullscreen
    private void ReloadResolution() { Screen.SetResolution(_resolution[0], _resolution[1], Screen.fullScreen, _resolution[2]); } // Resolution
    private void ReloadQuality() { QualitySettings.SetQualityLevel(Quality); } // Quality
    #endregion ReloadingVideo
}