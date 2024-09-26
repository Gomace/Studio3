using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    #region Events
    public delegate void OnSettingsSaved();
    public event OnSettingsSaved onSettingsSaved;
    public delegate void OnLoadSettings();
    public event OnLoadSettings onLoadSettings;
    public delegate void OnResetSettings();
    public event OnResetSettings onResetSettings;
    #endregion Events
    
    [SerializeField] private RectTransform _btns, _categories;
    [SerializeField] private Color _clickColor = new(55, 55, 55, 255), 
                                    _normalColor = new(128, 128, 128, 255);

    public void OnDisable() { onLoadSettings?.Invoke(); }

    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _btns) // Reset button colors
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor; // Change selected button color

        foreach (RectTransform cat in _categories) // Disable all categories
            cat.gameObject.SetActive(false);
        category.SetActive(true); // Enable selected category
    }

    public void SaveSettings() { onSettingsSaved?.Invoke(); }
    public void ResetSettings() { onResetSettings?.Invoke(); } // Each category's reset button is set in each category's leading script
}