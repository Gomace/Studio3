using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _btns, _categories;
    [SerializeField] private Color _clickColor = new Color(55, 55, 55, 255), 
                                    _normalColor = new Color(128, 128, 128, 255);
    
    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _btns)
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor;

        foreach (RectTransform cat in _categories)
            cat.gameObject.SetActive(false);
        category.SetActive(true);
    }
}