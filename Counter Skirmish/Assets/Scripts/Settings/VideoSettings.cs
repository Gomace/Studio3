using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown _resolutionDropdown;
    [SerializeField] private RectTransform _TxtForIndicators, _indicatorPrefab;
    [SerializeField] private Sprite _selected, _notSelected;
    
    private Resolution[] _resolutions;
    private Image[] _resIndicators;

    
    private void Start()
    {
        if (_resolutionDropdown)
            SetupResolutions();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
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
                options % 2 == 0 ? 26 + (options / 2 * -1 + i) * 52 : (options / 2 * -1 + i) * 52,
                indicator.localPosition.y);
            indicator.GetComponent<Button>().onClick.AddListener(() => { SetResolution(i); });
        }
    }
}
