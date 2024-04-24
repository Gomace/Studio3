using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class DetailsMenu : MonoBehaviour
{
    public delegate void OnDetailsLoad(CreatureInfo @creature);
    public event OnDetailsLoad onDetailsLoad;

    [SerializeField] private HubCharacter _player;
    
    [Header("These should be filled out.")]
    [SerializeField] private RectTransform _categories;
    [SerializeField] private RectTransform _screens;
    [SerializeField] private Color _clickColor = new Color(55, 55, 55, 255),
                                    _normalColor = new Color(128, 128, 128, 255);
    
    private CreatureInfo _creature;
    
    public CreatureInfo Creature
    {
        get => _creature;
        set
        {
           _creature = value; 
           LoadDetails();
        } 
    }
    
    private void OnEnable() => LoadDetails();

    private void LoadDetails() => onDetailsLoad?.Invoke(_creature);

    public void UpdateAbilities()
    {
        _player.SaveRoster();
        // _player.SaveCollection();
    }
    
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
