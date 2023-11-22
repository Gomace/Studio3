using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class DetailsMenu : MonoBehaviour
{
    public delegate void OnDetailsLoad(CreatureBase @base);
    public static event OnDetailsLoad onDetailsLoad;
    
    [SerializeField] private RectTransform _categories, _screens;
    [SerializeField] private Color _clickColor = new Color(55, 55, 55, 255),
                                    _normalColor = new Color(128, 128, 128, 255);
    
    private CreatureBase _cBase;
    
    public CreatureBase CBase
    {
        get => _cBase;
        set
        {
           _cBase = value; 
           LoadDetails();
        } 
    }
    
    private void OnEnable() => LoadDetails();

    private void LoadDetails() => onDetailsLoad?.Invoke(CBase);

    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _categories)
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor;

        foreach (RectTransform screen in _screens)
            screen.gameObject.SetActive(false);
        category.SetActive(true);
        
        LoadDetails();
    }
}
